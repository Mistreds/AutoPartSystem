using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace AutoPartSystem.ViewModel.Warehouse
{
    public class GoodCardViewModel : ReactiveObject
    {
        private Model.Warehouse.WarehouseModel WarehouseModel;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Warehouse _warehouse;
        public Data.Warehouse Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private Data.Model _model;
        public Data.Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        private ObservableCollection<Data.Model>? _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set => this.RaiseAndSetIfChanged(ref _models, value);
        }
        private ObservableCollection<Data.Mark>? _marklist;
        public ObservableCollection<Data.Mark>? MarkList
        {
            get => _marklist;
            set => this.RaiseAndSetIfChanged(ref _marklist, value);
        }
        private ObservableCollection<Data.Brand> _brands;
        public ObservableCollection<Data.Brand> Brands
        {
            get => _brands;
            set => this.RaiseAndSetIfChanged(ref _brands, value);
        }
        private string _brand_name;
        public string BrandName
        {
            get => _brand_name;
            set => this.RaiseAndSetIfChanged(ref _brand_name, value);
        }
        private string _mark_name;
        public string MarkName
        {
            get => _mark_name;
            set => this.RaiseAndSetIfChanged(ref _mark_name, value);
        }
        private string _model_name;
        public string ModelName
        {
            get => _model_name;
            set => this.RaiseAndSetIfChanged(ref _model_name, value);
        }
        private Data.Mark _mark;
        public Data.Mark Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        public GoodCardViewModel(Model.Warehouse.WarehouseModel WarehouseModel, Model.MarkModel.MarkModel MarkModel, Data.Warehouse Warehouse)
        {
            this.Warehouse = Warehouse;
            Console.WriteLine(Warehouse.Goods.GoodsModel.Count);
            Model = Warehouse.Goods.GoodsModel.FirstOrDefault().Model;
            Mark = Model.Mark;
            BrandName = Warehouse.Goods.Brand.Name;
            ModelName = Model.Name;
            MarkName = Mark.Name;
            MarkList = MarkModel.GetMark();
            Brands = MarkModel.GetBrand();
            Models = MarkModel.GetModelFromMarkId(Mark.Id);
            this.WarehouseModel = WarehouseModel;
            this.MarkModel = MarkModel;

        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand);
        private void SelectModelFromMarkCommand(int mark_id)
        {
            Console.WriteLine("dasd");
            Models = MarkModel.GetModelFromMarkId(mark_id);
            Model = new Data.Model();
            ModelName = "";
        }
        public ReactiveCommand<Unit, Unit> SaveWarehouse => ReactiveCommand.Create(() => {
            var model = new Data.Model();
            var mark = new Data.Mark();
            if (Mark != null)
            {
                if(Model!=null)
                {
                    model = MarkModel.GetModelFromNameFind(ModelName, Mark.Id);
                    if (model == null)
                    {
                        model=new Data.Model(ModelName, Mark.Id);
                    }
                    else
                    {
                        model.Mark = null;
                    }
                }
            }
            else
            {
                mark = MarkModel.GetMarkFromNameFind(MarkName);
                if (mark == null)
                {
                    model = new Data.Model(ModelName, new Data.Mark(MarkName));
                }
                else
                {

                    if (Model != null)
                    {
                        model = MarkModel.GetModelFromNameFind(ModelName, mark.Id);
                        if (model == null)
                        {
                            model = new Data.Model(ModelName, mark.Id);
                        }
                        else
                        {
                            model.Mark = null;
                        }
                    }
                }
            }
            MarkModel.AddModel(model);

        });
    }
}
