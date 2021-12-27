using Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.Model
{

    public interface ICashModel
    {
        public int GetStatus();
        public Data.OpenCloseCash GetCashDay();
        public void UpdateCash(int Cash, string name, Employee employee, string type);
        public void OpenCash(int cash);
        public List<Data.TypeExpenses> GetExpenses();
        public void SaveExpencesWithOpenCash(ObservableCollection<Data.Expenses> expenses, Data.OpenCloseCash openCloseCash );
        public void SaveExpencesWithCloseCash(ObservableCollection<Data.Expenses> expenses, Data.OpenCloseCash openCloseCash);
        public void SaveOpenCash(Data.OpenCloseCash openCloseCash);
        public void AddNewExpenses(Data.Expenses expenses);
        public ObservableCollection<Data.InsertOutCash> GetInsertOutCashes(int emp_id, DateTime date1, DateTime date2);
        public ObservableCollection<Data.Expenses> GetExpenses(int emp_id, DateTime date1, DateTime date2);
    }
    public class CashModel:ReactiveObject,ICashModel
    {
        /// <summary>
        /// 0 - касса сегодня не открывалась, ранее была закрыта
        /// 1 - касса открыта на данный момент
        /// 2 - касса на сегодня уже закрыта
        /// 3 - касса ранее не была закрыта
        /// 4 - касса сегодня закрыта, но можно открыть
        /// </summary>
        private int status;
        private Data.OpenCloseCash openCloseCash;
        private Data.Employee Employee;
        public CashModel(Data.Employee employee)
        {
            status = 0;
            Employee = employee;
            Init();
        }
        private void Init()
        {
            status = 0;
            GetIsNotCloseCash();
            if (status != 0) return;
            GetCashDayFromDb();
            if (status !=0) return;
            GetCloseCashDayFromDb();
        }
        private void GetIsNotCloseCash()
        {
            using var db = new Data.ConDB();
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            openCloseCash = db.OpenCloseCash.Where(p => p.EmployeeId == Employee.Id && p.Status==1 && p.OpenDate<day_start  ).OrderByDescending(p=>p.Id).FirstOrDefault();
            if(openCloseCash != null)
            {
                status = 3;
            }
        }
        private void GetCashDayFromDb()
        {
            using var db= new Data.ConDB();
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var day_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            openCloseCash = db.OpenCloseCash.Where(p => p.EmployeeId == Employee.Id && p.OpenDate >= day_start && p.OpenDate <= day_end && p.Status==1).OrderByDescending(p=>p.Id).FirstOrDefault();
            if(openCloseCash!=null)
            {
                status = 1;
            }
            else
            {
                status=0;
            }
        }
        private void GetCloseCashDayFromDb()
        {
            using var db = new Data.ConDB();
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var day_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            openCloseCash = db.OpenCloseCash.Where(p => p.EmployeeId == Employee.Id && p.CloseData>= day_start && p.CloseData <= day_end && p.Status == 2).OrderByDescending(p => p.Id).FirstOrDefault();
            if(openCloseCash != null)
            {
                status = 2;
            }
            if(Employee.IsOpenCash)
            {
                status = 4;
            }

        }

        public int GetStatus()
        {
            return status;
        }

        public OpenCloseCash GetCashDay()
        {
            return openCloseCash;
        }
        public void UpdateCash(int Cash, string name, Employee employee, string type)
        {
            int cash = 0;
            if (type == "Добавить" || type == "Оплата товара" || type=="Добавление в кассу")
            {
                cash += Cash;
            }
            else
            {
                cash -= Cash;
            }
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            var day_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var inv = new Model.InvoiceModel();
            using var db = new Data.ConDB();
            db.InsertOutCash.Add(new Data.InsertOutCash(name, employee.Cash, cash, employee.Cash + cash, type, employee.Id));
            db.SaveChanges();
            employee.Cash = employee.Cash + cash;
            if (ViewModel.MainViewModel.Employee.Id == employee.Id)
                ViewModel.MainViewModel.Employee.Cash = employee.Cash;
            db.Update(employee);
            db.SaveChanges();
        }

        public void OpenCash(int cash)
        {
            openCloseCash = new Data.OpenCloseCash(Employee.Id, cash);
            using var db = new Data.ConDB();
            if(ViewModel.MainViewModel.Employee.Cash != cash)
            {
                if(cash > ViewModel.MainViewModel.Employee.Cash)
                {
                    UpdateCash(cash - ViewModel.MainViewModel.Employee.Cash, "Добавление в кассу при открытии", ViewModel.MainViewModel.Employee, "Добавление в кассу");

                    db.Update(openCloseCash);
                    db.SaveChanges();
                    Init();
                    ViewModel.MainViewModel.SelMain().UpdateOpenCash();
                }
                else
                {
                    ViewModel.Cash.CashViewModel cashViewModel = new ViewModel.Cash.CashViewModel(openCloseCash, cash, ViewModel.MainViewModel.Employee);
                }
            }
            else
            {
                db.Update(openCloseCash);
                db.SaveChanges();
                Init();
                ViewModel.MainViewModel.SelMain().UpdateOpenCash();
            }
            
        }

        public List<TypeExpenses> GetExpenses()
        {
            using var db= new Data.ConDB();
            return db.TypeExpenses.Where(p=>p.Id!=5).ToList();
        }

        public void SaveExpencesWithOpenCash(ObservableCollection<Expenses> expenses, OpenCloseCash openCloseCash)
        {
            using var db = new Data.ConDB();
            db.Expenses.AddRange(expenses);
            db.OpenCloseCash.Update(openCloseCash);
            db.SaveChanges();
            Init();
            ViewModel.MainViewModel.Employee.Cash = openCloseCash.OpenCash;
            ViewModel.MainViewModel.SelMain().Employee2.Cash = ViewModel.MainViewModel.Employee.Cash;
            db.Update(ViewModel.MainViewModel.Employee);
            db.SaveChanges();
            ViewModel.MainViewModel.SelMain().UpdateOpenCash();
        }

        public void SaveOpenCash(OpenCloseCash openCloseCash)
        {
            using var db = new Data.ConDB();
            db.Update(openCloseCash);
            db.SaveChanges();
            Init();
            ViewModel.MainViewModel.SelMain().UpdateOpenCash();
        }

        public void SaveExpencesWithCloseCash(ObservableCollection<Expenses> expenses, OpenCloseCash openCloseCash)
        {
            using var db = new Data.ConDB();
            openCloseCash.CloseData = DateTime.Now;
            db.Expenses.AddRange(expenses);
            db.OpenCloseCash.Update(openCloseCash);
            db.SaveChanges();
            Init();
            ViewModel.MainViewModel.Employee.Cash = openCloseCash.CloseCash;
            ViewModel.MainViewModel.SelMain().Employee2.Cash = ViewModel.MainViewModel.Employee.Cash;
            db.Update(ViewModel.MainViewModel.Employee);
            db.SaveChanges();
            ViewModel.MainViewModel.SelMain().UpdateOpenCash();
        }

        public void AddNewExpenses(Expenses expenses)
        {
            using var db=new Data.ConDB();
            db.Expenses.Add(expenses);
            db.SaveChanges();
        }

        public ObservableCollection<InsertOutCash> GetInsertOutCashes(int emp_id, DateTime date1, DateTime date2)
        {
            using var db=new Data.ConDB();
            return new ObservableCollection<InsertOutCash>(db.InsertOutCash.Include(p => p.Employee).Where(p => (emp_id == 0 || p.EmployeeId == emp_id) && p.Date >= date1 && p.Date <= date2).ToList());
        }

        public ObservableCollection<Expenses> GetExpenses(int emp_id, DateTime date1, DateTime date2)
        {
            using var db = new Data.ConDB();
            return new ObservableCollection<Expenses>(db.Expenses.Include(p=>p.Employee).Include(p=>p.TypeExpenses).Include(p=>p.TypePay).Where(p => (emp_id == 0 || p.EmployeeId == emp_id) && p.Date >= date1 && p.Date <= date2).ToList());
        }
    }
}
