using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
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
        public void UpdateCity();
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
            Employees = new ObservableCollection<Employee>(db.Employees.Include(p => p.Position).Include(p => p.City).Select(p => new Employee(p.Id, p.Name, p.Login, p.PositionId, p.Position, p.CityId, p.City, p.PhoneNumber)));
            Cities = new ObservableCollection<City>(db.City.ToList());
        }
        public ObservableCollection<Employee> AddEmployers(Data.Employee employee)
        {
            using var db = new ConDB();
            employee.HashPassword();
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
            return new ObservableCollection<Employee>(db.Employees.Include(p => p.Position).Include(p=>p.City).Select(p => new Employee(p.Id, p.Name, p.Login, p.PositionId, p.Position,p.CityId, p.City, p.PhoneNumber)));
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
    }
}
