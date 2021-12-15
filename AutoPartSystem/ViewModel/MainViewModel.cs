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
   public class MainViewModel:ReactiveObject
    {
        public static AdminViewModel? AdminViewModel { get; set; }
        public static WarehouseViewModel? WarehouseViewModel { get; set;}
        public static ClientViewModel? ClientViewModel { get; set; }
        public static InvoiceViewModel? InvoiceViewModel { get; set; }
        public static GetMoveGoodsViewModel? GetMoveGoodsViewModel { get; set; }
        public static Data.Employee? Employee { get;private set; }
        private Data.Employee _employee;
        public Data.Employee Employee2
        {
            get=>this._employee;
            set=>this.RaiseAndSetIfChanged(ref _employee, value);
        }
        public static Model.MarkModel.MarkModel? _markModel;
        public static Model.Admin.AdminModel AdminModel;
        public static Model.Client.ClientModel ClientModel;
        public static Model.Warehouse.WarehouseModel WarehouseModel;
        public static Model.Warehouse.MoveGoodsModel MoveGoodsModel;
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get=> _main_control;
            set=>this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<UserControl> _controls;
        private static MainViewModel main { get; set; }
        public static MainViewModel SelMain()
        {
            return main;
        }
        private bool is_close_cash;
        public bool IsCloseCash
        {
            get => is_close_cash;
            set => this.RaiseAndSetIfChanged(ref is_close_cash, value);
        }
        private bool is_not_close_cash;
        public bool IsNotCloseCash
        {
            get => is_not_close_cash;
            set => this.RaiseAndSetIfChanged(ref is_not_close_cash, value);
        }
        private Data.OpenCloseCash _open_cash;
        public Data.OpenCloseCash OpenCloseCash
        {
            get => _open_cash;
            set=>this.RaiseAndSetIfChanged(ref _open_cash, value);
        }
        public MainViewModel(Data.Employee employee)
        {
            main = this;
            Employee = employee;
            Employee2 = employee;
            _markModel=new Model.MarkModel.MarkModel();
            AdminModel= new Model.Admin.AdminModel();
            AdminModel.GetNowCash(Employee.Cash);
            var open_cash = AdminModel.GetOpenCash(Employee.Id);

            if(open_cash==null)
            {
                IsCloseCash = true;
                var not_close_cash=AdminModel.GetNotCloseCashs(Employee.Id);
                if (not_close_cash.Count != 0)
                {
                    foreach (var not in not_close_cash)
                    {
                        MessageBox.Show($"Необходимо закрыть кассу за {not.OpenDate.ToString("dd-MM-yyyy")}", "Внимание");
                        var Close_cash = new View.Admin.CloseCash(not);
                        Close_cash.Show();
                        IsNotCloseCash = true;
                    }
                }
                
                
            }
            else
            {
                IsCloseCash = false;
            }
            ClientModel=new Model.Client.ClientModel();
            AdminViewModel = new AdminViewModel(_markModel);
            WarehouseViewModel=new WarehouseViewModel(_markModel);
            ClientViewModel = new ClientViewModel(_markModel);
            InvoiceViewModel = new InvoiceViewModel();
            MoveGoodsModel=new Model.Warehouse.MoveGoodsModel();
            GetMoveGoodsViewModel=new GetMoveGoodsViewModel();
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.MainPage(), new View.Admin.MainAdminPage(), new View.Client.ClientPage(), new View.Invoice.InvoiceMainPage(), new View.MoveGoods.GetMoveGoods()};
            

        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            switch (page_id)
            {
                case "AddNewGoods":
                    MainControl = _controls[0];
                    WarehouseViewModel.AddNewWarehouseWinOpenCommand();
                    break;
                case "AddNewVirtualGoods":
                    MainControl = _controls[0];
                    WarehouseViewModel.AddNewWarehouseVirtualWinOpenCommand();
                    break;
                case "ZavSkladTable":
                    MainControl = _controls[0];
                    WarehouseViewModel.OpenPageCommand(page_id);
                    break;
                case "VirtualSkladTable":
                    MainControl = _controls[0];
                    WarehouseViewModel.OpenPageCommand(page_id);
                    break;
                case "AdminEmp":
                    MainControl = _controls[1];
                    AdminViewModel.OpenPageCommand(page_id);
                    break;
                case "AdminModel":
                    MainControl = _controls[1];
                    AdminViewModel.OpenPageCommand(page_id);
                    break;
                case "OpenClientTable":
                    MainControl = _controls[2];
                    ClientViewModel.OpenPageCommand(page_id);
                    break;
                case "OpenInvoiceComm":
                    MainControl = _controls[3];
                    InvoiceViewModel.OpenPageCommand(page_id);
                    break;
                case "OpenInvoice":
                    MainControl = _controls[3];
                    InvoiceViewModel.OpenPageCommand(page_id);
                    break;
                case "OpenMoveGoods":
                    MainControl = _controls[4];
                    InvoiceViewModel.OpenPageCommand(page_id);
                    break;
                case "AdminCity":
                    MainControl = _controls[1];
                    AdminViewModel.OpenPageCommand(page_id);
                    break;
                case "InsertCash":
                    View.Cash.InsertOutCash insertCash = new View.Cash.InsertOutCash(Employee2, "Добавить", AdminModel);
                    insertCash.Show();
                    break;
                case "OutCash":
                    View.Cash.InsertOutCash outCash = new View.Cash.InsertOutCash(Employee2, "Вывод", AdminModel);
                    outCash.Show();
                    break;
                case "Report":
                   View.Admin.Report report=new View.Admin.Report();
                   ReportViewModel reportViewModel = new ReportViewModel();
                    report.DataContext= reportViewModel;
                    report.Show();
                    break;
            }
            
        }
        public ReactiveCommand<string, Unit> OpenCloseCashCom => ReactiveCommand.Create<string>(OpenCloseCashCommand);
        private void OpenCloseCashCommand(string name)
        {
            switch (name)
            {
                case "Open":
                    break;
                case "Close":
                    break;
            }
        }
    }
}
