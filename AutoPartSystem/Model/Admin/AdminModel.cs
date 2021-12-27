using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Windows;

namespace AutoPartSystem.Model.Admin
{
    public interface IAdminModelation
    {
        public List<Data.Position> GetPositions();
        public ObservableCollection<Data.Employee> GetEmployees();
        public ObservableCollection<Data.Employee> GetEmployeesForSelect();
        public ObservableCollection<Data.Employee> AddEmployers(Data.Employee employee);
        public List<Data.City> GetCities();
        public ObservableCollection<Data.City> GetCitiesFromText(string text);
        public Data.City GetCityFromName(string name);
        public ObservableCollection<Data.City> GetCityClient();
        public void UpdateCity();
        public List<City> GetEmpCity();
        public List<Employee> GetEmployeeMeneger(int EmpId);
        public ObservableCollection<Employee> GetManagerEmp();
        public void GetAddres(out string Almata, out string Astana, out  string Aktau);
        public void SaveAddress( string Almata, string Astana,  string Aktau);
        public void AddCity(string city);
        public void GetNowCash(int Cash);
        public void UpdateCash(int Cash,string name, Employee employee, string type);
        public void UpdateCash(int Cash, Employee employee);
        public Data.OpenCloseCash GetOpenCash(int emp_id);
        public ObservableCollection<Data.OpenCloseCash> GetNotCloseCashs(int emp_id);
        public void UpdateEmployee(ObservableCollection<Data.Employee> employees);
        public void UpdatePassEmployee(Data.Employee employee); 
        public void DeleteEmployee(Data.Employee employee);
    }
    public class AdminModel :ReactiveObject, IAdminModelation
    {
        private ObservableCollection<Data.Employee> _employees;
        public ObservableCollection<Data.Employee> Employees
        {
            get => _employees;
            set=>this.RaiseAndSetIfChanged(ref _employees, value);
        }
        private ObservableCollection<Data.City> _citys;
        public ObservableCollection<Data.City> Cities
        {
            get => _citys;
            set=>this.RaiseAndSetIfChanged(ref _citys, value);
        }
        public AdminModel()
        {
            using var db = new ConDB();
            Employees = new ObservableCollection<Employee>(db.Employees.Include(p => p.Position).Include(p => p.City).Where(p=>p.IsDelete==false));
            Cities = new ObservableCollection<City>(db.City.ToList());
        }
        public ObservableCollection<Employee> AddEmployers(Data.Employee employee)
        {
            using var db = new ConDB();
            db.Employees.Add(employee);
            db.SaveChanges();
            Employees.Add(employee);
            return GetEmployees();
        }
        public List<City> GetCities()
        {
            using var db = new Data.ConDB();
            return db.City.ToList();    
        }

        public ObservableCollection<Employee> GetEmployees()
        {
            using var db = new ConDB();
            return new ObservableCollection<Employee>(db.Employees.Include(p => p.Position).Include(p=>p.City).Where(p=>p.IsDelete==false));
        }

        public ObservableCollection<Employee> GetEmployeesForSelect()
        {
            var a=new ObservableCollection<Employee>(Employees);
            a.Insert(0, new Employee { Id = 0, Name = "Все сотрудники" });
            return a;
        }

        public List<Position> GetPositions()
        {
            using var db = new Data.ConDB();
            return db.Positions.ToList();
        }

        public ObservableCollection<City> GetCitiesFromText(string text)
        {
            return new ObservableCollection<City>(_citys.Where(p => p.Name.ToLower().Contains(text.ToLower())).ToList());
        }

