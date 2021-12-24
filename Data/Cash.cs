using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CashDay : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set=>this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _cash;
        public int Cash
        {
            get=> _cash;    
            set=>this.RaiseAndSetIfChanged(ref _cash, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get=> _employee_id;
            set=>this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set=>this.RaiseAndSetIfChanged(ref _employee, value);
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set=>this.RaiseAndSetIfChanged(ref _date, value);
        }
        public CashDay() { }
        public CashDay(int cash, int emp)
        {
            Cash = cash;
            EmployeeId = emp;
            Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
    }
    public class InsertOutCash:MainClass
    {
        private int _old_cash;
        public int OldCash
        {
            get => _old_cash;
            set => this.RaiseAndSetIfChanged(ref _old_cash, value);
        }
        private int _cash;
        public int Cash
        {
            get => _cash;
            set => this.RaiseAndSetIfChanged(ref _cash, value);
        }
        private int _new_cash;
        public int NewCash
        {
            get => _new_cash;
            set => this.RaiseAndSetIfChanged(ref _new_cash, value);
        }
        private string _type;
        public string Type
        {
            get => _type;
            set=>this.RaiseAndSetIfChanged(ref _type, value);
        }
        private DateTime _date;
        public DateTime Date
        {
            get=> _date;
            set=>this.RaiseAndSetIfChanged(ref _date, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => _employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);
        }
        public InsertOutCash() { }
        public InsertOutCash(string Name, int OldCash, int Cash, int NewCash, string type, int emp_id)
        {
            this.Name = Name;
            this.OldCash= OldCash;
            this.Cash= Cash;
            this.NewCash= NewCash;
            this.Type = type;
            this.EmployeeId= emp_id;
            Date = DateTime.Now;
        }
    }
    public class Expenses:ReactiveObject
    {

    }
    public class OpenCloseCash : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set=>this.RaiseAndSetIfChanged(ref _id, value);
        }
        private DateTime _open_date;
        public DateTime OpenDate
        {
            get => _open_date;
            set => this.RaiseAndSetIfChanged(ref _open_date, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set=>this.RaiseAndSetIfChanged(ref _employee,value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => _employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private DateTime _close_date;
        public DateTime CloseData
        {
            get => _close_date;
            set => this.RaiseAndSetIfChanged(ref _close_date, value);
        }
        private int _status;
        public int Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        private int _open_cash;
        public int OpenCash
        {
            get => _open_cash;
            set=>this.RaiseAndSetIfChanged(ref _open_cash, value);
        }
        private int _close_cash;
        public int CloseCash
        {
            get => _close_cash;
            set => this.RaiseAndSetIfChanged(ref _close_cash, value);
        }
        public OpenCloseCash() { }
        public OpenCloseCash(int emp_id, int Cash)
        {
            this.EmployeeId = emp_id;
            this.OpenDate = DateTime.Now;
            this.OpenCash = Cash;
        }
    }
}
