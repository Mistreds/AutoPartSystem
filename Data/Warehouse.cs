using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        public  int GoodId
        {
            get => _good_id;
            set=>this.RaiseAndSetIfChanged(ref _good_id, value);
        }
        private Goods _goods;
        public virtual Goods Goods
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
       
        private string _warehouse_place;
        public string WarehousePlace
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
        private bool _is_virtual;
        public bool IsVirtual
        {
            get => _is_virtual;
            set=>this.RaiseAndSetIfChanged(ref _is_virtual, value);
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
        private ObservableCollection<GoodsModel> _good_model;
        public ObservableCollection<GoodsModel> GoodsModel
        {
            get => _good_model;
            set => this.RaiseAndSetIfChanged(ref _good_model, value);
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
        private  double _input_price;
        public  double InputPrice
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
        private double _input_astana;
        public double InputAstana

        {
            get => _input_astana;
            set => this.RaiseAndSetIfChanged(ref _input_astana, value);
        }
        private double _input_aktau;
        public double InputAktau

        {
            get => _input_aktau;
            set => this.RaiseAndSetIfChanged(ref _input_aktau, value);
        }
        private int _count_cell;
        public int CountCell
        {
            get => _count_cell;
            set => this.RaiseAndSetIfChanged(ref _count_cell, value);
        }
        private double _price_cell;
        public double PriceCell
        {
            get => _price_cell;
            set { this.RaiseAndSetIfChanged(ref _price_cell, value);

               
                this.RaisePropertyChanging();
            }
        }
        
        private int _type_pay_id;
        [NotMapped]
        public int TypePayId
        {
            get => _type_pay_id;
            set => this.RaiseAndSetIfChanged(ref _type_pay_id, value);
        }
        private TypePay _type_pay;
        [NotMapped]
        public TypePay TypePay
        {
            get => _type_pay;
            set => this.RaiseAndSetIfChanged(ref _type_pay, value);
        }
        public Goods() { }
        public Goods(Goods goods)
        {
            this.Id = goods.Id;
            this.WarehouseId = goods.WarehouseId;
            this.Warehouse =goods.Warehouse;
            this.GoodsModel = goods.GoodsModel;
            this.Description= goods.Description;
            this.Article = goods.Article;
            this.InputPrice = goods.InputPrice;
            this.RecomPrice = goods.RecomPrice;
            this.PriceCell = goods.PriceCell;
            this.CountCell = goods.CountCell;
            this.InputAstana = goods.InputAstana;
            this.InputAktau = goods.InputAktau;
           
            

        }
    }
    public class GoodsModel:ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set=>this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _goods_id;
        public int GoodsId
        {
            get => _goods_id;
            set=>this.RaiseAndSetIfChanged(ref _goods_id, value);
        }
        private Goods _goods;
        public Goods Goods
        {
            get => _goods;
            set => this.RaiseAndSetIfChanged(ref _goods, value);
        }
        private int _model_id;
        public int ModelId
        {
            get => _model_id;
            set => this.RaiseAndSetIfChanged(ref _model_id, value);
        }
        
        private Model _model;
        public Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
    }
  
}