        public City GetCityFromName(string name)
        {
            return Cities.Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public void UpdateCity()
        {
            using var db = new ConDB();
            Cities = new ObservableCollection<City>(db.City.ToList());
        }

        public List<City> GetEmpCity()
        {
            return _citys.Where(p => p.Id >= 1 && p.Id <= 3).ToList();
        }

        public void GetAddres(out string Almata, out string Astana, out string Aktau)
        {
            Almata = Cities.Where(p => p.Id == 1).Select(p => p.Address).FirstOrDefault();
            Astana = Cities.Where(p => p.Id == 2).Select(p => p.Address).FirstOrDefault();
            Aktau = Cities.Where(p => p.Id == 3).Select(p => p.Address).FirstOrDefault();
        }

        public void SaveAddress(string Almata, string Astana, string Aktau)
        {
            using var db = new Data.ConDB();
            var al=Cities.FirstOrDefault(p => p.Id == 1);
            var ast = Cities.FirstOrDefault(p => p.Id == 2);
            var ak = Cities.FirstOrDefault(p => p.Id == 3);
            al.Address = Almata;
            ast.Address = Astana;
            ak.Address = Aktau;
            db.Update(al);
            db.Update(ast);
            db.Update(ak);
            db.SaveChanges();
        }

        public ObservableCollection<City> GetCityClient()
        {
            return new ObservableCollection<City>(_citys.Where(p => p.Id > 3).ToList());
        }

        public void AddCity(string city)
        {
           using var db=new Data.ConDB();
            var citys = new Data.City(city);
            db.Add(citys);
            db.SaveChanges();
        }

        public List<Employee> GetEmployeeMeneger(int EmpId)
        {
            return Employees.Where(p => p.Id != EmpId && (p.PositionId == 3 || p.PositionId == 4)).ToList();
        }

        public void GetNowCash(int Cash)
        {
            using var db = new Data.ConDB();
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var cash = db.CashDay.Where(p => p.Date ==  date && p.EmployeeId== ViewModel.MainViewModel.Employee.Id).FirstOrDefault();
            if (cash == null)
            {
                Data.CashDay cash_day = new CashDay(Cash, ViewModel.MainViewModel.Employee.Id);
                db.Add(cash_day);
                db.SaveChanges();
            }
        }

        public void UpdateCash(int Cash, string name, Employee employee, string type)
        {
            int cash = 0;
            if(type== "Добавить" || type== "Оплата товара")
            {
                cash += Cash;
            }
            else
            {
                cash-=Cash;
            }
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23,59,59 );
            var day_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var inv = new Model.InvoiceModel();
            using var db = new Data.ConDB();
            db.InsertOutCash.Add(new Data.InsertOutCash(name, employee.Cash, cash, employee.Cash + cash, type, employee.Id));
            db.SaveChanges();
            employee.Cash=employee.Cash + cash;
            if(ViewModel.MainViewModel.Employee.Id==employee.Id)
            ViewModel.MainViewModel.Employee.Cash = employee.Cash;
            db.Update(employee);
            db.SaveChanges();
        }

        public void UpdateCash(int Cash, Employee employee)
        {
            using var db = new Data.ConDB();
            employee.Cash = employee.Cash + Cash;
            ViewModel.MainViewModel.Employee.Cash = employee.Cash;
            db.Update(employee);
            db.SaveChanges();
        }

        public ObservableCollection<Employee> GetManagerEmp()
        {
            return new ObservableCollection<Employee>(Employees.Where(p => p.Id ==1 || (p.PositionId == 3 || p.PositionId == 4)).ToList());
        }



       public OpenCloseCash GetOpenCash(int emp_id)
        {
            var day_start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
            var day_end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            using var db=new Data.ConDB();
            return db.OpenCloseCash.Where(p => p.EmployeeId == emp_id && p.OpenDate >= day_start && p.OpenDate <= day_end).FirstOrDefault();
        }

        public ObservableCollection<OpenCloseCash> GetNotCloseCashs(int emp_id)
        {
            using var db = new Data.ConDB();
            return new ObservableCollection<OpenCloseCash>(db.OpenCloseCash.Where(p => p.EmployeeId == emp_id && p.Status == 1).ToList());
        }

        public void UpdateEmployee(ObservableCollection<Employee> employees)
        {
            using var db=new Data.ConDB();
            {
                db.UpdateRange(employees);
                db.SaveChanges();
                
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить сотрудника {employee.Name} ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using var db = new Data.ConDB();
                {
                    db.Update(employee);
                    db.SaveChanges();
                    Employees.Remove(employee);
                }

            }
        }

        public void UpdatePassEmployee(Employee employee)
        {
            using var db = new Data.ConDB();
            {
                db.Update(employee);
                db.SaveChanges();

            }
        }
    }
}
