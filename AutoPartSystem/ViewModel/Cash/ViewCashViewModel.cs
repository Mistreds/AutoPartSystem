using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.ViewModel.Cash
{
    public class ViewCashViewModel:ReactiveObject
    {

        private bool _is_find_visible;
        public bool IsFindVisible
        {
            get => _is_find_visible;
            set=>this.RaiseAndSetIfChanged(ref _is_find_visible, value);
        }
        private Data.Employee Employee;
        private ObservableCollection<Data.Employee> _employers_table;
        public ObservableCollection<Data.Employee> EmployersTable
        {
            get => _employers_table;
            set => this.RaiseAndSetIfChanged(ref _employers_table, value);
        }
        private ObservableCollection<Data.InsertOutCash> _insert_out_cash;
        public ObservableCollection<Data.InsertOutCash> InsertOutCashe
        {
            get => _insert_out_cash;
            set=>this.RaiseAndSetIfChanged(ref _insert_out_cash, value);
        }
        private ObservableCollection<Data.Expenses> _expenses_table;
        public ObservableCollection<Data.Expenses> ExpensesTable
        {
            get => _expenses_table;
            set => this.RaiseAndSetIfChanged(ref _expenses_table, value);
        }
        private DateTime _in_date1;
        public DateTime InDate1
        {
            get => _in_date1;
            set=>this.RaiseAndSetIfChanged(ref _in_date1,value);
        }
        private DateTime _in_date2;
        public DateTime InDate2
        {
            get => _in_date2;
            set => this.RaiseAndSetIfChanged(ref _in_date2, value);
        }
        private DateTime _ex_date1;
        public DateTime ExDate1
        {
            get => _ex_date1;
            set => this.RaiseAndSetIfChanged(ref _ex_date1, value);
        }
        private DateTime _ex_date2;
        public DateTime ExDate2
        {
            get => _ex_date2;
            set => this.RaiseAndSetIfChanged(ref _ex_date2, value);
        }
        private int in_emp;
        public int InEmp
        {
            get => in_emp;
            set=>this.RaiseAndSetIfChanged(ref in_emp, value);
        }
        private int ex_emp;
        public int ExEmp
        {
            get => ex_emp;
            set => this.RaiseAndSetIfChanged(ref ex_emp, value);
        }
        public ViewCashViewModel(Data.Employee employee)
        {
            InDate1 = new DateTime(DateTime.Now.Year, 1, 1,0,0,0);
            InDate2 = new DateTime(DateTime.Now.Year, 12, 31, 23, 59,59);
            ExDate1 = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            ExDate2 = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
            Employee = employee;
            EmployersTable = MainViewModel.AdminModel.GetEmployeesForSelect();
            if (employee.IsAdmin==true)
            {
                IsFindVisible = true;
            }
        }
        public ReactiveCommand<Unit, Unit> SetFiltrInOut => ReactiveCommand.Create(() => {
            InsertOutCashe = MainViewModel.CashModel.GetInsertOutCashes(InEmp, InDate1, InDate2);
        });
        public ReactiveCommand<Unit, Unit> SetFiltrEx => ReactiveCommand.Create(() => {
            ExpensesTable = MainViewModel.CashModel.GetExpenses(ExEmp, InDate1, InDate2);
        });
        public void OpenPage(string type_id)
        {
            switch(type_id)
            {
                case "InOutCash":
                    var em_id = 0;
                    if(Employee.IsAdmin!=true)
                    {
                        em_id = Employee.Id;
                    }
                    InsertOutCashe = MainViewModel.CashModel.GetInsertOutCashes(em_id, InDate1, InDate2);
                    var in_out = new View.Cash.InOutCash();
                    in_out.DataContext = this;
                    in_out.Show();
                    break;
                case "Expoise":
                     em_id = 0;
                    if(Employee.IsAdmin!=true)
                    {
                        em_id = Employee.Id;
                    }
                    ExpensesTable = MainViewModel.CashModel.GetExpenses(em_id, InDate1, InDate2);
                    var ExpWin = new View.Cash.ExpoiseWin();
                    ExpWin.DataContext = this;
                    ExpWin.Show();
                    break;
            }
        }
    }
}
