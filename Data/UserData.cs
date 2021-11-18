using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public  class Employee: UserLogin
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
        public Employee() { }
        public Employee(int id, string name, string login, int position_id, Position position) {
            this.Id = id;
            this.Name = name;
            this.Login = login;
            this.PositionId = position_id;
            this.Position = position;
        }
        public void HashPassword()
        {
            this.Password = Hash.SHA512(Password);
        }
        public Employee(int id, string name,string login,string password, int position_id)
        {
            Id = id;
            Name = name;
            PositionId = position_id;
            this.Login = login;
            this.Password = Hash.SHA512(password);
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
    public class Position : ReactiveObject
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
}
