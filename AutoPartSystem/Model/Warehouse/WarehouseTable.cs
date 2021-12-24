
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.ViewModel
{
    public class WarehouseAdd : Data.Warehouse
    {
        private string model_name;
        public string ModelName
        {
            get => model_name;
            set => this.RaiseAndSetIfChanged(ref model_name, value);
        }
        private Data.Mark _mark;
        public Data.Mark Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private string mark_name;
        public string MarkName
        {
            get => mark_name;
            set => this.RaiseAndSetIfChanged(ref mark_name, value);
        }
        private string _brand_name;
        public string BrandName
        {
            get => _brand_name;
            set => this.RaiseAndSetIfChanged(ref _brand_name,value);
        }
        public WarehouseAdd()
        {
            this.Goods = new Data.Goods();
            this.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel>();
        }
        public WarehouseAdd(bool virt)
        {
            if (virt)
            {
                this.Goods = new Data.Goods();
                this.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel>();
                IsVirtual = true;
            }
        }
    }
    public class ModelAdd : Data.Model
    {
        private string mark_name;
        public string MarkName
        {
            get => mark_name;
            set => this.RaiseAndSetIfChanged(ref mark_name, value);
        }
        private Data.Model _model;
        public Data.Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
    }
    public class MarkModelFind : ReactiveObject
    {
        public int model_id { get; set; }
        public string model_name { get; set; }
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set => this.RaiseAndSetIfChanged(ref _is_selected, value);
        }
        public MarkModelFind() { }
        public MarkModelFind(int id, string name)
        {
            model_id = id;
            model_name = name;
            IsSelected = true;
        }
    }
    public class WarehouseTable : Data.Warehouse
    {
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set => this.RaiseAndSetIfChanged(ref _is_selected, value);

        }
        private int prom_input;
        public int PromInput
        {
            get => prom_input;
            set=>this.RaiseAndSetIfChanged(ref prom_input, value);
        }
        private bool _is_booking;
        public bool IsBooking
        {
            get => _is_booking;
            set=>this.RaiseAndSetIfChanged(ref _is_booking, value);
        }
        public void UpdateWare(WarehouseTable update)
        {
            this.Goods.InputPrice = update.Goods.InputPrice;
            this.InAktau=update.InAktau;
            this.InAlmata=update.InAlmata;
            this.InAstana=update.InAstana;
            this.Goods.RecomPrice=update.Goods.RecomPrice;
            this.Goods.InputAstana=update.Goods.InputAstana;
            this.Goods.InputAktau=update.Goods.InputAktau;
            if (MainViewModel.Employee.SetCell && !MainViewModel.Employee.IsAdmin)
            {
                switch (MainViewModel.CityId)
                {
                    case 1:
                        PromInput = Goods.InputPrice;
                        break;
                    case 2:
                        PromInput = Goods.InputPrice + Goods.InputAstana;
                        break;
                    case 3:
                        PromInput = Goods.InputPrice + Goods.InputAktau;
                        break;
                }
            }
            else
            {
                PromInput = Goods.InputPrice;
            }
        }
        private ObservableCollection<Data.GoodsInvoice> _goods_booking;
        public ObservableCollection<Data.GoodsInvoice> GoodsBooking
        {
            get => _goods_booking;
            set=>this.RaiseAndSetIfChanged(ref _goods_booking, value);
        }
        public void SetArrivel(int almata, int astana, int actau)
        {
            this.InAlmata += almata;
            this.InAktau += actau;
            this.InAstana += astana;
        }
        public void SetPrice(int input, int recoum, int astana, int aktau)
        {
            this.Goods.RecomPrice = recoum;
            this.Goods.InputPrice = input;
            this.Goods.InputAstana = astana;
            this.Goods.InputAktau = aktau;

        }
        public WarehouseTable()
        {
        }
        public void SetFromThisObject(WarehouseTable table)
        {
            Id = table.Id;
            Goods = table.Goods;
            InAlmata = table.InAlmata;
            InAstana = table.InAstana;
            InAktau = table.InAktau;
            WarehousePlace = table.WarehousePlace;
            TypePay = table.TypePay;
            Note = table.Note;
        }
        public static WarehouseTable NewTable(WarehouseTable table)
        {
           
            var WarehouseTable = new WarehouseTable(table.Id, new Data.Goods(table.Goods), table.InAlmata, table.InAstana, table.InAktau, table.WarehousePlace, table.TypePay, table.Note, table.IsVirtual);
         
            return WarehouseTable;
        }
        public static WarehouseTable NewTable(WarehouseTable table,bool type)
        {

            var WarehouseTable = new WarehouseTable(table.Id, new Data.Goods(table.Goods), table.InAlmata, table.InAstana, table.InAktau, table.WarehousePlace, table.TypePay, table.Note, table.IsVirtual,true);
            Console.WriteLine("iuqwueqw " + WarehouseTable.Goods.TypePayId);
            return WarehouseTable;
        }
        public ReactiveCommand<Unit, Unit> GetAlmataInput => ReactiveCommand.Create(() => {
            PromInput = Goods.InputPrice;
        });
        public ReactiveCommand<Unit, Unit> GetAstanaInput => ReactiveCommand.Create(() => {
            PromInput = Goods.InputPrice+Goods.InputAstana;
        });
        public ReactiveCommand<Unit, Unit> GetAktauInput => ReactiveCommand.Create(() => {
            PromInput = Goods.InputPrice+Goods.InputAktau;
        });
        public WarehouseTable(int id , Data.Goods good, int inAlmata , int inAstana , int inAktau ,
         string warehousePlace ,
            string typePay , string note, bool is_virtual )
        {
            Id = id;
            Goods = good;
            if(MainViewModel.Employee.SetCell && !MainViewModel.Employee.IsAdmin)
            {
                switch(MainViewModel.CityId)
                {
                    case 1:
                        PromInput = Goods.InputPrice;
                        break;
                    case 2:
                        PromInput = Goods.InputPrice + Goods.InputAstana;
                        break;
                    case 3:
                        PromInput = Goods.InputPrice + Goods.InputAktau;
                        break;
                }
            }
           else
            {
                PromInput = Goods.InputPrice;
            }
            Goods.TypePayId = 1;
            Goods.PriceCell = Goods.RecomPrice;
            GoodsBooking = MainViewModel.InvoiceViewModel.InvoiceModel.FindBookingGoods(Goods.Id);
            foreach(var item in GoodsBooking)
            {
                Console.WriteLine(item.Invoice.Employee.Name+" "+item.Count);
            }
            if(GoodsBooking.Count>0)
            {
                IsBooking = true;
            }
            InAlmata = inAlmata;
            InAstana = inAstana;
            InAktau = inAktau;
            WarehousePlace = warehousePlace;
            TypePay = typePay;
            Note = note;
            IsVirtual = is_virtual;
            
        }
        public WarehouseTable(int id, Data.Goods good, int inAlmata, int inAstana, int inAktau,
         string warehousePlace,
            string typePay, string note, bool is_virtual, bool type)
        {
            Id = id;
            Goods = good;
            InAlmata = inAlmata;
            InAstana = inAstana;
            InAktau = inAktau;
            WarehousePlace = warehousePlace;
            TypePay = typePay;
            Note = note;
            IsVirtual = is_virtual;

        }
    }

}
