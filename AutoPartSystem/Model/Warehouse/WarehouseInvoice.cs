using AutoPartSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.Model.Warehouse
{
    public interface IWarehouseInvoce
    {
        public void SetWarehouse(ObservableCollection<WarehouseTable> warehouses);
        public ObservableCollection<WarehouseTable> GetWarehouseTables();
        public ObservableCollection<Data.Warehouse> GetWarehouse();
    }
    public class WarehouseInvoceModel : IWarehouseInvoce
    {
        ObservableCollection<WarehouseTable> warehouses;
        private Data.Warehouse GetWarehouseFromTable(WarehouseTable _warehouse_table)
        {
            return _warehouse_table as Data.Warehouse;
        }
        public ObservableCollection<Data.Warehouse> GetWarehouse()
        {
            return new ObservableCollection<Data.Warehouse>(warehouses.Select(p => GetWarehouseFromTable(p)).ToList());
        }

        public ObservableCollection<WarehouseTable> GetWarehouseTables()
        {
            return warehouses;
        }

        public void SetWarehouse(ObservableCollection<WarehouseTable> warehouses)
        {
            this.warehouses = warehouses;
            foreach (var warehouse in warehouses)
            {

            }
        }
    }
}
