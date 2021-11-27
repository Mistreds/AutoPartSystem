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
   public class MainViewModel:ReactiveObject
    {
        public static AdminViewModel? AdminViewModel { get; set; }
        public static WarehouseViewModel? WarehouseViewModel { get; set;}
        public static ClientViewModel? ClientViewModel { get; set; }
        public static Data.Employee? Employee { get;private set; }
        private Model.MarkModel.MarkModel? _markModel;
        public static Model.Admin.AdminModel AdminModel;
        public static Model.Client.ClientModel ClientModel;
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get=> _main_control;
            set=>this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<UserControl> _controls;
        public MainViewModel(Data.Employee employee)
        {
            Employee = employee;
            _markModel=new Model.MarkModel.MarkModel();
            AdminModel= new Model.Admin.AdminModel();
            ClientModel=new Model.Client.ClientModel();
            if (Employee.PositionId==1)
            {
                AdminViewModel = new AdminViewModel(_markModel);
            }
            WarehouseViewModel=new WarehouseViewModel(_markModel);
            ClientViewModel = new ClientViewModel(_markModel);
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.MainPage(), new View.Admin.MainAdminPage(), new View.Client.ClientPage() };

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
                case "ZavSkladTable":
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
            }
            
        }
    }
}
