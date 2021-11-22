using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace Data
{
    public class Warehouse:ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _good_id;
        public int GoodId
        {
            get => _good_id;
            set=>this.RaiseAndSetIfChanged(ref _good_id, value);
        }
        private Goods _goods;
        public Goods Goods
        {
            get => _goods;
            set=>this.RaiseAndSetIfChanged(ref _goods, value);
        }
        private int _in_almata;
        public int InAlmata
        {
            get => _in_almata;
            set=>this.RaiseAndSetIfChanged(ref _in_almata, value); 
        }
        private int _in_astana;
        public int InAstana
        {
            get => _in_astana;
            set=>this.RaiseAndSetIfChanged(ref _in_astana, value);
        }
        private int _in_aktau;
        public int InAktau
        {
            get => _in_aktau;
            set => this.RaiseAndSetIfChanged(ref _in_aktau, value);
        }
       
        private int _warehouse_place;
        public int WarehousePlace
        {
            get => _warehouse_place;
            set=>this.RaiseAndSetIfChanged(ref _warehouse_place, value);
        }
        
        private string _type_pay;
        public string TypePay
        {
            get => _type_pay;
            set=>this.RaiseAndSetIfChanged(ref _type_pay, value);
        }
        private string _note;
        public string Note
        {
            get=> _note;
            set=>this.RaiseAndSetIfChanged(ref _note, value);
        }
        public Warehouse() { }
    }
    public class Goods:ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _model_id;
        public int ModelId
        {
            get => _model_id;
            set => this.RaiseAndSetIfChanged(ref _model_id, value);
        }
        private int _warehouse_id;
        public int WarehouseId
        {
            get => _warehouse_id;
            set => this.RaiseAndSetIfChanged(ref _warehouse_id, value);
        }
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private Model _model;
        public Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        private string _description;
        public string Description //Описание
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        private string _article;
        public string Article
        {
            get => _article;
            set => this.RaiseAndSetIfChanged(ref _article, value);
        }
        private double _input_price;
        public double InputPrice
        {
            get => _input_price;
            set => this.RaiseAndSetIfChanged(ref _input_price, value);
        }
        private double _recom_price;
        public double RecomPrice
        {
            get => _recom_price;
            set => this.RaiseAndSetIfChanged(ref _recom_price, value);
        }
    }
    public class GoodsInvoice : ReactiveObject
    {
        public GoodsInvoice() { }
        public GoodsInvoice(Goods goods)
        {
            GoodId = goods.Id;
            Goods = goods;
            Count = 1;
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
            set {
                this.RaiseAndSetIfChanged(ref _count, value);
                UpdatePrice();
            }
        }
        private double _trans_price;
        public double TransPrice
        {
            get => _trans_price;
            set
            {
                this.RaiseAndSetIfChanged(ref _trans_price, value);
                UpdatePrice();
            }

        }
        private int _good_id;
        public int GoodId
        {
            get => _good_id;
            set {
                this.RaiseAndSetIfChanged(ref _good_id, value);
               
            }
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
            _all_price = Goods.RecomPrice * Count - TransPrice;
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
            set=>this.RaiseAndSetIfChanged(ref _invoice, value);
        }
        private Goods _goods;
        public Goods Goods
        {
            get => _goods;
            set {
                this.RaiseAndSetIfChanged(ref _goods, value);
                UpdatePrice();
            }
        }
    }
}
