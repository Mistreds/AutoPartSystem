using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.Model.Admin
{
    public interface IAdminModelation
    {
        public List<Data.Position> GetPositions();
        public ObservableCollection<Data.Employee> GetEmployees();
        public ObservableCollection<Data.Employee> AddEmployers(Data.Employee employee);
        public List<Data.City> GetCities();
    }
    public class AdminModel : IAdminModelation
    {
        public ObservableCollection<Employee> AddEmployers(Data.Employee employee)
        {
            using var db = new ConDB();
            employee.HashPassword();
            db.Employees.Add(employee);
            db.SaveChanges();
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

        public List<Position> GetPositions()
        {
            using var db = new Data.ConDB();
            return db.Positions.ToList();
        }
    }
}
