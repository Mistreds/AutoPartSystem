using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartSystem.ViewModel.Cash
{
    public class CashViewModel:ReactiveObject
    {

        private ObservableCollection<Data.Expenses> _expenses;
        public ObservableCollection<Data.Expenses> Expenses
        {
            get => _expenses;
            set=>this.RaiseAndSetIfChanged(ref _expenses, value);
        }
        private int expen;
        public int Expen
        {
            get=> this.expen;
            set => this.RaiseAndSetIfChanged(ref expen, value);
        }
        private string info_text;
        public string InfoText
        {
            get => info_text;
            set=>this.RaiseAndSetIfChanged(ref info_text, value);
        }
        private Data.OpenCloseCash openClose;
        private int cash;
        private int cash_exp;
        private Data.Employee employee;
        private View.Cash.ExpenciveWindow ExpenciveWindow;
        public List<Data.TypePay> TypePay { get; private set; }
        public List<Data.TypeExpenses> TypeExpenses { get;private set; }
        public CashViewModel(Data.OpenCloseCash openClose, int cash, Data.Employee employee)
        {
            TypeExpenses = MainViewModel.CashModel.GetExpenses();
            this.cash = cash;
            this.openClose = openClose;
            this.employee = employee;
            cash_exp = employee.Cash - cash; ;
            Expen = cash_exp;
            InfoText = "Необходимо указать недосдачу";
            Expenses = new ObservableCollection<Data.Expenses>();
            ExpenciveWindow = new View.Cash.ExpenciveWindow();
           
            this.WhenAnyValue(vm => vm.Expenses.Count).Subscribe(_ => UpdateExpen1());
            SaveExpensive = ReactiveCommand.Create(() => {

                DateTime date = DateTime.Now;
                if (Expenses.Count == 0)
                {
                    MessageBox.Show("Не выбраны расходы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var ff = Expenses.Where(p => p.TypeExpenses == null).FirstOrDefault();
                if (ff != null)
                {
                    MessageBox.Show("Не выбран тип расходов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var expense in Expenses)
                {
                   
                    expense.TypePayId = 1;
                    expense.TypeExpensesId = expense.TypeExpenses.Id;
                    expense.TypeExpenses = null;
                    expense.EmployeeId = employee.Id;
                    expense.Date = date;
                    expense.IsOpenDay = true;
                }
                MainViewModel.CashModel.SaveExpencesWithOpenCash(Expenses, openClose);
                ExpenciveWindow.Close();
            });
            ExpenciveWindow.DataContext = this;
            ExpenciveWindow.Show();
        }
        private CloseCashViewModel CloseCashViewModel;
        public CashViewModel(CloseCashViewModel closeCashViewModel)
        {
            TypeExpenses = MainViewModel.CashModel.GetExpenses();
            CloseCashViewModel = closeCashViewModel;
            this.cash = CloseCashViewModel.CloseCash;
            this.employee = MainViewModel.Employee;
            cash_exp = CloseCashViewModel.Cash - cash;
            Expen = cash_exp;
            InfoText = "Необходимо указать недосдачу";
            Expenses = new ObservableCollection<Data.Expenses>();
            ExpenciveWindow = new View.Cash.ExpenciveWindow();
    
            this.WhenAnyValue(vm => vm.Expenses.Count).Subscribe(_ => UpdateExpen1());
            SaveExpensive = ReactiveCommand.Create(() => {

                DateTime date = DateTime.Now;
                if (Expenses.Count == 0)
                {
                    MessageBox.Show("Не выбраны расходы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var ff = Expenses.Where(p => p.TypeExpenses == null).FirstOrDefault();
                if(ff!=null)
                {
                    MessageBox.Show("Не выбран тип расходов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var expense in Expenses)
                {
                   
                    Console.WriteLine(expense.TypeExpenses.Id);
                    expense.TypePayId = 1;
                    expense.TypeExpensesId = expense.TypeExpenses.Id;
                    expense.TypeExpenses = null;
                    expense.EmployeeId = employee.Id;
                    expense.Date = date;
                }
                CloseCashViewModel.Expenses = Expenses;
                ExpenciveWindow.Close();
            });
            ExpenciveWindow.DataContext = this;
            ExpenciveWindow.Show();
        }
        private void UpdateExpen1()
        {
            foreach(var expense in Expenses)
            {
                expense.WhenAnyValue(vm => vm.Cash).Subscribe(_ => UpdateExpen());
            }
        }
        private void UpdateExpen()
        {
           
           Expen= cash_exp - Expenses.Sum(p => p.Cash);
           if(Expen>0)
            {
                InfoText = "Необходимо указать расходы";
            }
           else
            {
                if(Expen<0)
                {
                    InfoText = "Расходы не могут быть больше недостачи";
                }
                else
                {
                    InfoText = "Расходы совпадают с недосдачей";
                }
            }
        }
        public ReactiveCommand<Unit,Unit> SaveExpensive { get; set; }
    }
}
