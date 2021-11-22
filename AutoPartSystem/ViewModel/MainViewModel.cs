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
        public static Data.Employee? Employee { get;private set; }
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
            if(Employee.PositionId==1)
            {
                AdminViewModel = new AdminViewModel();
            }
            WarehouseViewModel=new WarehouseViewModel();
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.MainPage(), new View.Admin.MainAdminPage() };

        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            MainControl = _controls[Convert.ToInt32(page_id)];
        }
    }
}
