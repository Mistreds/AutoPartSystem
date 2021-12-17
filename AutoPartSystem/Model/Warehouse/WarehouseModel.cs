using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
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
        public WarehouseTable AddWarehouse1(WarehouseAdd warehouse);
        public ObservableCollection<WarehouseTable> GetAllWarehouse();
        public ObservableCollection<WarehouseTable> GetAllWarehouseForSale();
        public ObservableCollection<WarehouseTable> GetWarehouseVirtualTables();
        public ObservableCollection<WarehouseTable> GetWarehouseVirtualForSale();
        public ObservableCollection<ViewModel.MarkModelFind> GetAllDesctiption(string name);
        public ObservableCollection<ViewModel.MarkModelFind> GetAllArticle(string name);
        public ObservableCollection<ViewModel.MarkModelFind> GetAllVirtualDesctiption(string name);
        public ObservableCollection<ViewModel.MarkModelFind> GetAllVirtualArticle(string name);
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
        public ObservableCollection<WarehouseTable> GetWarehousesVirtualFilter(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
        public ObservableCollection<WarehouseTable> GetWarehousesFilterZav(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
        public void UpdateWarehouseCount(int almata, int astana, int ackau, int war_id);
        public void UpdateWarehouseMainPrice(double input_price, double rec_price, double ast_price, double akt_price, int id);
        public void UpdateWarehouse(WarehouseTable warehouse);
        public void UpdateWarehouse(Data.Warehouse warehouse);
        public void MoveOrderToWarehouse(Data.MainMove mainMove);
        public void ArriveGoods(Data.MainMove mainMove);
        public List<Data.TypePay> GetTypePay();
        public Data.Warehouse GetWarehouseFromArticleAndDes(string article, string description);
    }

    public class WarehouseModel : IWarehouseModel
    {
        private ObservableCollection<WarehouseTable> Warehouses;
        private ObservableCollection<WarehouseTable> WarehouseVirtual;
        private List<Data.TypePay> TypePay;
        private async Task _get_warehouse_from_db()
        {
            await Task.Run(() =>
            {
                using var db = new Data.ConDB();
                // Warehouses=new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Model).ThenInclude(p => p.Mark)
                // .ToList());
            });
        }
        private WarehouseViewModel _view_model;
        public WarehouseModel(ViewModel.WarehouseViewModel ViewModel)
        {
            _view_model = ViewModel;
            using var db = new Data.ConDB();
            GetWarehouseFromDb();
            TypePay = db.TypePay.ToList();
            WarehouseVirtual = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == true).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
        }
        private void GetWarehouseFromDb()
        {
            using var db = new Data.ConDB();
            Warehouses = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p=>p.Goods.Brand).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
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
            if (Warehouses == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(Warehouses.Where(p => p.Goods.Description.ToLower().Contains(name.ToLower()) && p.IsVirtual == false).GroupBy(x => new { x.Id, x.Goods.Description }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Description)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<MarkModelFind> GetAllArticle(string name)
        {
            if (Warehouses == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(Warehouses.Where(p => p.Goods.Article.ToLower().Contains(name.ToLower()) && p.IsVirtual == false).GroupBy(x => new { x.Id, x.Goods.Article }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Article)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article)
        {
            var a = new ObservableCollection<WarehouseTable>(Warehouses.Where(p => model.Any(m => m.IsSelected == true && p.Goods.GoodsModel.Select(p => p.ModelId).ToList().Contains(m.model_id)) && desctiption.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && p.IsVirtual == false));
            var b = new ObservableCollection<WarehouseTable>();
            foreach (var item in a)
            {
                foreach (var item2 in item.Goods.GoodsModel.Where(p => model.Where(s => s.IsSelected == true).Select(s => s.model_id).Contains(p.ModelId)))
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    b.Add(bi);
                }
            }
            return b;
        }
        public ObservableCollection<WarehouseTable> GetWarehousesFilterZav(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article)
        {
            var a = new ObservableCollection<WarehouseTable>(Warehouses.Where(p => model.Any(m => m.IsSelected == true && p.Goods.GoodsModel.Select(p => p.ModelId).ToList().Contains(m.model_id)) && desctiption.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && p.IsVirtual == false));

            return a;
        }
        public void UpdateWarehouseCount(int almata, int astana, int ackau, int war_id)
        {
            var Warehouse = Warehouses.Where(p => p.Id == war_id).FirstOrDefault();
            Warehouse.SetArrivel(almata, astana, ackau);
            UpdateWarehouse(Warehouse);
        }
        public void UpdateWarehouseMainPrice(double input_price, double rec_price, double ast_price, double akt_price, int war_id)
        {
            var Warehouse = Warehouses.Where(p => p.Id == war_id).FirstOrDefault();
            Warehouse.SetPrice(input_price, rec_price, ast_price, akt_price);
            UpdateWarehouse(Warehouse);
        }
        public void UpdateWarehouse(WarehouseTable warehouse)
        {
            using var db = new Data.ConDB();
            db.Warehouse.Update(warehouse);
            db.SaveChanges();
            _view_model.WarehousesTable = GetAllWarehouse();
        }

        public ObservableCollection<WarehouseTable> GetWarehouseVirtualTables()
        {
            return WarehouseVirtual;
        }
        public ObservableCollection<MarkModelFind> GetAllVirtualDesctiption(string name)
        {
            if (Warehouses == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(WarehouseVirtual.Where(p => p.Goods.Description.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Goods.Description }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Description)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<MarkModelFind> GetAllVirtualArticle(string name)
        {
            if (Warehouses == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(WarehouseVirtual.Where(p => p.Note.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Note }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Note)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<WarehouseTable> GetWarehousesVirtualFilter(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article)
        {
            var a = new ObservableCollection<WarehouseTable>(WarehouseVirtual.Where(p => model.Any(m => m.IsSelected == true && p.Goods.GoodsModel.Select(p => p.ModelId).ToList().Contains(m.model_id)) && desctiption.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id)));
            if (model.Count() == model.Count(p => p.IsSelected == true))
            {
                return a;
            }
            var b = new ObservableCollection<WarehouseTable>();
            foreach (var item in a)
            {
                foreach (var item2 in item.Goods.GoodsModel.Where(p => model.Where(s => s.IsSelected == true).Select(s => s.model_id).Contains(p.ModelId)))
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    b.Add(bi);
                }
            }
            return b;
        }
        public ObservableCollection<WarehouseTable> GetAllWarehouseForSale()
        {
            var b = new ObservableCollection<WarehouseTable>();
            foreach (var item in Warehouses)
            {
                foreach (var item2 in item.Goods.GoodsModel)
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    b.Add(bi);
                }
            }
            return b;
        }
        public ObservableCollection<WarehouseTable> GetWarehouseVirtualForSale()
        {
            var b = new ObservableCollection<WarehouseTable>();
            foreach (var item in WarehouseVirtual)
            {
                foreach (var item2 in item.Goods.GoodsModel)
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    b.Add(bi);
                }
            }
            return b;
        }
        public WarehouseTable AddWarehouse1(WarehouseAdd ware)
        {

            foreach (var a in ware.Goods.GoodsModel)
            {
                if (a.Model.Id != 0)
                {
                    a.ModelId = a.Model.Id;
                    a.Model = null;
                }
                else
                {
                    if (a.Model.Mark.Id != 0)
                    {
                        a.Model.MarkId = a.Model.Mark.Id;
                        a.Model.Mark = null;
                    }
                }
            }
            using var db = new Data.ConDB();
            db.Warehouse.Add(ware);
            db.SaveChanges();

            var war = db.Warehouse.Include(p => p.Goods)
                  .Include(p => p.Goods.GoodsModel)
                  .ThenInclude(p => p.Model)
                  .ThenInclude(p => p.Mark)
                  .Where(p => p.IsVirtual == true && p.Id == ware.Id)
                  .Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual))
                  .FirstOrDefault();


            Console.WriteLine(war.Id);
            return war;
        }

        public void MoveOrderToWarehouse(Data.MainMove mainMove)
        {
            foreach (var ware in Warehouses.Where(p => mainMove.MoveGoods.Select(p => p.WarehouseId).Contains(p.Id)))
            {
                switch (mainMove.CityFromId)
                {
                    case 1:
                        ware.InAlmata -= mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                        break;
                    case 2:
                        ware.InAstana -= mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                        break;
                    case 3:
                        ware.InAktau -= mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                        break;
                }
            }
            using var db = new Data.ConDB();
            db.UpdateRange(Warehouses);
            db.SaveChanges();
        }

        public void ArriveGoods(Data.MainMove mainMove)
        {
            if (MessageBox.Show("Данным действием вы подтверждаете что товар пришел на склад, продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                foreach (var ware in Warehouses.Where(p => mainMove.MoveGoods.Select(p => p.WarehouseId).Contains(p.Id)))
                {
                    switch (mainMove.CityToId)
                    {
                        case 1:
                            ware.InAlmata += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                            break;
                        case 2:
                            ware.InAstana += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                            break;
                        case 3:
                            ware.InAktau += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Count).FirstOrDefault();
                            break;
                    }
                }
                using var db = new Data.ConDB();
                db.UpdateRange(Warehouses);
                db.SaveChanges();

                ViewModel.MainViewModel.MoveGoodsModel.RemoveMove(mainMove);            

            }
        }

        public List<Data.TypePay> GetTypePay()
        {
            return TypePay;
        }

        public Data.Warehouse GetWarehouseFromArticleAndDes(string article, string description)
        {
            return Warehouses.Where(p => p.Goods.Article.ToLower() == article.ToLower() && p.Goods.Description.ToLower() == description.ToLower()).FirstOrDefault();
        }

        public void UpdateWarehouse(Data.Warehouse warehouse)
        {
            using var db = new Data.ConDB();
            db.Warehouse.Update(warehouse);
            db.SaveChanges();
        }
    }

}