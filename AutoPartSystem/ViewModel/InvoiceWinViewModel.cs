using System;
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
        private List<Data.City> _cities;
        public List<Data.City> Cities
        {
            get => _cities;
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
        private View.Invoice.InvoceTable InvoiceTable;
        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel, Model.MarkModel.MarkModel MarkModel)
        {
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            this.MarkModel = MarkModel;
            Mark = MarkModel.GetMark();
            CreateInvoice = new View.Invoice.CreateInvoice();
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = CreateInvoice;
            _cities=MainViewModel.AdminModel.GetCities();
            CreateInvoiceBase = ReactiveCommand.Create(() =>
             {
                 WarehouseInvoceModel.AddInvoiceToDataBase(Invoice);
             });
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()),MainViewModel.Employee);
            try
            {
                this.WhenAnyValue(vm => vm.Invoice.Client.Model.MarkId).WhereNotNull().Subscribe(x => UpdateModels(x));
            }
            catch { }
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            invoiceGood.Show();
           
        }
        public InvoiceWinViewModel(Data.Invoice invoice)
        {
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
            CreateInvoice = new View.Invoice.CreateInvoice();
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = InvoiceTable;
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            invoiceGood.Show();
            

        }
        private void UpdateModels(int mark_id)
        {
            Models=MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<Unit, Unit> SelectNewClient => ReactiveCommand.Create(() => {

            View.Client.ClientWindow clientWindow = new View.Client.ClientWindow(MainViewModel.ClientModel.GetClient(), Invoice);
            clientWindow.Show();
        });
        public ReactiveCommand<string, Unit> CreateInvoiceCommercial=>ReactiveCommand.Create<string>(CreateInvoiceCommercialCommand);
        private void CreateInvoiceCommercialCommand(string inv_com)
        {
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
       
    }
}
