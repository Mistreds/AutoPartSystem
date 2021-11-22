using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    
    public class InvoiceWinViewModel:ReactiveObject
    {
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        private Data.Invoice invoice;
        public Data.Invoice Invoice
        {
            get=> invoice;
            set=>this.RaiseAndSetIfChanged(ref invoice, value);
        }
        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel)
        {
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            Invoice= new Data.Invoice(WarehouseInvoceModel.GetWarehouse(),MainViewModel.Employee);
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            invoiceGood.Show();
        }
    }
}
