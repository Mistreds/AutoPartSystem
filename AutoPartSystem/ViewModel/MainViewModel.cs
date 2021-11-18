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
        public static AdminViewModel AdminViewModel { get; set; }
        private Data.Employee? _employee;
        public Data.Employee? Employee
        {
            get => _employee;
            set=>this.RaiseAndSetIfChanged(ref _employee, value);

        }
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
            
            _controls = new ObservableCollection<UserControl> { new View.Admin.MainAdmin() };

        }
        public ReactiveCommand<Unit, Unit> OpenAdminMain => ReactiveCommand.Create(() => { MainControl = _controls[0]; });
    }
}
