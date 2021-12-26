using Microsoft.EntityFrameworkCore;
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
    public class CloseCashViewModel : ReactiveObject
    {
        private int _close_cash;
        public int CloseCash
        {
            get => _close_cash;
            set
            {
                this.RaiseAndSetIfChanged(ref _close_cash, value); UpdateNewCash();
                if (value == Cash)
                {
                    IsNeedExpence = false;
                }
                if(value<Cash)
                {
                    IsNeedExpence=true;
                    Expenses = new ObservableCollection<Data.Expenses>();
                }
                
            }
        }
        private int _cash;
        public int Cash
        {
            get => _cash;
            set => this.RaiseAndSetIfChanged(ref _cash, value);
        }
        private int new_cash;
        public int NewCash
        {
            get => new_cash;
            set => this.RaiseAndSetIfChanged(ref new_cash, value);
        }
        private int cash_to_buch;
        public int CashToBuch
        {
            get => cash_to_buch;
            set { this.RaiseAndSetIfChanged(ref cash_to_buch, value); UpdateNewCash(); }
        }
        private int _marz;
        public int Marz
        {
            get => _marz;
            set
            {
                this.RaiseAndSetIfChanged(ref _marz, value);
                UpdateNewCash();
            }
        }
        private void UpdateNewCash()
        {
            NewCash = CloseCash - Marz - CashToBuch;
        }
        private bool is_need_expence;
        public bool IsNeedExpence
        {
            get => is_need_expence;
            set => this.RaiseAndSetIfChanged(ref is_need_expence, value);
        }
        public int RecMarz { get; set; }
        private Data.OpenCloseCash _open_close_cash;
        public Data.OpenCloseCash OpenCloseCash
        {
            get => _open_close_cash;
            set => this.RaiseAndSetIfChanged(ref _open_close_cash, value);
        }
        public DateTime date { get; set; }
        private View.Cash.CloseCash CloseCashView;
        private ObservableCollection<Data.Expenses> _expenses;
        public ObservableCollection<Data.Expenses> Expenses
        {
            get => _expenses;
            set => this.RaiseAndSetIfChanged(ref _expenses, value);
        }
        private int input_cash;
        public CloseCashViewModel(Data.OpenCloseCash OpenCloseCash)
        {
            date = DateTime.Now;
            CloseCashView = new View.Cash.CloseCash();
            this.OpenCloseCash = OpenCloseCash;
            using var db = new Data.ConDB();
            RecMarz = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= OpenCloseCash.OpenDate && p.Date <= date && p.IsEnd == false && p.IsInvoice && p.EmployeeId == MainViewModel.Employee.Id).Sum(p => p.AllMarz);
            Marz = RecMarz;
            Cash = MainViewModel.Employee.Cash;
            CloseCash = Cash;
            CloseCashView.DataContext = this;
            CloseCashView.Show();

        }
        public ReactiveCommand<Unit, Unit> OpenExpencive => ReactiveCommand.Create(() => {
            CashViewModel cashViewModel = new CashViewModel(this);
        });
        public ReactiveCommand<Unit, Unit> CloseCashCommand => ReactiveCommand.Create(() => {

            if(NewCash<0)
            {
                MessageBox.Show("Касса не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(Marz>RecMarz)
            {
                MessageBox.Show("Нельзя забрать маржу больше рекомендованного", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (IsNeedExpence == false)
            {
                OpenCloseCash.CloseCash = NewCash;
                OpenCloseCash.Marz = Marz;
                OpenCloseCash.Status = 2;
                if (MainViewModel.SelMain().Status == 3)
                {
                    OpenCloseCash.Status = 3;
                }
                if (Marz != 0)
                    MainViewModel.CashModel.UpdateCash(Marz, "", MainViewModel.Employee, "Снятая маржа");
                if (CashToBuch!=0)
                MainViewModel.CashModel.UpdateCash(CashToBuch, "" , MainViewModel.Employee, "Передача бухгалтеру");
              
                if(Marz<RecMarz)
                {
                    MainViewModel.CashModel.AddNewExpenses(new Data.Expenses { Cash=Marz-RecMarz, Date=DateTime.Now, Name="Съем маржи со счета", TypeExpensesId=4, TypePayId=2, EmployeeId=MainViewModel.Employee.Id,   });
                }
                date = DateTime.Now;
                OpenCloseCash.CloseData = date;
                MainViewModel.CashModel.SaveOpenCash(OpenCloseCash);
                CloseCashView.Close();
            }
            else
            {
                if(Expenses.Count==0)
                {
                    MessageBox.Show("Не выбранны расходы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                OpenCloseCash.CloseCash = NewCash;
                OpenCloseCash.Marz = Marz;
                OpenCloseCash.Status = 2;
                OpenCloseCash.Shortage = Cash - CloseCash;
                if (MainViewModel.SelMain().Status == 3)
                {
                    OpenCloseCash.Status = 3;
                }
                if (CashToBuch != 0)
                    MainViewModel.CashModel.UpdateCash(CashToBuch, "", MainViewModel.Employee, "Передача бухгалтеру");
                if (Marz != 0)
                    MainViewModel.CashModel.UpdateCash(Marz, "", MainViewModel.Employee, "Снятая маржа");
                if (Marz < RecMarz)
                {
                    MainViewModel.CashModel.AddNewExpenses(new Data.Expenses { Cash = Marz - RecMarz, Date = DateTime.Now, Name = "Съем маржи со счета", TypeExpensesId = 4, TypePayId = 2, EmployeeId = MainViewModel.Employee.Id, });
                }
                MainViewModel.CashModel.SaveExpencesWithCloseCash(Expenses,OpenCloseCash);
                CloseCashView.Close();
            }
        
        });
    }
}
