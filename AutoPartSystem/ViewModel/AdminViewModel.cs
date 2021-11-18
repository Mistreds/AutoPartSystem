using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoPartSystem.ViewModel
{
    public class AdminViewModel:ReactiveObject
    {
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private bool _is_add;
        public bool IsAdd
        {
            get => _is_add;
            set
            {
                if(value)
                    MainControl = _controls[1];
                else
                    MainControl = _controls[0];
                this.RaiseAndSetIfChanged(ref _is_add, value);
            }
        }
        private List<Data.Position> _positions;
        public List<Data.Position> Positions
        {
            get=> _positions;
        }
        private ObservableCollection<Data.Employee> _employers_table;
        public ObservableCollection<Data.Employee> EmployersTable
        {
            get => _employers_table;
            set => this.RaiseAndSetIfChanged(ref _employers_table, value); 
        }
        private readonly ObservableCollection<UserControl> _controls;
        private Model.Admin.AdminModel AdminModel;
        private Data.Employee add_employee;
        public Data.Employee AddEmployee
        {
            get => add_employee;
            set=>this.RaiseAndSetIfChanged(ref add_employee, value);
        }
        public AdminViewModel()
        {
            AdminModel = new Model.Admin.AdminModel();
            _positions = AdminModel.GetPositions();
            EmployersTable = AdminModel.GetEmployees();
            _controls = new ObservableCollection<UserControl> { new View.Admin.AdminTable(), new View.Admin.AddUser() };
            MainControl = _controls[0];
            AddEmployee = new Data.Employee();
        }
        public ReactiveCommand<Unit, Unit> AddEmmployeeCommand => ReactiveCommand.Create(() => {

            EmployersTable = AdminModel.AddEmployers(AddEmployee);
        });
        public ReactiveCommand<Unit,Unit> AddOrTable => ReactiveCommand.Create(() => { IsAdd = !IsAdd; });
    }
}
