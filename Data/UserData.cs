using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MainClass : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }
    public  class Employee: UserLogin, IMainInterface
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);   
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        private int _position_id;
        public int PositionId
        {
            get => _position_id;
            set=>this.RaiseAndSetIfChanged(ref _position_id, value); 
        }
        private Position _position;
        public Position Position
        {
            get=> _position;
            set=>this.RaiseAndSetIfChanged(ref _position, value); 
        }
        private int _city_id;
        public int CityId
        {
            get => _city_id;
            set => this.RaiseAndSetIfChanged(ref _city_id, value);
        }
        private City _city;
        public City City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }
        public Employee() { }
        public Employee(int id, string name, string login, int position_id, Position position,int CityId, City City) {
            this.Id = id;
            this.Name = name;
            this.Login = login;
            this.PositionId = position_id;
            this.Position = position;
            this.CityId = CityId;   
            this.City = City;
        }
        public void HashPassword()
        {
            this.Password = Hash.SHA512(Password);
        }
        public Employee(int id, string name,string login,string password, int position_id, int CityId)
        {
            Id = id;
            Name = name;
            PositionId = position_id;
            this.Login = login;
            this.Password = Hash.SHA512(password);
            this.CityId = CityId;
        }
    }
    public class UserLogin:ReactiveObject
    {
        private string _login;
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public UserLogin() { }
        public UserLogin(int id,string login, string password)
        {
            this.Login=login;
            this._password = Hash.SHA512(password);
        }
    }
    public class Position : ReactiveObject, IMainInterface
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private string _name;
        public string Name
        { 
            get => _name;
            set=>this.RaiseAndSetIfChanged(ref _name, value);
        }
        public Position()
        {

        }
        public Position(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
    public interface IMainInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class City : ReactiveObject, IMainInterface
    {
        private int _id;
        public int Id
        {
            get=> _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private string _name;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        public City() { }
        public City(int Id, string Name)
        {
            this.Id=Id;
            this.Name=Name;
        }
    }
}
