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
        public ObservableCollection<ViewModel.MarkModelFind> GetAllDesctiption(string name);
        public ObservableCollection<ViewModel.MarkModelFind> GetAllArticle(string name);
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
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
            Warehouses=new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods.GoodsModel).ThenInclude(p=>p.Model).ThenInclude(p=>p.Mark).Select(p=>new WarehouseTable(p.Id,p.Goods ,  p.InAlmata, p.InAstana, p.InAktau,p.WarehousePlace,  p.TypePay, p.Note)).ToList());
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

        public ObservableCollection<MarkModelFind> GetAllDesctiption(string name)
        {
            if(Warehouses==null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a=new ObservableCollection<MarkModelFind>(Warehouses.Where(p=>p.Goods.Description.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Goods.Description }).Select(p=>new MarkModelFind(p.Key.Id, p.Key.Description)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<MarkModelFind> GetAllArticle(string name)
        {
            if (Warehouses == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(Warehouses.Where(p => p.Goods.Article.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Goods.Article }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Article)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article)
        {
            //return new ObservableCollection<WarehouseTable>(Warehouses.Where(p=>model.Where(m=>m.IsSelected==true).Select(m=>m.model_id).ToList().Contains(p.Goods.ModelId) && desctiption.Where(m => m.IsSelected == true).Select(d=>d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id)));r
            return null;
        }
    }
    
}