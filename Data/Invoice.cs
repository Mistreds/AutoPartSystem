using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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
                foreach (var s in GoodsInvoice)
                {
                    s.WhenAnyValue(s => s.AllPrice).Subscribe(_ => SetAllCountAndAllPrice());
                }
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
        private bool _is_invoice;
        public bool IsInvoice
        {
            get => _is_invoice;
            set=>this.RaiseAndSetIfChanged(ref _is_invoice,value);
        }
        private void SetAllCountAndAllPrice()
        {
            AllPrice = GoodsInvoice.Sum(p => p.AllPrice);
            AllCount = GoodsInvoice.Sum(p => p.Count);
        }
        public Invoice() { }
        public Invoice(Invoice inv)
        {
            this.Id = inv.Id;
            this.IsInvoice = inv.IsInvoice;
            this.EmployeeId = inv.Employee.Id;
            this.AllCount = inv.AllCount;
            this.Date=inv.Date;
            if(inv.Client.Id!=0)
            {
                this.ClientId = inv.Client.Id;
            }
            else
            {
                this.Client = inv.Client;
            }
            this.GoodsInvoice = new ObservableCollection<GoodsInvoice>(inv.GoodsInvoice.Select(p=>new Data.GoodsInvoice(p)));
        }
        public Invoice(Invoice inv, string type)
        {
            this.Id=inv.Id;
            this.IsInvoice=inv.IsInvoice;
            this.GoodsInvoice = new ObservableCollection<GoodsInvoice>(inv.GoodsInvoice.Select(p => new Data.GoodsInvoice(p, inv.Id)));
            this.Employee = inv.Employee;
            this.EmployeeId=inv.Employee.Id;
            this.Client=inv.Client;
            this.ClientId = inv.ClientId;
            this.Date = inv.Date;
            this.AllCount=inv.AllCount;
            this.AllPrice=inv.AllPrice;
        }
        public Invoice(ObservableCollection<Warehouse> warehouses,Employee employee)
        {
            GoodsInvoice = new ObservableCollection<GoodsInvoice>(warehouses.Select(p => new Data.GoodsInvoice(p.Goods)));
            Client= new Client();
            Client.new_mark_model();
            Employee=employee;
            foreach(var s in GoodsInvoice)
            {
                s.WhenAnyValue(s => s.AllPrice).Subscribe(_ => SetAllCountAndAllPrice());
            }
            Date = DateTime.Now;
        }
    }
    public class GoodsInvoice : ReactiveObject
    {
        public GoodsInvoice(GoodsInvoice goodsInvoice)
        {
            this.Id= goodsInvoice.Id;
            this.AllPrice= goodsInvoice.AllPrice;
            this.Count= goodsInvoice.Count;
            this.GoodsId = goodsInvoice.Goods.Id;
            this.ModelId = goodsInvoice.Model.Id;
            this.Price = goodsInvoice.Price;
            
        }
        public GoodsInvoice()
        { }
        public GoodsInvoice(GoodsInvoice goodsInvoice, int invoice_id){

            this.Id = goodsInvoice.Id;
            this.Count = goodsInvoice.Count;
            this.GoodsId = goodsInvoice.Goods.Id;
            this.Goods = goodsInvoice.Goods;
            this.ModelId = goodsInvoice.Model.Id;
            this.Price = goodsInvoice.Price;
            this.Goods.PriceCell = this.Price;
            this.InvoiceId= invoice_id;
            this.Model= goodsInvoice.Model;
            this.AllPrice = goodsInvoice.AllPrice;
            this.WhenAnyValue(vm => vm.Goods.PriceCell).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdatePrice());

        }
        public GoodsInvoice(Goods goods)
        {
            GoodsId = goods.Id;
            Goods = goods;
            Model = goods.GoodsModel.FirstOrDefault().Model;
            ModelId = Model.Id;
            this.WhenAnyValue(vm => vm.Goods.PriceCell).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdatePrice());
            Count = Goods.CountCell;
            Price = goods.PriceCell;
            UpdatePrice();
        }
        private bool _dont_have_goods;
        public bool DontHaveGoods
        {
            get => _dont_have_goods;
            set=>this.RaiseAndSetIfChanged(ref _dont_have_goods, value);
        }
        private int _model_id;
        public int ModelId
        {
            get => _model_id;
            set => this.RaiseAndSetIfChanged(ref _model_id, value);
        }
        private Model model;
        public Model Model
        {
            get => model;
            set => this.RaiseAndSetIfChanged(ref model, value);
        }
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                this.RaiseAndSetIfChanged(ref _count, value);
                
            }
        }
        private int _goods_id;
        public int GoodsId
        {
            get => _goods_id;
            set
            {
                this.RaiseAndSetIfChanged(ref _goods_id, value);
            }
        }
        private double _price;
        public double Price
        {
            get => _price;
            set=>this.RaiseAndSetIfChanged(ref this._price, value); 
        }
        private double _all_price;
        public double AllPrice
        {
            get => _all_price;
            private set
            {
                this.RaiseAndSetIfChanged(ref _all_price, value);
            }
        }
        private void UpdatePrice()
        {
            AllPrice = Goods.PriceCell * Count;
        }
        private int _invoice_id;
        public int InvoiceId
        {
            get => _invoice_id;
            set => this.RaiseAndSetIfChanged(ref _invoice_id, value);
        }
        private Invoice _invoice;
        public Invoice Invoice
        {
            get => _invoice;
            set => this.RaiseAndSetIfChanged(ref _invoice, value);
        }
        private Goods _goods;
        public Goods Goods
        {
            get => _goods;
            set
            {
                this.RaiseAndSetIfChanged(ref _goods, value);
                UpdatePrice();

            }
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
        private Mark _mark;
        public Mark Mark
        {
            get => _mark;
            set=>this.RaiseAndSetIfChanged(ref _mark, value);
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
        public Client()
        {
            Mark=new Mark();
        }
        public Client(Client p)
        {
            this.Id = p.Id;
            this.PhoneName = p.PhoneName;
            this.CityId = p.CityId;
            this.City = p.City;
            this.Name = p.Name;
            this.Model = p.Model;
            this.Mark=p.Model.Mark;
            this.ModelId = p.ModelId;
        }
        public void new_mark_model()
        {
            Mark=new Mark();
        }
    }
}
