using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace Data
{
   public class Invoice:ReactiveObject
    {
        private int _id;
        public int Id
        {
            get=> _id;
            set=>this.RaiseAndSetIfChanged(ref _id, value);
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
            get=>this._employee_id;
            set=>this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get=>this._employee;
            set=>this.RaiseAndSetIfChanged(ref _employee, value);
        }
        private ObservableCollection<GoodsInvoice> _good_invoice;
        public ObservableCollection<GoodsInvoice> GoodsInvoice
        {
            get => _good_invoice;
            set
            {
                this.RaiseAndSetIfChanged(ref _good_invoice, value);
                SetAllCountAndAllPrice();
            }
        }
        private int _client_id;
        public int ClientId
        {
            get => this._client_id;
            set => this.RaiseAndSetIfChanged(ref _client_id, value);
        }
        private Client _client;
        public Client Client
        {
            get => this._client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }
        private int all_count;
        public int AllCount
        {
            get => all_count;
            private set
            {
                this.RaiseAndSetIfChanged(ref all_count, value);
            }
        }
        private double all_price;
        public double AllPrice
        {
            get => all_price;
            private set
            {
                this.RaiseAndSetIfChanged(ref all_price, value);
            }
        }
        private void SetAllCountAndAllPrice()
        {
            all_price = GoodsInvoice.Sum(p => p.AllPrice);
            all_count = GoodsInvoice.Sum(p => p.Count);
        }
        public Invoice() { }
        public Invoice(ObservableCollection<Warehouse> warehouses,Employee employee)
        {
            GoodsInvoice = new ObservableCollection<GoodsInvoice>(warehouses.Select(p => new Data.GoodsInvoice(p.Goods)));
            Client= new Client();
            Employee=employee;
            Date = DateTime.Now;
        }
    }
    public class Client:MainClass
    {
        private int model_id;
        public int ModelId
        {
            get => model_id;
            set=>this.RaiseAndSetIfChanged(ref model_id, value);
        }
        private Model _model;
        public Model Model
        {
            get => _model;
            set=>this.RaiseAndSetIfChanged(ref _model, value);
        }

        private string _phone_name;
        public string PhoneName
        {
            get => _phone_name;
            set=>this.RaiseAndSetIfChanged(ref _phone_name, value);
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
            set=>this.RaiseAndSetIfChanged(ref _city,value);
        }
    
    }
}
