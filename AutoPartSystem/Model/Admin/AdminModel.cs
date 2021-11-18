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
    }
    public class AdminModel : IAdminModelation
    {
        public ObservableCollection<Employee> AddEmployers(Data.Employee employee)
        {
            using var db = new ConnectDB();
            employee.HashPassword();
            db.Employees.Add(employee);
            db.SaveChanges();
            return GetEmployees();
        }

        public ObservableCollection<Employee> GetEmployees()
        {
            using var db = new ConnectDB();
            return new ObservableCollection<Employee>(db.Employees.Include(p => p.Position).Select(p => new Employee(p.Id, p.Name, p.Login, p.PositionId, p.Position)));
        }

        public List<Position> GetPositions()
        {
            using var db = new Data.ConnectDB();
            return db.Positions.ToList();
        }
    }
}
