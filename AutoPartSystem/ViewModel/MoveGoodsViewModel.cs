using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class MoveGoodsViewModel:ReactiveObject
    {
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<WarehouseTable> _warehouses_table;
        public ObservableCollection<WarehouseTable> WarehousesTable
        {
            get => _warehouses_table;
            set => this.RaiseAndSetIfChanged(ref _warehouses_table, value);
        }
        private bool _is_model_find;
        public bool IsModelFind
        {
            get => _is_model_find;
            set => this.RaiseAndSetIfChanged(ref _is_model_find, value);
        }
        private bool _is_description_find;
        public bool IsDesctiptionFind
        {
            get => _is_description_find;
            set => this.RaiseAndSetIfChanged(ref _is_description_find, value);
        }
        private bool _is_article_find;
        public bool IsArticleFind
        {
            get => _is_article_find;
            set => this.RaiseAndSetIfChanged(ref _is_article_find, value);
        }
        private List<Data.City> _cities;
        public List<Data.City> Cities
        {
            get => _cities;
            set=>this.RaiseAndSetIfChanged(ref _cities, value);
        }
        private ObservableCollection<MarkModelFind>? _mark_model_find;
        public ObservableCollection<MarkModelFind> MarkModelFind
        {
            get => _mark_model_find;
            set => this.RaiseAndSetIfChanged(ref _mark_model_find, value);
        }
        private ObservableCollection<MarkModelFind>? _description_find;
        public ObservableCollection<MarkModelFind> DescriptionFind
        {
            get => _description_find;

            set => this.RaiseAndSetIfChanged(ref _description_find, value);
        }
        private int city_id_1;
        public int CityId1
        {
            get => city_id_1;
            set=>this.RaiseAndSetIfChanged(ref city_id_1, value);
        }
        private int city_id_2;
        public int CityId2
        {
            get => city_id_2;
            set => this.RaiseAndSetIfChanged(ref city_id_2, value);
        }
        private ObservableCollection<MarkModelFind>? _article_find;
        public ObservableCollection<MarkModelFind> ArticleFind
        {
            get => _article_find;
            set => this.RaiseAndSetIfChanged(ref _article_find, value);
        }
        public Model.MarkModel.MarkModel? MarkModel;
        private Model.Warehouse.WarehouseModel? WarehouseModel;
        private View.MoveGoods.MainMove mainMove;
        private ObservableCollection<Data.MoveGoods> _move_goods;
        public ObservableCollection<Data.MoveGoods> MoveGoods
        {
            get => _move_goods;
            set => this.RaiseAndSetIfChanged(ref _move_goods, value);
        }
        private ObservableCollection<UserControl> _controls;
        public MoveGoodsViewModel(Model.Warehouse.WarehouseModel WarehouseModel, ObservableCollection<WarehouseTable> WarehousesTable)
        {
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.MoveOrder(), new View.Warehouse.CheckMove() };
            SetFilter = ReactiveCommand.Create(() =>
            {
                this.WarehousesTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesFilterZav(MarkModelFind, DescriptionFind, ArticleFind));
            });
            MarkModel = MainViewModel._markModel;
            this.WarehousesTable = WarehousesTable;
            this.WarehouseModel = WarehouseModel;
            mainMove = new View.MoveGoods.MainMove();
            mainMove.DataContext = this;
            Cities = MainViewModel.AdminModel.GetEmpCity();
            mainMove.Show();
            MarkModelFind = MarkModel.MarkModelFind("");
            DescriptionFind = WarehouseModel.GetAllDesctiption("");
            ArticleFind = WarehouseModel.GetAllArticle("");
            MainControl = _controls[0];
        }
        #region WarehouseSortFilterCommand
        public ReactiveCommand<string, Unit> SelectFindModel => ReactiveCommand.Create<string>(SelectFindModelCommand);
        private void SelectFindModelCommand(string name)
        {

            MarkModelFind = MarkModel.MarkModelFind(name);
        }

        public ReactiveCommand<string, Unit> SelectFindDesctiption => ReactiveCommand.Create<string>(SelectFindDesctiptionCommand);
        private void SelectFindDesctiptionCommand(string name)
        {
            DescriptionFind = WarehouseModel.GetAllDesctiption(name);
        }
        public ReactiveCommand<string, Unit> SelectFindVirtualDesctiption => ReactiveCommand.Create<string>(SelectFindVirtualDesctiptionCommand);
        private void SelectFindVirtualDesctiptionCommand(string name)
        {

            DescriptionFind = WarehouseModel.GetAllVirtualDesctiption(name);
        }
        public ReactiveCommand<string, Unit> SelectFindArticle => ReactiveCommand.Create<string>(SelectFindArticleCommand);
        private void SelectFindArticleCommand(string name)
        {

            ArticleFind = WarehouseModel.GetAllArticle(name);
        }
        public ReactiveCommand<string, Unit> SortWarehouse => ReactiveCommand.Create<string>(SortWarehouseCommand);
        private void SortWarehouseCommand(string name)
        {
            switch (name)
            {
                case "ModelDown":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => p.Goods.GoodsModel.Select(p => $"{ p.Model.Mark.Name} {p.Model.Name}").FirstOrDefault()).ToList());
                    break;
                case "ModelUp":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.GoodsModel.Select(p => $"{ p.Model.Mark.Name} {p.Model.Name}")).ToList());
                    break;
                case "DescriptionDown":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => p.Goods.Description).ToList());
                    break;
                case "DescriptionUp":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Description).ToList());
                    break;
                case "ArticleDown":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => p.Goods.Article).ToList());
                    break;
                case "ArticleUp":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Article).ToList());
                    break;
            }
        }
        public ReactiveCommand<int, Unit> SelectAllModel => ReactiveCommand.Create<int>(SelectAllModelCommand);
        private void SelectAllModelCommand(int name)
        {
            var bools = MarkModelFind.FirstOrDefault(p => p.model_id == 0);
            if (name == 0)
            {
                if (bools.IsSelected)
                {
                    foreach (var model in MarkModelFind)
                    {
                        if (model.model_id == 0)
                        {
                            continue;
                        }
                        var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == model.model_id).FirstOrDefault(); ;
                        mod.IsSelected = true;
                        model.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var model in MarkModelFind)
                    {
                        model.IsSelected = false;
                        if (model.model_id == 0)
                        {
                            continue;
                        }
                        var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == model.model_id).FirstOrDefault(); ;
                        mod.IsSelected = false;
                    }
                }
            }
            else
            {
                bools.IsSelected = false;
                var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == name).FirstOrDefault();
                mod.IsSelected = MarkModelFind.FirstOrDefault(p => p.model_id == name).IsSelected;
            }
        }
        public ReactiveCommand<int, Unit> SelectAllDesctiption => ReactiveCommand.Create<int>(SelectAllDesctiptionCommand);
        private void SelectAllDesctiptionCommand(int name)
        {
            var bools = DescriptionFind.Where(p => p.model_id == 0).FirstOrDefault();
            if (name == 0)
            {
                if (bools.IsSelected)
                {
                    foreach (var model in DescriptionFind)
                    {
                        model.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var model in DescriptionFind)
                    {
                        model.IsSelected = false;
                    }
                }
            }
            else
            {
                bools.IsSelected = false;
            }
        }
        public ReactiveCommand<int, Unit> SelectAllArticle => ReactiveCommand.Create<int>(SelectAllArticleCommand);
        private void SelectAllArticleCommand(int name)
        {
            var bools = ArticleFind.Where(p => p.model_id == 0).FirstOrDefault();
            if (name == 0)
            {
                if (bools.IsSelected)
                {
                    foreach (var model in ArticleFind)
                    {
                        model.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var model in ArticleFind)
                    {
                        model.IsSelected = false;
                    }
                }
            }
            else
            {
                bools.IsSelected = false;
            }
        }
        private ReactiveCommand<Unit, Unit> _set_filter;
        public ReactiveCommand<Unit, Unit> SetFilter
        {
            get => _set_filter;
            set => this.RaiseAndSetIfChanged(ref _set_filter, value);
        }
        public ReactiveCommand<Unit, Unit> CreateMove => ReactiveCommand.Create(() => {
            if (CityId1 == 0 || CityId2 == 0)
            {
                MessageBox.Show("Город, откуда перемещают товар или город куда перемещают товар не выбран","Ошибка");
                return;
            }
            if(CityId1==CityId2)
            {
                MessageBox.Show("Нельзя переместить в этот же город ", "Ошибка");
                return;
            }
            MoveGoods = new ObservableCollection<Data.MoveGoods>();
                foreach (var ware in WarehousesTable.Where(p=>p.IsSelected).ToList())
            {
                MoveGoods.Add(new Data.MoveGoods { Count = ware.Goods.CountCell, WarehouseId = ware.Id, Warehouse = ware });
            }
            MainControl = _controls[1];
        });
        #endregion
    }
}
