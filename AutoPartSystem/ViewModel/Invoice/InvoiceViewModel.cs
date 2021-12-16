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
        public class FindInvoice:ReactiveObject
        {
            private int _id;
            public int Id
            {
                get => _id;
                set=>this.RaiseAndSetIfChanged(ref _id, value);
            }
            private string _fio;
            public string Fio
            {
                get => _fio;
                set => this.RaiseAndSetIfChanged(ref _fio, value);
            }
            private string phone;
            public string Phone
            {
                get => phone;
                set => this.RaiseAndSetIfChanged(ref phone, value);
            }
            private int _emp_id;

            public int EmpId
            {
                get => _emp_id;
                set => this.RaiseAndSetIfChanged(ref _emp_id, value);
            }
            private DateTime _date_1;
            public DateTime Date1
            {
                get => _date_1;
                set => this.RaiseAndSetIfChanged(ref _date_1, value);

            }
            private DateTime _date_2;
            public DateTime Date2
            {
                get => _date_2;
                set => this.RaiseAndSetIfChanged(ref _date_2, value);

            }
            public FindInvoice()
            {
                Date1 = new DateTime(DateTime.Now.Year, 1,1);
                Date2= new DateTime(DateTime.Now.Year, 12, 31);
                Phone = "";
                Fio = "";
            }
        }
        private FindInvoice _find_invoice;
        public FindInvoice FindInvoices
        {
            get => _find_invoice;
            set=>this.RaiseAndSetIfChanged(ref _find_invoice, value);
        }
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
        private ObservableCollection<Data.Employee>? _employers_table;
        public ObservableCollection<Data.Employee>? EmployersTable
        {
            get => _employers_table;
            set => this.RaiseAndSetIfChanged(ref _employers_table, value);
        }
        private Model.InvoiceModel InvoiceModel;
        private string _invoice_string;
        public string InvoiceString
        {
            get => _invoice_string;
            set=>this.RaiseAndSetIfChanged(ref _invoice_string, value);
        }

        private bool is_invoice;
        public InvoiceViewModel()
        {
            InvoiceModel = new Model.InvoiceModel();
            InvoiceTable = InvoiceModel.ComInvoice;
            EmployersTable = MainViewModel.AdminModel.GetEmployeesForSelect();
            _control = new ObservableCollection<UserControl> { new View.Invoice.InvoiceMainTab() };
        }

    #region  Command
        
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        public void OpenPageCommand(string command)
        {
            switch (command)
            {
                case "OpenInvoiceComm":
                    MainControl = _control[0];
                    is_invoice = false;
                    InvoiceTable= InvoiceModel.SelectComInvoiceFromDataBase(0);
                    InvoiceString = "Коммерческое предложения";
                    FindInvoices = new FindInvoice();
                    break;
                case "OpenInvoice":
                    MainControl = _control[0];
                    is_invoice = true;
                    InvoiceTable = InvoiceModel.SelectInvoiceFromDataBase(0);
                    InvoiceString = "Накладные";
                    FindInvoices = new FindInvoice();
                    break;
            }
        }
        public ReactiveCommand<Data.Invoice, Unit> OpenInvoiceInformation =>ReactiveCommand.Create<Data.Invoice>(OpenInvoiceInformationCommad);
        private void OpenInvoiceInformationCommad(Data.Invoice invoice)
        {
            InvoiceWinViewModel invoiceWinViewModel = new InvoiceWinViewModel(invoice);
        }

        public ReactiveCommand<Unit, Unit> FindInvoiceCommand => ReactiveCommand.Create(() =>
        {

            if (MainViewModel.Employee != null && MainViewModel.Employee.PositionId!=1)
            {
                FindInvoices.EmpId = MainViewModel.Employee.Id;
               
            }
            InvoiceTable=InvoiceModel.FindInvoice(FindInvoices, is_invoice);
           
        });

        #endregion
    }
}
