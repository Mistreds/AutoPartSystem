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
    
    public class WarehouseAdd:Data.Warehouse
    {
        private string model_name;
        public string ModelName
        {
            get => model_name;
            set=>this.RaiseAndSetIfChanged(ref model_name, value);
        }
        private Data.Mark _mark;
        public Data.Mark Mark
        {
            get=> _mark;
            set=>this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private string mark_name;
        public string MarkName
        {
            get=> mark_name;
            set=>this.RaiseAndSetIfChanged(ref mark_name, value);   
        }
    }
    public class WarehouseTable:Data.Warehouse
    {
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set=>this.RaiseAndSetIfChanged(ref _is_selected, value);
            
        }

        public WarehouseTable()
        {
        }

        public WarehouseTable(int id = default, Data.Goods good = null,  int inAlmata = default, int inAstana = default, int inAktau = default,
         int warehousePlace = default,
            string typePay = null, string note = null)
        {
            Id = id;
            Goods = good;
            InAlmata = inAlmata;
            InAstana = inAstana;
            InAktau = inAktau;
            WarehousePlace= warehousePlace;
            TypePay = typePay ;
            Note = note ;
        }
        
    }
    public class MarkModelFind:ReactiveObject
    {
        public int model_id { get; set; }
        public string model_name { get; set; }
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set=>this.RaiseAndSetIfChanged(ref _is_selected, value);
        }
        public MarkModelFind() { }
        public MarkModelFind(int id, string name)
        {
            model_id=id;
            model_name = name;
            IsSelected=true;
        }
    }

    public class WarehouseViewModel:ReactiveObject
    {
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set=>this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private bool _is_model_find;
        public bool IsModelFind
        {
            get => _is_model_find;
            set
            {
              
                this.RaiseAndSetIfChanged(ref _is_model_find, value);
            }
        }
        private ObservableCollection<Data.Model>? _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set=>this.RaiseAndSetIfChanged(ref _models,value);
        }
        private ObservableCollection<Data.Mark>? _mark;
        public ObservableCollection<Data.Mark>? Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private ObservableCollection<UserControl>? _controls;
        private ObservableCollection<WarehouseTable> _warehouses_table;
        public ObservableCollection<WarehouseTable> WarehousesTable
        {
            get => _warehouses_table;
            set => this.RaiseAndSetIfChanged(ref _warehouses_table, value);
        }
        public Model.MarkModel.MarkModel? MarkModel;
        private Model.Warehouse.WarehouseModel WarehouseModel;
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        private ObservableCollection<MarkModelFind>? _mark_model_find;
        public ObservableCollection<MarkModelFind> MarkModelFind
        {
            get => _mark_model_find;
            set => this.RaiseAndSetIfChanged(ref _mark_model_find, value);
        }
        private WarehouseAdd? _warehouse;
        public WarehouseAdd? Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        public WarehouseViewModel()
        {
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(), new View.Warehouse.AddToWarehouse() };
            MainControl = _controls[0];
            MarkModel=new Model.MarkModel.MarkModel();
            Mark=MarkModel.GetMark();
            Warehouse=new WarehouseAdd();
            Warehouse.Goods = new Data.Goods();
            WarehouseModel = new WarehouseModel();
            WarehousesTable = WarehouseModel.GetAllWarehouse();
            WarehouseInvoceModel=new WarehouseInvoceModel();
            MarkModelFind = MarkModel.MarkModelFind("");
            
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            MainControl = _controls[Convert.ToInt32(page_id)];
        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand);
        private void SelectModelFromMarkCommand(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<Unit, Unit> AddWarehous => ReactiveCommand.Create(() => {
            if (Warehouse.Mark != null)
            {
                if(Warehouse.Goods.Model != null)
                {
                    Warehouse.Goods.ModelId = Warehouse.Goods.Model.Id;
                    Warehouse.Goods.Model = null;
                }
                else
                {
                    Warehouse.Goods.Model = new Data.Model(Warehouse.ModelName, Warehouse.Mark.Id);
                }
            }
            else
            {
                Warehouse.Mark = MarkModel.GetMarkFromNameFind(Warehouse.MarkName);
                if(Warehouse.Mark!=null)
                {
                    Warehouse.Goods.Model = MarkModel.GetModelFromNameFind(Warehouse.ModelName, Warehouse.Mark.Id);
                    if (Warehouse.Goods.Model != null)
                    {
                        Warehouse.Goods.ModelId = Warehouse.Goods.Model.Id;
                        Warehouse.Goods.Model = null;
                    }
                    else
                    {
                        Warehouse.Goods.Model = new Data.Model(Warehouse.ModelName, Warehouse.Mark.Id);
                    }
                }
                else
                {
                    Warehouse.Goods.Model = new Data.Model(Warehouse.ModelName, new Data.Mark(Warehouse.MarkName));
                }
            }
            WarehouseModel.AddWarehouse(Warehouse);
            Warehouse = new WarehouseAdd();
        });
        public ReactiveCommand<Unit, Unit> AddSale => ReactiveCommand.Create(() => {
            WarehouseInvoceModel.SetWarehouse(new ObservableCollection<WarehouseTable>(WarehousesTable.Where(p=>p.IsSelected==true)));

            InvoiceWinViewModel  invoiceWinViewModel = new InvoiceWinViewModel(WarehouseInvoceModel);
        });
        public ReactiveCommand<string, Unit> SelectFindModel => ReactiveCommand.Create<string>(SelectFindModelCommand);
        private void SelectFindModelCommand(string name)
        {
            Console.WriteLine(name);
            MarkModelFind = MarkModel.MarkModelFind(name);
        }
        public ReactiveCommand<string, Unit> SortWarehouse => ReactiveCommand.Create<string>(SortWarehouseCommand);
        private void SortWarehouseCommand(string name)
        {
           switch(name)
            {
                case "ModelDown":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => $"{p.Goods.Model.Mark.Name} {p.Goods.Model.Name}").ToList());
                    break;
                case "ModelUp":
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => $"{p.Goods.Model.Mark.Name} {p.Goods.Model.Name}").ToList());
                    break;
            }
        }
        public ReactiveCommand<int, Unit> SelectAllModel => ReactiveCommand.Create<int>(SelectAllModelCommand);
        private void SelectAllModelCommand(int name)
        {
           var bools = MarkModelFind.Where(p => p.model_id == 0).FirstOrDefault();
            if (name == 0)
            {
                if(bools.IsSelected)
                {
                    foreach(var model in MarkModelFind)
                    {
                        model.IsSelected = true;
                    }
                }
                else
                {
                    foreach(var model in MarkModelFind)
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
        public ReactiveCommand<Unit, Unit> AddNewWarehouseWinOpen => ReactiveCommand.Create(() => {

            View.Warehouse.AddToWarehousePage addToWarehousePage = new View.Warehouse.AddToWarehousePage();
            addToWarehousePage.Show();
        });
    }
}
