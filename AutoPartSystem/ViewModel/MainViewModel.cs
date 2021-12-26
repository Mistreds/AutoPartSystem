using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using AutoPartSystem.Model;
namespace AutoPartSystem.ViewModel
{
   public class MainViewModel:ReactiveObject
    {
        public static AdminViewModel AdminViewModel { get; set; }
        public static WarehouseViewModel WarehouseViewModel { get; set;}
        public static ClientViewModel ClientViewModel { get; set; }
        public static InvoiceViewModel InvoiceViewModel { get; set; }
        public static GetMoveGoodsViewModel GetMoveGoodsViewModel { get; set; }
        public static Cash.ViewCashViewModel ViewCashViewModel { get; set; }
        public static Data.Employee Employee { get;private set; }
        private Data.Employee _employee;
        public Data.Employee Employee2
        {
            get=>this._employee;
            set=>this.RaiseAndSetIfChanged(ref _employee, value);
        }
        public static Model.MarkModel.MarkModel _markModel;
        public static Model.Admin.AdminModel AdminModel;
        public static Model.Client.ClientModel ClientModel;
        public static Model.Warehouse.WarehouseModel WarehouseModel;
        public static Model.Warehouse.MoveGoodsModel MoveGoodsModel;
        public static Model.CashModel CashModel;
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
        private int _status;
        public int Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        private Data.OpenCloseCash _open_cash;
        public Data.OpenCloseCash OpenCloseCash
        {
            get => _open_cash;
            set=>this.RaiseAndSetIfChanged(ref _open_cash, value);
        }
        public static int CityId { get;private set; }
        public static int PositId { get;private set; }
        private bool _is_block_interface;
        public bool IsBlockInterface
        {
            get => this._is_block_interface;
            set => this.RaiseAndSetIfChanged(ref _is_block_interface, value);
        }
        public MainViewModel(Data.Employee employee)
        {
            main = this;
            
            Employee = employee;
            Employee2 = employee;
            CityId=employee.CityId;
            PositId = employee.PositionId;
            if(PositId==4)
            {
                PositId = 3;
            }
            _markModel =new Model.MarkModel.MarkModel();
            AdminModel= new Model.Admin.AdminModel();
            CashModel = new Model.CashModel(employee);
            if (Employee.SetCell && !Employee.IsAdmin)
            {
                AdminModel.GetNowCash(Employee.Cash);
                
                Status = CashModel.GetStatus();
                if(Status == 0)
                {
                    IsBlockInterface = false;
                    IsCloseCash = true;
                    IsNotCloseCash = false;
                }
                if(Status == 1)
                {
                    OpenCloseCash = CashModel.GetCashDay();
                    IsBlockInterface = true;
                }
                if(Status == 2)
                {
                    OpenCloseCash = CashModel.GetCashDay();
                    IsBlockInterface = true;
                    MessageBox.Show("Касса на сегодня закрыта, если необходимо продолжить работу, обратитесь к администратору","Внимание",MessageBoxButton.OK,MessageBoxImage.Information) ;
                    IsBlockInterface = false;
                }
                if(Status == 3)
                {
                    OpenCloseCash = CashModel.GetCashDay();
                    MessageBox.Show("Во время предыдущей работы касса была не закрыта", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsBlockInterface = false;
                    IsNotCloseCash =true;
                }
            }
            else
            {
                IsBlockInterface = true;   
            }
            ClientModel =new Model.Client.ClientModel();
            AdminViewModel = new AdminViewModel(_markModel);
            InvoiceViewModel = new InvoiceViewModel();
            WarehouseViewModel =new WarehouseViewModel(_markModel);
            ClientViewModel = new ClientViewModel(_markModel);
           
            MoveGoodsModel=new Model.Warehouse.MoveGoodsModel();
            GetMoveGoodsViewModel=new GetMoveGoodsViewModel();
            ViewCashViewModel = new Cash.ViewCashViewModel(employee);
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.MainPage(), new View.Admin.MainAdminPage(), new View.Client.ClientPage(), new View.Invoice.InvoiceMainPage(), new View.MoveGoods.GetMoveGoods()};
            IsCloseCash = false;
            if (!IsCloseCash)
            OpenPageCommand("ZavSkladTable");
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
                case "OpenAgentClientTable":
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
                case "OpenBooking":
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
                case "InOutCash":
                    ViewCashViewModel.OpenPage(page_id);
                    break;
                case "Expoise":
                    ViewCashViewModel.OpenPage(page_id);
                    break;

            }
            
        }
        public void UpdateOpenCash()
        {
            Status = CashModel.GetStatus();
            OpenCloseCash = CashModel.GetCashDay();
            if(Status==0)
            {
                IsBlockInterface = false;
                IsCloseCash = true;
                IsNotCloseCash = false;
            }
            if(Status==1)
            {
                IsBlockInterface = true;
            }
            if (Status == 2)
            {
                IsBlockInterface = false;
            }
        }
        public ReactiveCommand<string, Unit> OpenCloseCashCom => ReactiveCommand.Create<string>(OpenCloseCashCommand);
        private void OpenCloseCashCommand(string name)
        {
            switch (name)
            {
                case "Open":
                    View.Cash.OpenCash openCash= new View.Cash.OpenCash();
                    openCash.Show();
                    break;
                case "Close":
                    ViewModel.Cash.CloseCashViewModel closeCash = new Cash.CloseCashViewModel(OpenCloseCash);
                    break;
            }
        }
        public ReactiveCommand<Unit, Unit> CheckUpdate => ReactiveCommand.Create(CheckUpdates.CheckUpdate);
    }
}
