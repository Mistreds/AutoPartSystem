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
        private string _reg_number;
        public string RegNumber
        {
            get=> _reg_number;
            set=>this.RaiseAndSetIfChanged(ref _reg_number, value);
        }
        private Model _model;
        public Model Model
        {
            get=> _model;
            set=>this.RaiseAndSetIfChanged(ref _model, value);
        }
        private string _description;
        public string Description //Описание
        {
            get=> _description;
            set=>this.RaiseAndSetIfChanged(ref _description, value);
        }
        private string _article;
        public string Article
        {
            get => _article;
            set => this.RaiseAndSetIfChanged(ref _article, value);
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
        private double _input_price;
        public double InputPrice
        {
            get => _input_price;
            set => this.RaiseAndSetIfChanged(ref _input_price, value);
        }
        private int _warehouse_place;
        public int WarehousePlace
        {
            get => _warehouse_place;
            set=>this.RaiseAndSetIfChanged(ref _warehouse_place, value);
        }
        private double _recom_price;
        public double RecomPrice
        {
            get => _recom_price;
            set=>this.RaiseAndSetIfChanged(ref _recom_price, value);
        }
        public Warehouse() { }

    }
}
