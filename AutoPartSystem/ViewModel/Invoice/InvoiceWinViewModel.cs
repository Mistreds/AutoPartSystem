﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using OfficeOpenXml;
using System.Threading.Tasks;
using ReactiveUI;
using System.IO;
using OfficeOpenXml.Style;
using System.Windows.Controls;

namespace AutoPartSystem.ViewModel
{
    
    public class InvoiceWinViewModel:ReactiveObject
    {
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Invoice invoice;
        public Data.Invoice Invoice
        {
            get=> invoice;
            set=>this.RaiseAndSetIfChanged(ref invoice, value);
        }
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
        public List<Data.TypePay> TypePay { get; private set; }
        private ObservableCollection<Data.City> _cities;
        public ObservableCollection<Data.City> Cities
        {
            get => _cities;
            set=>this.RaiseAndSetIfChanged(ref _cities, value);
        }
        private UserControl main_control;
        public UserControl MainControl
        {
            get => main_control;
            set=>this.RaiseAndSetIfChanged(ref main_control, value);
        }
        public bool IsEdit { get; set; }
        public bool IsInvoice { get; set; }
        private View.Invoice.CreateInvoice CreateInvoice;
        private View.Invoice.AgentInvoice AgentInvoice;
        private View.Invoice.InvoceTable InvoiceTable;
        public List<Data.Employee> Employees { get; private set; }
        private bool _is_new_client;
        public bool IsNewClient
        {
            get => _is_new_client;
            set
            {
               
                
                Client=new Data.Client();
                Mark = MarkModel.GetMark();
                Invoice.Client = new Data.Client();
                Client.new_mark_model();
                Cities = MainViewModel.AdminModel.GetCitiesFromText("");
                this.RaiseAndSetIfChanged(ref _is_new_client, value);
            }
        }
        private Data.Client _client;
        public Data.Client Client
        {
            get => _client;
            set=>this.RaiseAndSetIfChanged(ref _client, value);
        }
        private bool _is_marzh;
        public bool IsMarzh
        {
            get => _is_marzh;
            set=>this.RaiseAndSetIfChanged(ref _is_marzh, value);  
        }
        private int _emp_id;
        public int EmpId
        {
            get => _emp_id;
            set=>this.RaiseAndSetIfChanged(ref _emp_id , value);
        }
        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel, Model.MarkModel.MarkModel MarkModel)
        {
            SelectNewClient= ReactiveCommand.Create(() => {

                   ClientViewModel clientView = new ClientViewModel(Invoice);

               });
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            Employees = MainViewModel.AdminModel.GetEmployeeMeneger(MainViewModel.Employee.Id);
            this.MarkModel = MarkModel;
            Mark = MarkModel.GetMark();
            Client = new Data.Client();
            Client.new_mark_model();
            CreateInvoice = new View.Invoice.CreateInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = CreateInvoice;
            Cities=MainViewModel.AdminModel.GetCitiesFromText("");
            CreateInvoiceBase = ReactiveCommand.Create(() =>
             {
                 if (IsNewClient)
                 {
                     MainViewModel.ClientModel.AddClient(Client);
                 }
                 if(Invoice.IsDelMarzh)
                     WarehouseInvoceModel.AddInvoiceToDataBase(Invoice, EmpId);
                 else
                    WarehouseInvoceModel.AddInvoiceToDataBase(Invoice);
             });
            foreach(var ss in WarehouseInvoceModel.GetWarehouse())
            {
                Console.WriteLine("asdata "+ss.Goods.TypePayId);
            }
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()),MainViewModel.Employee);
            try
            {
                this.WhenAnyValue(vm => vm.Client.MarkId).WhereNotNull().Subscribe(x => UpdateModelsNew(x));
                this.WhenAnyValue(vm => vm.Invoice.Client.MarkId).WhereNotNull().Subscribe(x => UpdateModels(x));
            }
            catch { }
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            invoiceGood.Show();      
        }
        public InvoiceWinViewModel(ObservableCollection<WarehouseTable> warehouseTables,bool isagent)
        {
            this.WarehouseInvoceModel = new Model.Warehouse.WarehouseInvoceModel();
            WarehouseInvoceModel.SetWarehouse(warehouseTables);
            Employees = MainViewModel.AdminModel.GetEmployeeMeneger(MainViewModel.Employee.Id);
            Client = new Data.Client(1);
            AgentInvoice = new View.Invoice.AgentInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = AgentInvoice;
            Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            CreateInvoiceBase = ReactiveCommand.Create(() =>
            {
               
            });
         
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()), MainViewModel.Employee);
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            invoiceGood.Show();
        }
        public ReactiveCommand<Unit, Unit> UpdateBooking => ReactiveCommand.Create(() => {
            
        
        });
        public InvoiceWinViewModel(Data.Invoice invoice)
        {
            SelectNewClient = ReactiveCommand.Create(() => {

                ClientViewModel clientView = new ClientViewModel(Invoice);

            });
            IsEdit = true;
            Invoice = invoice;
            IsInvoice = Invoice.IsInvoice;
            this.WarehouseInvoceModel = new Model.Warehouse.WarehouseInvoceModel();
            this.MarkModel = MainViewModel._markModel;
            CreateInvoiceBase = ReactiveCommand.Create(() =>
             {
                 
                 WarehouseInvoceModel.UpdateInvoice(Invoice);
             });
            try
            {
                this.WhenAnyValue(vm => vm.Invoice.Client.Model.MarkId).WhereNotNull().Subscribe(x => UpdateModels(x));
            }
            catch { }
            CreateInvoice = new View.Invoice.CreateInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = InvoiceTable;
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            //this.WhenAnyValue(vm =>ViewModel.MainViewModel.AdminModel.Cities.Count).Subscribe(x=> UpdateCity());
            invoiceGood.Show();
        }
        private void UpdateCity()
        {
            Cities = MainViewModel.AdminModel.GetCitiesFromText("");
        }
        private void UpdateModels(int mark_id)
        {
            
            Models=MarkModel.GetModelFromMarkId(mark_id);
        }
        private void UpdateModelsNew(int mark_id)
        {

            Models = MarkModel.GetModelFromMarkId(mark_id);
            Client.ModelId = 0;
        }
        public ReactiveCommand<Unit, Unit> SelectNewClient { get; set; }
        public ReactiveCommand<string, Unit> CreateInvoiceCommercial=>ReactiveCommand.Create<string>(CreateInvoiceCommercialCommand);
        private void CreateInvoiceCommercialCommand(string inv_com)
        {
            if(IsNewClient)
            {
                var city=MainViewModel.AdminModel.GetCityFromName(Client.CityName);
                if(city==null)
                {
                    city = new Data.City { Name = Client.CityName };
                }
                Client.City=city;
                Invoice.Client = Client;
            }
            if(inv_com=="Invoice")
            {
                Invoice.IsInvoice = true;
            }
            if(inv_com=="Commercial")
            {
                Invoice.IsInvoice = false;
            }
            MainControl = InvoiceTable;
        }
        public ReactiveCommand<Unit, Unit> BackInvoice => ReactiveCommand.Create(() => {
            MainControl=CreateInvoice;
        
        });
        private ReactiveCommand<Unit, Unit> _create_invoice_base;
        public ReactiveCommand<Unit, Unit> CreateInvoiceBase
        {
            get => _create_invoice_base;
            set => this.RaiseAndSetIfChanged(ref _create_invoice_base, value);
        }
        public ReactiveCommand<Unit, Unit> AddNewGoods=>ReactiveCommand.Create(()=>{
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,0);
        
        });
        public ReactiveCommand<Unit, Unit> AddVirtualGoods => ReactiveCommand.Create(() => {
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,1);

        });
        public ReactiveCommand<Unit, Unit> AddNewVirtualGoods => ReactiveCommand.Create(() => {
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,2);

        });
        public ReactiveCommand<Unit, Unit> CreateExcel => ReactiveCommand.Create(() => { WarehouseInvoceModel.CreateExcelFile(Invoice); });

        }
}