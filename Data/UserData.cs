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
        private string _phone_number;
        public string PhoneNumber
        {
            get => _phone_number;
            set => this.RaiseAndSetIfChanged(ref _phone_number, value);
        }
        private bool _is_delete;
        public bool IsDelete
        {
            get => _is_delete;
            set => this.RaiseAndSetIfChanged(ref _is_delete, value);
        }

        public Employee(int id, string name, int positionId, Position position, int cityId, City city, string phoneNumber, int cash, bool setPrihod, bool setMoveCity, bool setMoveAgent, bool setEditGood, bool setReport, bool setStat, bool downGoods, bool setGoodCount, bool setGood, bool setCell)
        {
            Id = id;
            Name = name;
            PositionId = positionId;
            Position = position;
            CityId = cityId;
            City = city;
            PhoneNumber = phoneNumber;
            Cash = cash;
            SetPrihod = setPrihod;
            SetMoveCity = setMoveCity;
            SetMoveAgent = setMoveAgent;
            SetEditGood = setEditGood;
            SetReport = setReport;
            SetStat = setStat;
            DownGoods = downGoods;
            SetGoodCount = setGoodCount;
            SetGood = setGood;
            SetCell = setCell;
        }

        public Employee() { }
        public Employee(int id, string name, string login, int position_id, Position position,int CityId, City City, string phone) {
            this.Id = id;
            this.Name = name;
            this.Login = login;
            this.PositionId = position_id;
            this.Position = position;
            this.CityId = CityId;   
            this.City = City;
            this.PhoneNumber= phone;
        }
        private int cash;
        public int Cash
        {
            get => cash;
            set=>this.RaiseAndSetIfChanged(ref cash, value);
        }
        public void HashPassword()
        {
            this.Password = Hash.SHA512(Password);
        }
        public void SetPassword(string password)
        {
            this.Password = Hash.SHA512(password);
        }
        private bool set_prihod;
        public bool SetPrihod
        {
            get => set_prihod;
            set=>this.RaiseAndSetIfChanged(ref set_prihod, value);
        }
        private bool set_move_city;
        public bool SetMoveCity
        {
            get => set_move_city;
            set => this.RaiseAndSetIfChanged(ref set_move_city, value);
        }
        private bool set_move_agent;
        public bool SetMoveAgent
        {
            get => set_move_agent;
            set => this.RaiseAndSetIfChanged(ref set_move_agent, value);
        }
        private bool set_edit_good;
        public bool SetEditGood
        {
            get => set_edit_good;
            set=>this.RaiseAndSetIfChanged(ref set_edit_good, value);
        }
        private bool set_report;
        public bool SetReport
        {
            get => set_report;
            set=>this.RaiseAndSetIfChanged(ref set_report, value);
        }
        private bool set_stat;
        public bool SetStat
        {
            get => set_stat;
            set=>this.RaiseAndSetIfChanged(ref set_stat, value);
        }
        private bool down_goods;
        public bool DownGoods
        {
            get => down_goods;
            set=>this.RaiseAndSetIfChanged(ref down_goods, value);
        }
        private bool set_good_count;
        public bool SetGoodCount
        {
            get => set_good_count;
            set=>this.RaiseAndSetIfChanged(ref set_good_count, value);
        }
        private bool set_good;
        public bool SetGood
        {
            get => set_good;
            set => this.RaiseAndSetIfChanged(ref set_good, value);
        }
        private bool set_cell;
        public bool SetCell
        {
            get=> set_cell;
            set=>this.RaiseAndSetIfChanged(ref set_cell, value);    
        }
        private bool is_admin;
        public bool IsAdmin
        {
            get => is_admin;
            set=>this.RaiseAndSetIfChanged(ref is_admin, value);
        }
        private bool _is_open_cash;
        public bool IsOpenCash
        {
            get => _is_open_cash;
            set=>this.RaiseAndSetIfChanged(ref _is_open_cash, value);
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
        private string _address;
        public string Address
        {
            get => _address;
            set=>this.RaiseAndSetIfChanged(ref _address, value);
        }
        public City() { }
        public City(int Id, string Name)
        {
            this.Id=Id;
            this.Name=Name;
        }
        public City(string Name)
        {
            
            this.Name = Name;
        }
    }
}
