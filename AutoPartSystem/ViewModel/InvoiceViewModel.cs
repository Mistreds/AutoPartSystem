using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class InvoiceViewModel:ReactiveObject
    {
        private ObservableCollection<UserControl> _control;
        private UserControl _main_user;
        public UserControl MainControl
        {
            get => _main_user;
            set => this.RaiseAndSetIfChanged(ref _main_user, value);
        }
        private ObservableCollection<Data.Invoice> _invoice_table;
        public ObservableCollection<Data.Invoice> InvoiceTable
        {
            get => _invoice_table;
            set=>this.RaiseAndSetIfChanged(ref _invoice_table, value);
        }
        private Model.InvoiceModel InvoiceModel;
        public InvoiceViewModel()
        {
            InvoiceModel = new Model.InvoiceModel();
            InvoiceTable = InvoiceModel.ComInvoice;
            _control = new ObservableCollection<UserControl> { new View.Invoice.InvoiceMainTab() };
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        public void OpenPageCommand(string command)
        {
            switch (command)
            {
                case "OpenInvoiceComm":
                    MainControl = _control[0];
                    InvoiceTable= InvoiceModel.UpdateComInvoiceFromDataBase(0);
                    break;
            }
        }
        public ReactiveCommand<Data.Invoice, Unit> OpenInvoiceInformation =>ReactiveCommand.Create<Data.Invoice>(OpenInvoiceInformationCommad);
        private void OpenInvoiceInformationCommad(Data.Invoice invoice)
        {
            InvoiceWinViewModel invoiceWinViewModel = new InvoiceWinViewModel(invoice);
        }
    }
}
