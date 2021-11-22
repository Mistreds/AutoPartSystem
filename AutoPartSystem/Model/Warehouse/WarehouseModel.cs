using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoPartSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AutoPartSystem.Model.Warehouse
{
    public static class WarehouseExtension
    {
       
    }
    public interface IWarehouseModel
    {
        public void AddWarehouse(Data.Warehouse warehouse);
        public ObservableCollection<WarehouseTable> GetAllWarehouse();
    }
    
    public class WarehouseModel:IWarehouseModel
    {
        private ObservableCollection<WarehouseTable> Warehouses;

        private async Task _get_warehouse_from_db()
        {
            await Task.Run(() =>
            { 
                using var db = new Data.ConDB();
               // Warehouses=new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Model).ThenInclude(p => p.Mark)
                   // .ToList());
            });
        }
        public WarehouseModel()
        {
            using var db = new Data.ConDB();
            Warehouses=new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods.Model).ThenInclude(p => p.Mark).Select(p=>new WarehouseTable(p.Id,p.Goods ,  p.InAlmata, p.InAstana, p.InAktau,p.WarehousePlace,  p.TypePay, p.Note)).ToList());

        }
        public void AddWarehouse(Data.Warehouse warehouse)
        {
            using var db = new Data.ConDB();
            db.Warehouse.Add(warehouse);
            db.SaveChanges();
        }

        public ObservableCollection<WarehouseTable> GetAllWarehouse()
        {
            return Warehouses;
        }
    }
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
            return new ObservableCollection<Data.Warehouse>(warehouses.Select(p=>GetWarehouseFromTable(p)).ToList());
        }

        public ObservableCollection<WarehouseTable> GetWarehouseTables()
        {
            return warehouses;
        }

        public void SetWarehouse(ObservableCollection<WarehouseTable> warehouses)
        {
            this.warehouses = warehouses;
            foreach(var warehouse in warehouses)
            {
               
            }
        }
    }
}