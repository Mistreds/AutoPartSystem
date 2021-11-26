using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
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
        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel, Model.MarkModel.MarkModel MarkModel)
        {
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            this.MarkModel = MarkModel;
            Mark = MarkModel.GetMark();
            
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()),MainViewModel.Employee);
            this.WhenAnyValue(vm => vm.Invoice.Client.Mark).Subscribe(x => UpdateModels(x.Id));
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            invoiceGood.Show();
        }
        private void UpdateModels(int mark_id)
        {
            Models=MarkModel.GetModelFromMarkId(mark_id);
        }
    }
}
