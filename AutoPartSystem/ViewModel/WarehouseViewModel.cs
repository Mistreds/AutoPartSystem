#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoPartSystem.Model.Warehouse;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{

    public class WarehouseAdd : Data.Warehouse
    {
        private string model_name;
        public string ModelName
        {
            get => model_name;
            set => this.RaiseAndSetIfChanged(ref model_name, value);
        }
        private Data.Mark _mark;
        public Data.Mark Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private string mark_name;
        public string MarkName
        {
            get => mark_name;
            set => this.RaiseAndSetIfChanged(ref mark_name, value);
        }
        public WarehouseAdd()
        {
            this.Goods = new Data.Goods();
            this.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel>();
        }
    }
    public class ModelAdd:Data.Model
    {
        private string mark_name;
        public string MarkName
        {
            get => mark_name;
            set => this.RaiseAndSetIfChanged(ref mark_name, value);
        }

        private Data.Model _model;
        public Data.Model Model
        {
            get => _model;
            set=>this.RaiseAndSetIfChanged(ref _model, value);
        }
    }
    public class WarehouseTable : Data.Warehouse
    {
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set => this.RaiseAndSetIfChanged(ref _is_selected, value);

        }

        public WarehouseTable()
        {
        }

        public WarehouseTable(int id = default, Data.Goods good = null, int inAlmata = default, int inAstana = default, int inAktau = default,
         string warehousePlace = default,
            string typePay = null, string note = null)
        {
            Id = id;
            Goods = good;
            InAlmata = inAlmata;
            InAstana = inAstana;
            InAktau = inAktau;
            WarehousePlace = warehousePlace;
            TypePay = typePay;
            Note = note;
        }

    }
    public class MarkModelFind : ReactiveObject
    {
        public int model_id { get; set; }
        public string model_name { get; set; }
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set => this.RaiseAndSetIfChanged(ref _is_selected, value);
        }
        public MarkModelFind() { }
        public MarkModelFind(int id, string name)
        {
            model_id = id;
            model_name = name;
            IsSelected = true;
        }
    }

    public class WarehouseViewModel : ReactiveObject
    {
        #region UserControls
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<UserControl>? _controls;
        #endregion
        #region AddNewWarehouseData
        private ObservableCollection<Data.Model>? _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set => this.RaiseAndSetIfChanged(ref _models, value);
        }
        private ObservableCollection<Data.Mark>? _mark;
        public ObservableCollection<Data.Mark>? Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private WarehouseAdd? _warehouse;
        public WarehouseAdd? Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private ModelAdd _model;
        public ModelAdd Model
        {
            get => _model;
            set=>this.RaiseAndSetIfChanged(ref _model, value);
        }
        #endregion
        #region WarehouseTableData
        private ObservableCollection<WarehouseTable> _warehouses_table;
        public ObservableCollection<WarehouseTable> WarehousesTable
        {
            get => _warehouses_table;
            set => this.RaiseAndSetIfChanged(ref _warehouses_table, value);
        }
        #endregion
        #region Models
        public Model.MarkModel.MarkModel? MarkModel;
        private Model.Warehouse.WarehouseModel WarehouseModel;
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        #endregion
        #region WarehouseSortAndFind
        private bool _is_model_find;
        public bool IsModelFind
        {
            get => _is_model_find;
            set=>this.RaiseAndSetIfChanged(ref _is_model_find, value); 
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
        private ObservableCollection<MarkModelFind>? _article_find;
        public ObservableCollection<MarkModelFind> ArticleFind
        {
            get => _article_find;
            set => this.RaiseAndSetIfChanged(ref _article_find, value);
        }
        #endregion
        public WarehouseViewModel()
        {
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(), new View.Warehouse.AddToWarehouse() };
            MainControl = _controls[0];
            MarkModel = new Model.MarkModel.MarkModel();
            Mark = MarkModel.GetMark();
            Warehouse = new WarehouseAdd();
            WarehouseModel = new WarehouseModel();
            WarehousesTable = WarehouseModel.GetAllWarehouse();
            WarehouseInvoceModel = new WarehouseInvoceModel();
            MarkModelFind = MarkModel.MarkModelFind("");
            DescriptionFind = WarehouseModel.GetAllDesctiption("");
            ArticleFind = WarehouseModel.GetAllArticle("");
            Model = new ModelAdd();
        }
        public ReactiveCommand<Unit, Unit> AddModelToWareTable => ReactiveCommand.Create(() => {


            if (Model.Mark != null)
            {
                if (Model.Model != null)
                {
                    Model.Model.Mark = Model.Mark;
                    Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model= Model.Model,ModelId=Model.Id });
                }
                else
                {
                    Model.Model = MarkModel.GetModelFromNameFind(Model.Name, Model.Mark.Id);
                    if (Model.Model != null)
                    {
                        Model.Model.Mark = Model.Mark;
                        Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model = Model.Model, ModelId = Model.Id });
                    }
                    else
                    {
                        Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, Model.Mark.Id, Model.Mark) });
                    }
                }
            }
            else
            {
                Model.Mark = MarkModel.GetMarkFromNameFind(Model.MarkName);
                if (Model.Mark != null)
                {
                   Model.Model = MarkModel.GetModelFromNameFind(Model.Name, Model.Mark.Id);
                    if (Model.Model != null)
                    {
                        Model.Model.Mark = Model.Mark;
                        Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model = Model.Model, ModelId = Model.Id });
                    }
                    else
                    {
                        Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, Model.Mark.Id,Model.Mark) });
                    }
                }
                else
                {
                    Warehouse.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, new Data.Mark(Model.MarkName)) });
                }
            }

        });
        public void OpenPageCommand(string page_id)
        {
            switch (page_id)
            {
                case "ZavSkladTable":
                    MainControl = _controls[0];
                    break;
            }

        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand);
        private void SelectModelFromMarkCommand(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<Unit, Unit> AddWarehous => ReactiveCommand.Create(() => {
            foreach(var a in Warehouse.Goods.GoodsModel)
            {
                Console.WriteLine(a.ModelId);
                if(a.Model.Id!=0)
                {
                    a.ModelId = a.Model.Id;
                    a.Model=null;
                }
                else
                {
                    if(a.Model.Mark.Id!=0)
                    {
                        a.Model.MarkId=a.Model.Mark.Id;
                        a.Model.Mark=null;
                    }
                }
            }
            WarehouseModel.AddWarehouse(Warehouse);
            Warehouse = new WarehouseAdd();
        });
        public ReactiveCommand<Unit, Unit> AddSale => ReactiveCommand.Create(() => {
            WarehouseInvoceModel.SetWarehouse(new ObservableCollection<WarehouseTable>(WarehousesTable.Where(p => p.IsSelected == true)));

            InvoiceWinViewModel invoiceWinViewModel = new InvoiceWinViewModel(WarehouseInvoceModel);
        });
        #region WarehouseSortFilterCommand
        
            public ReactiveCommand<string, Unit> SelectFindModel => ReactiveCommand.Create<string>(SelectFindModelCommand);
            private void SelectFindModelCommand(string name)
            {
                Console.WriteLine(name);
                MarkModelFind = MarkModel.MarkModelFind(name);
            }
            public ReactiveCommand<string, Unit> SelectFindDesctiption => ReactiveCommand.Create<string>(SelectFindDesctiptionCommand);
            private void SelectFindDesctiptionCommand(string name)
            {
                Console.WriteLine(name);
                DescriptionFind = WarehouseModel.GetAllDesctiption(name);
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
                       // WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => $"{p.Goods.Model.Mark.Name} {p.Goods.Model.Name}").ToList());
                        break;
                    case "ModelUp":
                       // WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => $"{p.Goods.Model.Mark.Name} {p.Goods.Model.Name}").ToList());
                        break;
                    case "DescriptionDown":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p =>p.Goods.Description).ToList());
                        break;
                    case "DescriptionUp":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Description).ToList());
                        break;
                    case "ArticleDown":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p =>p.Goods.Article).ToList());
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
                            model.IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var model in MarkModelFind)
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
            public ReactiveCommand<Unit, Unit> SetFilter => ReactiveCommand.Create(() => {
                WarehousesTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesFilter(MarkModelFind, DescriptionFind, ArticleFind));
                
            });
        #endregion
        public ReactiveCommand<WarehouseTable, Unit> CreatePrihod => ReactiveCommand.Create<WarehouseTable>(CreatePrihodCommand);
        private void CreatePrihodCommand(WarehouseTable table)
        {
            var ArrGoods= new View.Warehouse.ArrivelGoods();
            ArrGoods.Show();
        }
        public ReactiveCommand<Unit, Unit> AddNewWarehouseWinOpen => ReactiveCommand.Create(AddNewWarehouseWinOpenCommand);
        public void AddNewWarehouseWinOpenCommand()
        {
            View.Warehouse.AddToWarehousePage addToWarehousePage = new View.Warehouse.AddToWarehousePage();
            Mark = MarkModel.GetMark();
            addToWarehousePage.Show();
        }
    }
}
