using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutoPartSystem.ViewModel;
using AutoPartSystem.ViewModel.Warehouse;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

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
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article, ObservableCollection<ViewModel.MarkModelFind> Brand);
        public ObservableCollection<WarehouseTable> GetWarehousesVirtualFilter(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
        public ObservableCollection<WarehouseTable> GetWarehousesFilterZav(ObservableCollection<ViewModel.MarkModelFind> model, ObservableCollection<ViewModel.MarkModelFind> desctiption, ObservableCollection<ViewModel.MarkModelFind> article);
        public void UpdateWarehouseCount(int almata, int astana, int ackau, int war_id);
        public void UpdateWarehouseMainPrice(int input_price, int rec_price, int ast_price, int akt_price, int id);
        public void UpdateWarehouse(WarehouseTable warehouse);
        public void UpdateWarehouse(Data.Warehouse warehouse);
        public void MoveOrderToWarehouse(Data.MainMove mainMove);
        public void ArriveGoods(Data.MainMove mainMove);
        public List<Data.TypePay> GetTypePay();
        public Data.Warehouse GetWarehouseFromArticleAndDes(string article, string description);
        public void UpdateGoodModel(ObservableCollection<Data.GoodsModel> goodsModels, ObservableCollection<Data.GoodsModel> goodsModelsRemove);
        public void UpdateGoodMode(Data.GoodsModel goodsModels);
        public void UpdateGoodImage(Data.GoodsImage goodsImage);
        public ViewModel.Warehouse.ImageGood GetGoodImage(int GoodId);
        public void UpdateAll();
        public ObservableCollection<WarehouseTable> GetWarehouseTextFilter(string Filter);
        public void UpdateBrandPrice(Data.Brand brand, int proc);
    }

    public class WarehouseModel : ReactiveObject,IWarehouseModel
    {
        private int update;
        public int Update
        {
            get=>this.update;
            set=>this.RaiseAndSetIfChanged(ref update, value);
        }
        private int updateVirt;
        public int UpdateVirt
        {
            get => this.update;
            set => this.RaiseAndSetIfChanged(ref update, value);
        }
        private ObservableCollection<WarehouseTable> _warehouses;
        public ObservableCollection<WarehouseTable> Warehouses
        {
            get => _warehouses;
            set => this.RaiseAndSetIfChanged(ref _warehouses, value);
        }
        private ObservableCollection<WarehouseTable> _warehouse_sale;
        public ObservableCollection<WarehouseTable> WarehousesSale
        {
            get => _warehouse_sale;
            set=>this.RaiseAndSetIfChanged(ref _warehouse_sale, value);
        }
        private ObservableCollection<WarehouseTable> _warehouse_filter;
        public ObservableCollection<WarehouseTable> WarehouseFilter
        {
            get => _warehouse_filter;
            set=>this.RaiseAndSetIfChanged(ref _warehouse_filter,value);
        }
        public int is_filter;
        public int is_filter_virt;
        private ObservableCollection<WarehouseTable> WarehouseVirtual;
        private ObservableCollection<WarehouseTable> _warehouse_virtual_sel;
        public ObservableCollection<WarehouseTable>  WarehouseVirtualSel
        {
            get=> _warehouse_virtual_sel;
            set => this.RaiseAndSetIfChanged(ref _warehouse_virtual_sel, value);
        }
        private ObservableCollection<WarehouseTable> _warehouse_virtual_filter_sel;
        public ObservableCollection<WarehouseTable> WarehouseVirtualFilterSel
        {
            get => _warehouse_virtual_sel;
            set => this.RaiseAndSetIfChanged(ref _warehouse_virtual_sel, value);
        }
        private List<Data.TypePay> TypePay; 
        private async Task getWareHouse()
        {
            await Task.Run(() => {
                using var db = new Data.ConDB();
                Warehouses = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.Brand).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == false && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
                if(Warehouses==null)
                {
                    Warehouses = new ObservableCollection<WarehouseTable>();
                }
                _ = _get_warehouse_from_db();
            });
        }
        private async Task getWareHouseVirt()
        {
            await Task.Run(() => {
                using var db = new Data.ConDB();
                WarehouseVirtual = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == true && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
                if (WarehouseVirtual == null)
                {
                    WarehouseVirtual = new ObservableCollection<WarehouseTable>();
                }
                WarehouseVirtualFilterSel = new ObservableCollection<WarehouseTable>();
                _ = _get_warehouse_from_db_virtual();
            });
        }
        private async Task _get_warehouse_from_db_virtual()
        {
            await Task.Run(() =>
            {
                while (true)
                {

                    using var db = new Data.ConDB();
                    try
                    {
                        var Warehousess = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.Brand).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == true && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());

                        while (WarehouseVirtual == null) { }
                        foreach (var Warehouse in WarehouseVirtual)
                        {

                            Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());

                        }
                        var not_in_ware = Warehousess.Where(p => !WarehouseVirtual.Select(s => s.Id).Contains(p.Id));
                        foreach (var ware in not_in_ware)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                WarehouseVirtual.Add(ware);
                            });
                        }
                        if (is_filter == 1)
                        {
                            foreach (var Warehouse in WarehouseVirtualFilterSel)
                            {
                                Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());
                            }
                        }
                        if (is_filter == 0)
                        {
                            foreach (var Warehouse in WarehouseVirtualFilterSel)
                            {
                                Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());
                            }
                            not_in_ware = Warehousess.Where(p => !WarehouseVirtualFilterSel.Select(s => s.Id).Contains(p.Id));
                            foreach (var ware in not_in_ware)
                            {
                                foreach (var item2 in ware.Goods.GoodsModel)
                                {
                                    var bi = WarehouseTable.NewTable(ware);
                                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                    {
                                        WarehouseVirtualFilterSel.Add(bi);
                                    });
                                }
                            }
                        }
                        UpdateVirt++;
                        Thread.Sleep(1000);
                    }
                    catch (Exception e) { Console.WriteLine(e.StackTrace); }
                }
            });
        }
        private async Task _get_warehouse_from_db()
        {
            await Task.Run(() =>
            {
                while(true)
                {
                
                using var db = new Data.ConDB();
                    try
                    {
                var Warehousess = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.Brand).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == false && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
                        
                        while (Warehouses == null) {}
                        foreach (var Warehouse in Warehouses)
                        {

                            Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());

                        }
                        var not_in_ware = Warehousess.Where(p => !Warehouses.Select(s => s.Id).Contains(p.Id));
                        foreach (var ware in not_in_ware)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                Warehouses.Add(ware);
                            });
                        }
                        if(is_filter==1)
                        {
                            foreach(var Warehouse in WarehouseFilter)
                            {
                                Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());
                            }
                        }
                        if(is_filter==2)
                        {
                            foreach(var Warehouse in WarehousesSale)
                            {
                                Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());
                            }
                            not_in_ware = Warehousess.Where(p => !WarehousesSale.Select(s => s.Id).Contains(p.Id));
                            foreach (var ware in not_in_ware)
                            {
                                foreach (var item2 in ware.Goods.GoodsModel)
                                {
                                    var bi = WarehouseTable.NewTable(ware);
                                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                    {
                                        WarehousesSale.Add(bi);
                                    });
                                }
                            }
                        }
                        Update++;
                        Thread.Sleep(1000);
                    }
                    catch(Exception e) { Console.WriteLine(e.Message);  }
                }
            });
        }
        private WarehouseViewModel _view_model;
        public WarehouseModel(ViewModel.WarehouseViewModel ViewModel)
        {
            Update = 0;
            UpdateVirt = 0;
            is_filter = 0;
            is_filter_virt = 0;
            _ =getWareHouse();
            _ = getWareHouseVirt();
            if (MainViewModel.Employee.SetCell && !MainViewModel.Employee.IsAdmin)
                _ = SaleWarehouse();
            
            _view_model = ViewModel;
            using var db = new Data.ConDB();          
            TypePay = db.TypePay.ToList();
            GetWarehouseVirtualFromDb();
           
        }
        public void UpdateAll()
        {
            GetWarehouseFromDb();
            GetWarehouseVirtualFromDb();
        }
        private void GetWarehouseFromDb()
        {
            using var db = new Data.ConDB();
            Warehouses = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.Brand).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == false && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
        }
        private void GetWarehouseVirtualFromDb()
        {
            using var db = new Data.ConDB();
            WarehouseVirtual = new ObservableCollection<WarehouseTable>(db.Warehouse.Include(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Goods.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsVirtual == true && p.IsDelete == false).Select(p => new WarehouseTable(p.Id, p.Goods, p.InAlmata, p.InAstana, p.InAktau, p.WarehousePlace, p.TypePay, p.Note, p.IsVirtual)).ToList());
        }
        public void AddWarehouse(Data.Warehouse warehouse)
        {
            using var db = new Data.ConDB();
            db.Warehouse.Add(warehouse);
            db.SaveChanges();
        }
        public ObservableCollection<WarehouseTable> GetAllWarehouse()
        {
            is_filter = 0;
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
        public ObservableCollection<WarehouseTable> GetWarehousesFilter(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article, ObservableCollection<MarkModelFind> Brand)
        {
            Console.WriteLine("####");
            Console.WriteLine(model.Where(p=>p.IsSelected==true).Count());
            Console.WriteLine(desctiption.Where(p => p.IsSelected == true).Count());
            Console.WriteLine(article.Where(p => p.IsSelected == true).Count());
            Console.WriteLine(Brand.Where(p => p.IsSelected == true).Count());
            Console.WriteLine("####");
            is_filter = 1;
            WarehouseFilter = new ObservableCollection<WarehouseTable>(Warehouses.Where(p => model.Any(m => m.IsSelected == true && p.Goods.GoodsModel.Select(p => p.ModelId).ToList().Contains(m.model_id)) && desctiption.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && p.IsVirtual == false &&  p.IsDelete==false && Brand.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Goods.BrandId)));
            
            if (!MainViewModel.Employee.SetCell)
            {
                
                return WarehouseFilter;
            }
            var WarehouseFilter1 = new ObservableCollection<WarehouseTable>(WarehouseFilter);
                
            WarehouseFilter = new ObservableCollection<WarehouseTable>();
            foreach (var item in WarehouseFilter1)
            {
                foreach (var item2 in item.Goods.GoodsModel.Where(p => model.Where(s => s.IsSelected == true).Select(s => s.model_id).Contains(p.ModelId)))
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    WarehouseFilter.Add(bi);
                }
            }
            Console.WriteLine(WarehouseFilter.Count);
                return WarehouseFilter;
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
        public void UpdateWarehouseMainPrice(int input_price, int rec_price, int ast_price, int akt_price, int war_id)
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
        public ObservableCollection<WarehouseTable> GetWarehouseUpdate()
        {
            Console.WriteLine(is_filter);
            if(is_filter==0)
            {
                return Warehouses;
            }
            if (is_filter == 1)
            {
                return WarehouseFilter;
            }
            if(is_filter==2)
            {
                return WarehousesSale;
            }
            return null;
        }
        public ObservableCollection<WarehouseTable> GetWarehouseVirtualTables()
        {
            is_filter_virt = 0;
            return WarehouseVirtualSel;
        }
        public ObservableCollection<WarehouseTable> GetWarehouseVirtualUpdate()
        {
            if (is_filter == 0)
            {
                return WarehouseVirtualSel;
            }
            if (is_filter == 1)
            {
                return WarehouseFilter;
            }
            return null;
        }
        public ObservableCollection<MarkModelFind> GetAllVirtualDesctiption(string name)
        {
            if (WarehouseVirtual == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            try
            {
                var a = new ObservableCollection<MarkModelFind>(WarehouseVirtual.Where(p => p.Goods.Description.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Goods.Description }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Description)).ToList());
                a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
                return a;
            } catch
            {
                return new ObservableCollection<MarkModelFind>();
            }

        }
        public ObservableCollection<MarkModelFind> GetAllVirtualArticle(string name)
        {
            if (WarehouseVirtual == null)
            {
                return new ObservableCollection<MarkModelFind>();
            }
            var a = new ObservableCollection<MarkModelFind>(WarehouseVirtual.Where(p => p.Note.ToLower().Contains(name.ToLower())).GroupBy(x => new { x.Id, x.Note }).Select(p => new MarkModelFind(p.Key.Id, p.Key.Note)).ToList());
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<WarehouseTable> GetWarehousesVirtualFilter(ObservableCollection<MarkModelFind> model, ObservableCollection<MarkModelFind> desctiption, ObservableCollection<MarkModelFind> article)
        {
            is_filter_virt = 1;
            var a = new ObservableCollection<WarehouseTable>(WarehouseVirtual.Where(p => model.Any(m => m.IsSelected == true && p.Goods.GoodsModel.Select(p => p.ModelId).ToList().Contains(m.model_id)) && desctiption.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id) && article.Where(m => m.IsSelected == true).Select(d => d.model_id).ToList().Contains(p.Id)));
            WarehouseVirtualFilterSel = new ObservableCollection<WarehouseTable>();
            foreach (var item in a)
            {
                foreach (var item2 in item.Goods.GoodsModel.Where(p => model.Where(s => s.IsSelected == true).Select(s => s.model_id).Contains(p.ModelId)))
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    WarehouseVirtualFilterSel.Add(bi);
                }
            }
            return WarehouseVirtualFilterSel;
        }
        private async Task SaleWarehouse()
        {
            await Task.Run(() => {
                while(Warehouses==null)
                {

                }
                WarehousesSale = new ObservableCollection<WarehouseTable>();
                is_filter = 2;
                foreach (var item in Warehouses)
                {
                    foreach (var item2 in item.Goods.GoodsModel)
                    {
                        var bi = WarehouseTable.NewTable(item);
                        bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            WarehousesSale.Add(bi);
                        });
                    }
                }
            });
           
        }
        public ObservableCollection<WarehouseTable> GetAllWarehouseForSale()
        {
            
            return WarehousesSale;
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
                using var db = new Data.ConDB();
                foreach (var ware in Warehouses.Where(p => mainMove.MoveGoods.Select(p => p.WarehouseId).Contains(p.Id)))
                {

                    var move = mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).FirstOrDefault();
                    if(move.Warehouse.Goods.CountCell>move.Count)
                    {
                        MessageBox.Show("Кол-во принимаемого товара не может быть больше отправленного", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                    switch (mainMove.CityToId)
                    {
                        case 1:
                            ware.InAlmata += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Warehouse.Goods.CountCell).FirstOrDefault();
                            break;
                        case 2:
                            ware.InAstana += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Warehouse.Goods.CountCell).FirstOrDefault();
                            break;
                        case 3:
                            ware.InAktau += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.Warehouse.Goods.CountCell).FirstOrDefault();
                            break;
                    }
                    //ware.Goods.GoodsModel = null;
                    
                    db.Warehouse.Update(ware);
                    db.SaveChanges();
                    move.Count -= move.Warehouse.Goods.CountCell;
                    if (move.Count == 0)
                    {
                        move.Warehouse = null;
                        mainMove.MoveGoods.Remove(move);
                        db.MoveGoods.Remove(move);
                        db.SaveChanges();
                        continue;
                    }
                    if (move.BackCount > move.Count)
                    {
                        MessageBox.Show("Кол-во возвращаемого товара не может быть больше отправленного", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                    Console.WriteLine(mainMove.CityFrom.Id);
                    switch (mainMove.CityFrom.Id)
                    {
                        case 1:
                            Console.WriteLine("dsdadas123123");
                            ware.InAlmata += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.BackCount).FirstOrDefault();
                            break;
                        case 2:
                            ware.InAstana += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.BackCount).FirstOrDefault();
                            break;
                        case 3:
                            ware.InAktau += mainMove.MoveGoods.Where(p => p.WarehouseId == ware.Id).Select(p => p.BackCount).FirstOrDefault();
                            break;
                    }
                    move.Count -= move.BackCount;
                    db.Warehouse.Update(ware);
                    db.SaveChanges();
                    if (move.Count==0)
                    {
                        move.Warehouse = null;
                        mainMove.MoveGoods.Remove(move);
                        db.MoveGoods.Remove(move);
                        db.SaveChanges();
                    }
                    else
                    {
                        move.Warehouse = null;
                        db.MoveGoods.Update(move);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
                if(mainMove.MoveGoods.Count==0)
                    ViewModel.MainViewModel.MoveGoodsModel.RemoveMove(mainMove);
                MainViewModel.GetMoveGoodsViewModel.UpdateMove();
            }
        }

        public List<Data.TypePay> GetTypePay()
        {
            return TypePay;
        }

        public Data.Warehouse GetWarehouseFromArticleAndDes(string article, string description)
        {
            return Warehouses.Where(p => p.Goods.Article.ToLower() == article.ToLower() && p.Goods.Description.ToLower() == description.ToLower() && p.IsDelete == false && p.IsVirtual == false).FirstOrDefault();
        }

        public void UpdateWarehouse(Data.Warehouse warehouse)
        {
            using var db = new Data.ConDB();
            db.Warehouse.Update(warehouse);
            db.SaveChanges();
        }
        public void UpdateGoodModel(ObservableCollection<Data.GoodsModel> goodsModels, ObservableCollection<Data.GoodsModel> goodsModelsRemove)
        {
            goodsModels = new ObservableCollection<Data.GoodsModel>(goodsModels.Select(p => new Data.GoodsModel { Id = p.Id, Model = new Data.Model(p.Model.Id, p.Model.Name, p.Model.MarkId, p.Model.Mark), GoodsId = p.GoodsId, ModelId = p.ModelId, }));
            foreach (var good in goodsModels)
            {
                if (good.Model != null)
                {
                    if (good.Model.Mark != null)
                    {
                        if (good.Model.Mark.Id != 0)
                        {
                            good.Model.MarkId = good.Model.Mark.Id;
                            good.Model.Mark = null;
                        }
                    }
                    if (good.Model.Id != 0)
                    {
                        good.ModelId = good.Model.Id;
                        good.Model = null;
                    }

                }
            }
            using var db = new Data.ConDB();
            if (goodsModelsRemove != null) db.GoodModel.RemoveRange(goodsModelsRemove);
            db.GoodModel.UpdateRange(goodsModels.Where(p => p.Id == 0).ToList());
            db.SaveChanges();
        }

        public void UpdateGoodImage(Data.GoodsImage goodsImage)
        {
            using var db = new Data.ConDB();
            if (goodsImage.Id == 0)
                db.Add(goodsImage);
            else
                db.Update(goodsImage);
            db.SaveChanges();
        }
        public ImageGood GetGoodImage(int GoodId)
        {
            using var db = new Data.ConDB();
            return db.GoodsImage.Where(p => p.GoodId == GoodId).Select(p => new ImageGood(p)).FirstOrDefault();
        }

        public void UpdateGoodMode(Data.GoodsModel goodsModels)
        {
            using var db = new Data.ConDB();
            db.GoodModel.Update(goodsModels);
            db.SaveChanges();
        }
        public ObservableCollection<WarehouseTable> GetWarehouseTextFilter(string Filter)
        {
            is_filter = 1;
            WarehouseFilter = new ObservableCollection<WarehouseTable>(Warehouses.Where(p =>
            p.Goods.GoodsModel.Any(g => g.Model.Name.ToLower().Contains(Filter.ToLower()) || g.Model.Mark.Name.ToLower().Contains(Filter.ToLower()))
           || p.Goods.Description.ToLower().Contains(Filter.ToLower())
           || p.Goods.Article.ToLower().Contains(Filter.ToLower())
           || p.Goods.Brand.Name.ToLower().Contains(Filter.ToLower())
           ||(p.Note!=null && p.Note.ToLower().Contains(Filter.ToLower()))

            ));
            if (!MainViewModel.Employee.SetCell)
            {
                return WarehouseFilter;
            }
            var WarehouseFilter1 = new ObservableCollection<WarehouseTable>(WarehouseFilter);
            WarehouseFilter = new ObservableCollection<WarehouseTable>();
            foreach(var item in WarehouseFilter1)
            {
                foreach (var item2 in item.Goods.GoodsModel)
                {
                    var bi = WarehouseTable.NewTable(item);
                    bi.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel> { item2 };
                    WarehouseFilter.Add(bi);
                }
            }
            return WarehouseFilter;
            
            
           
        }

        public void UpdateBrandPrice(Data.Brand brand, int proc)
        {
            var warehouse_brand = Warehouses.Where(p => p.Goods.BrandId == brand.Id);
            using var db = new Data.ConDB();
            foreach(var w_brand in warehouse_brand)
            {
                Console.WriteLine(w_brand.Goods.RecomPrice);
                w_brand.Goods.RecomPrice = w_brand.Goods.RecomPrice + ((w_brand.Goods.RecomPrice * proc) / 100);
                Console.WriteLine(w_brand.Goods.RecomPrice);
                db.Warehouse.Update(w_brand);
                db.SaveChanges();
            }
        }
    }

}