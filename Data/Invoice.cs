using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reactive.Linq;
namespace Data
{
    public class Invoice : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => this._employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => this._employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);
        }
        private ObservableCollection<GoodsInvoice> _good_invoice;
        public ObservableCollection<GoodsInvoice> GoodsInvoice
        {
            get => _good_invoice;
            set
            {
                this.RaiseAndSetIfChanged(ref _good_invoice, value);
               
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
        private int all_price;
        public int AllPrice
        {
            get => all_price;
            private set
            {
                this.RaiseAndSetIfChanged(ref all_price, value);
            }
        }
        private int _all_input_price;
        public int AllInputPrice
        {
            get => _all_input_price;
            set => this.RaiseAndSetIfChanged(ref _all_input_price, value);
        }
        private int _all_marz;
        public int AllMarz
        {
            get => _all_marz;
            set => this.RaiseAndSetIfChanged(ref _all_marz, value);
        }
        private bool _is_invoice;
        public bool IsInvoice
        {
            get => _is_invoice;
            set => this.RaiseAndSetIfChanged(ref _is_invoice, value);
        }
        private bool _is_agent;
        public bool IsAgent
        {
            get => _is_agent;
            set => this.RaiseAndSetIfChanged(ref _is_agent, value);
        }
        private void SetAllCountAndAllPrice()
        {
            AllPrice = GoodsInvoice.Sum(p => p.AllPrice);
            AllCount = GoodsInvoice.Sum(p => p.Count);
            AllInputPrice = GoodsInvoice.Sum(p => p.InputPrice);
            AllMarz = AllPrice - AllInputPrice;
            if (IsDelMarzh)
            {
                AllMarz = AllMarz / 2;
            }
        }
        private bool _is_del_marzh;
        public bool IsDelMarzh
        {
            get => _is_del_marzh;
            set
            {
                this.RaiseAndSetIfChanged(ref _is_del_marzh, value);  
            }
        }
        private bool _is_end;
        public bool IsEnd
        {
            get => _is_end;
            set=>this.RaiseAndSetIfChanged(ref _is_end, value);
        }
        public Invoice() { }
        public Invoice(Invoice inv)
        {
            this.Id = inv.Id;
            this.IsInvoice = inv.IsInvoice;
            this.EmployeeId = inv.Employee.Id;
            this.AllCount = inv.AllCount;
            this.AllPrice = inv.AllPrice;
            this.AllInputPrice = inv.AllInputPrice;
            this.AllMarz = inv.AllMarz;
            this.Date = inv.Date;
            IsDelMarzh = inv.IsDelMarzh;
            if (inv.Client.Id != 0)
            {
                this.ClientId = inv.Client.Id;
            }
            else
            {
                this.Client = inv.Client;
            }
            this.GoodsInvoice = new ObservableCollection<GoodsInvoice>(inv.GoodsInvoice.Select(p => new Data.GoodsInvoice(p)));
            this.IsAgent=inv.IsAgent;
        }
        private void test()
        {
            foreach (var s in GoodsInvoice)
            {
                s.WhenAnyValue(s => s.AllPrice).Subscribe(_ => SetAllCountAndAllPrice());
                s.WhenAnyValue(s => s.Count).Subscribe(_ => SetAllCountAndAllPrice());
                s.WhenAnyValue(s => s.InputPrice).Subscribe(_ => SetAllCountAndAllPrice());
                switch (Employee.CityId)
                {
                    case 1:
                        s.UpdateTrans(0);
                        break;
                    case 2:
                       
                        s.UpdateTrans(s.Goods.InputAstana);
                        break;
                    case 3:
                        s.UpdateTrans(s.Goods.InputAktau);
                        break;
                }
            }

            if (GoodsInvoice.Count == 0)
            {
                AllPrice = 0;
                AllCount = 0;
                AllInputPrice = 0;
                AllMarz = 0;
            }
            
        }
        public Invoice(Invoice inv, string type)
        {
            this.Id = inv.Id;
            this.IsInvoice = inv.IsInvoice;
            this.Employee = inv.Employee;
            this.EmployeeId = inv.Employee.Id;
            this.Client = inv.Client;
            this.ClientId = inv.ClientId;
            this.Date = inv.Date;
            this.AllCount = inv.AllCount;
            this.AllPrice = inv.AllPrice;
            this.AllInputPrice= inv.AllInputPrice;
            this.AllMarz = inv.AllMarz;
            this.IsAgent = inv.IsAgent;
            this.IsDelMarzh= inv.IsDelMarzh;
           
            this.GoodsInvoice = new ObservableCollection<GoodsInvoice>(inv.GoodsInvoice.Select(p => new Data.GoodsInvoice(p, inv.Id)));
            foreach (var s in GoodsInvoice)
            {
                s.WhenAnyValue(s => s.AllPrice).Subscribe(_ => SetAllCountAndAllPrice());
                s.WhenAnyValue(s => s.Count).Subscribe(_ => SetAllCountAndAllPrice());
            }
            this.WhenAnyValue(s => s.GoodsInvoice.Count).Subscribe(_ => test());
            this.WhenAnyValue(s => s.IsDelMarzh).Subscribe(_ => SetAllCountAndAllPrice());
            test();


        }
        public Invoice(ObservableCollection<Warehouse> warehouses, Employee employee)
        {
            GoodsInvoice = new ObservableCollection<GoodsInvoice>(warehouses.Select(p => new Data.GoodsInvoice(p.Goods)));
            Client = new Client();
            Client.new_mark_model();
            Employee = employee;
            foreach (var s in GoodsInvoice)
            {
                s.WhenAnyValue(s => s.AllPrice).Subscribe(_ => SetAllCountAndAllPrice());
                s.WhenAnyValue(s => s.Count).Subscribe(_ => SetAllCountAndAllPrice());
            }
            this.WhenAnyValue(s => s.GoodsInvoice.Count).Subscribe(_ => test());
            this.WhenAnyValue(s => s.IsDelMarzh).Subscribe(_ => SetAllCountAndAllPrice());
            Date = DateTime.Now;
            test();
        }
    }
    public class GoodsInvoice : ReactiveObject
    {
        public GoodsInvoice(GoodsInvoice goodsInvoice)
        {
            this.Id = goodsInvoice.Id;
            this.AllPrice = goodsInvoice.AllPrice;
            this.Count = goodsInvoice.Count;
            this.GoodsId = goodsInvoice.Goods.Id;
            this.ModelId = goodsInvoice.Model.Id;
            this.Price = goodsInvoice.Price;
            this.RecomPrice = goodsInvoice.RecomPrice;
            this.InputPrice = goodsInvoice.InputPrice;
            this.AllTrans = goodsInvoice.AllTrans;
            this.Marz=goodsInvoice.Marz;
            this.TypePayId = goodsInvoice.TypePayId;
           
        }
        public GoodsInvoice()
        { }
        private int _all_trans;
        public int AllTrans
        {
            get => _all_trans;
            set=>this.RaiseAndSetIfChanged(ref _all_trans, value);
        }
        private int _back_count;
        [NotMapped]
        public int BackCount
        {
            get => _back_count;
            set=>this.RaiseAndSetIfChanged(ref _back_count, value);
        }
        private int trans;
        public GoodsInvoice(GoodsInvoice goodsInvoice, int invoice_id)
        {

            this.Id = goodsInvoice.Id;
            this.Count = goodsInvoice.Count;
            this.GoodsId = goodsInvoice.Goods.Id;
            this.Goods = goodsInvoice.Goods;
            this.ModelId = goodsInvoice.Model.Id;
            this.Price = goodsInvoice.Price;
            this.Goods.PriceCell = this.Price;
            this.InvoiceId = invoice_id;
            this.Model = goodsInvoice.Model;
            this.AllPrice = goodsInvoice.AllPrice;
            this.InputPrice = goodsInvoice.InputPrice;
            this.AllTrans = goodsInvoice.AllTrans;
            this.Marz = goodsInvoice.Marz;
            this.TypePayId = goodsInvoice.TypePayId;
            this.WhenAnyValue(vm => vm.Goods.PriceCell).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdateTrans());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdateInput());
            this.WhenAnyValue(vm => vm.AllTrans).Subscribe(_ => UpdateInput());
            this.WhenAnyValue(vm => vm.InputPrice).Subscribe(_ => UpdateMarz());
            this.WhenAnyValue(vm => vm.AllPrice).Subscribe(_ => UpdateMarz());

        }
        public GoodsInvoice(Goods goods)
        {
            GoodsId = goods.Id;
            Goods = goods;
            TypePayId = goods.TypePayId;
            Console.WriteLine("dasd " + goods.TypePayId);
            Model = goods.GoodsModel.FirstOrDefault().Model;
            ModelId = Model.Id;
            Count = Goods.CountCell;
            Price = goods.PriceCell;
            RecomPrice = goods.RecomPrice;
            this.WhenAnyValue(vm => vm.Goods.PriceCell).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdatePrice());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdateTrans());
            this.WhenAnyValue(vm => vm.Count).Subscribe(_ => UpdateInput());
            this.WhenAnyValue(vm => vm.AllTrans).Subscribe(_ => UpdateInput());
            this.WhenAnyValue(vm => vm.InputPrice).Subscribe(_ => UpdateMarz());
            this.WhenAnyValue(vm => vm.AllPrice).Subscribe(_ => UpdateMarz());
            
            UpdatePrice();
            UpdateTrans();
            UpdateInput();
            UpdateMarz();
        }
        public void UpdateTrans(int trans)
        {
            this.trans = trans;
            UpdateTrans();
           // Console.WriteLine(trans);
        }
       
        private void UpdateTrans()
        {
            AllTrans = trans * Count;
        }
        private void UpdateInput()
        {
          
           InputPrice = Goods.InputPrice  * Count +AllTrans;
         
        }
        private void UpdateMarz()
        {
            Marz = AllPrice - InputPrice;
        }
        private bool _dont_have_goods;
        public bool DontHaveGoods
        {
            get => _dont_have_goods;
            set => this.RaiseAndSetIfChanged(ref _dont_have_goods, value);
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
        private int _price;
        public int Price
        {
            get => _price;
            set => this.RaiseAndSetIfChanged(ref this._price, value);
        }
        private int _all_price;
        public int AllPrice
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
            Price = Goods.PriceCell;
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

        private int _input_price;
        public int InputPrice
        {
            get => _input_price;
            set => this.RaiseAndSetIfChanged(ref _input_price, value);
        }
        private int _marz;
        public int Marz
        {
            get => _marz;
            set => this.RaiseAndSetIfChanged(ref _marz, value);
        }
        private int _recom_price;

        public int RecomPrice
        {
            get => _recom_price;
            set => this.RaiseAndSetIfChanged(ref _recom_price, value);
        }
        private int _type_pay_id;
        public int TypePayId
        {
            get => _type_pay_id;
            set=>this.RaiseAndSetIfChanged(ref _type_pay_id, value);
        }
        private TypePay _type_pay;
        public TypePay TypePay
        {
            get=> _type_pay;
            set=>this.RaiseAndSetIfChanged(ref _type_pay, value);
        }
    }
    public class BackInvoice : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => this._employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => this._employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);
        }
        private GoodsInvoice _good_invoice;
        public GoodsInvoice GoodsInvoice
        {
            get => this._good_invoice;
            set => this.RaiseAndSetIfChanged(ref _good_invoice, value);
        }
        private int _good_invoice_id;
        public int GoodsInvoiceId
        {
            get => _good_invoice_id;
            set => this.RaiseAndSetIfChanged(ref _good_invoice_id, value);
        }
        private int count;
        public int Count
        {
            get => count;
            set => this.RaiseAndSetIfChanged(ref count, value);
        }
    }
    public class Client : MainClass
    {
        private int model_id;
        public int ModelId
        {
            get => model_id;
            set => this.RaiseAndSetIfChanged(ref model_id, value);
        }
        private Model _model;
        public Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        private Mark _mark;
        public Mark Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private string _phone_name;
        public string PhoneName
        {
            get => _phone_name;
            set => this.RaiseAndSetIfChanged(ref _phone_name, value);
        }
        private int _city_id;
        public int CityId
        {
            get => _city_id;
            set => this.RaiseAndSetIfChanged(ref _city_id, value);
        }
        private string _city_name;
        public string CityName
        {
            get => _city_name;
            set => this.RaiseAndSetIfChanged(ref _city_name, value);
        }
        private int _mark_id;
        public int MarkId
        {
            get => _mark_id;
            set => this.RaiseAndSetIfChanged(ref _mark_id, value);
        }
        private City _city;
        public City City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }
        private bool _is_agent;
        public bool IsAgent
        {
            get => _is_agent;
            set=>this.RaiseAndSetIfChanged(ref _is_agent, value);
        }
        public Client()
        {
            Mark = new Mark();
        }
        public Client(Client p)
        {
            this.Id = p.Id;
            this.PhoneName = p.PhoneName;
            this.CityId = p.CityId;
            this.City = p.City;
            this.Name = p.Name;
            if(p.MarkId==0)
            {
                this.Model = p.Model;
                this.Mark = p.Model.Mark;
                MarkId = p.Model.MarkId;
            }
            else
            {
                this.ModelId = p.ModelId;
                this.Model = null;
            }
            
        }
        public Client(Client p, bool te)
        {
            this.Id = p.Id;
            this.PhoneName = p.PhoneName;
            this.CityId = p.CityId;
            this.City = p.City;
            this.Name = p.Name;
            this.ModelId = p.ModelId;
            this.IsAgent = te;
        }
        public Client (int model_id)
        {
            this.ModelId=model_id;
        }
        public void new_mark_model()
        {
            Mark = new Mark();
        }
    }
    public class TypePay:MainClass
    {
        public TypePay()
        { 
        }
        public TypePay(int id, string name)
        {
            this.Id=id;
            this.Name=name;
        }
    }
    public class MarzhEmployee:ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set=>this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _marz;
        public int Marz
        {
            get => _marz;
            set=>this.RaiseAndSetIfChanged(ref _marz, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => this._employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);

        }
        private Invoice _invoice;
        public Invoice Invoice
        {
            get => _invoice;
            set => this.RaiseAndSetIfChanged(ref _invoice, value);
        }
        private int _invoice_id;
        public int InvoiceId
        {
            get => this._invoice_id;
            set => this.RaiseAndSetIfChanged(ref _invoice_id, value);
        }
        public MarzhEmployee() { }
        public MarzhEmployee(int Marzh, int InvioceId, int EmployeeId)
        {
            this.Marz = Marzh;
            this.InvoiceId = InvioceId;
            this.EmployeeId = EmployeeId;
        }
    }
}
