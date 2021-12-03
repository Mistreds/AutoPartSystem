
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.ViewModel
{
    public class WarehouseTable : Data.Warehouse
    {
        private bool _is_selected;
        public bool IsSelected
        {
            get => _is_selected;
            set => this.RaiseAndSetIfChanged(ref _is_selected, value);

        }
        public void SetArrivel(int almata, int astana, int actau)
        {
            this.InAlmata += almata;
            this.InAktau += actau;
            this.InAstana += astana;
        }
        public void SetPrice(double input, double recoum)
        {
            this.Goods.RecomPrice = recoum;
            this.Goods.InputPrice = input;
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
        public WarehouseTable(int id , Data.Goods good, int inAlmata , int inAstana , int inAktau ,
         string warehousePlace ,
            string typePay , string note, bool is_virtual )
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
