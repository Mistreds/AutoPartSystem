using AutoPartSystem.ViewModel;
using Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartSystem.Model.Warehouse
{
    public interface IWarehouseInvoce
    {
        public void SetWarehouse(ObservableCollection<WarehouseTable> warehouses);
        public ObservableCollection<WarehouseTable> GetWarehouseTables();
        public ObservableCollection<Data.Warehouse> GetWarehouse();
        public void CreateExcelFile(Data.Invoice invoice);
        public void AddInvoiceToDataBase (Data.Invoice invoice);
        public void AddInvoiceToDataBase(Data.Invoice invoice, int EmpId);
        public void UpdateInvoice(Data.Invoice invoice);  
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
        public void CreateExcelFile(Invoice Invoice)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var name = "text.xlsx";
            var newFile = new FileInfo(name);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("MySheet");
                sheet.Cells["A9:M9"].Merge = true;
                sheet.Cells["A9:M9"].Value = "Менеджер";
                for (int i = 1; i <= 60; i++)
                {
                    sheet.Column(i).Width = 4;

                }
                sheet.Column(39).Width = 8;
                sheet.Column(40).Width = 8;
                sheet.Cells["N9:AF9"].Merge = true;
                sheet.Cells["N9:AF9"].Value = MainViewModel.Employee.Name;
                sheet.Cells["N9:AF9"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                getAlign(sheet.Cells["N9:AF9"]);
                sheet.Cells["AN9"].Value = "Телефон";
                //Номер телефона
                sheet.Cells["AL9:AQ9"].Merge = true;
                sheet.Cells["AL9:AQ9"].Value = MainViewModel.Employee.PhoneNumber;
                getAllBorder(sheet.Cells["AL9:AQ9"]);
                //Номер документа
                sheet.Row(12).Height = 25;
                sheet.Cells["AL12:AM12"].Merge = true;
                sheet.Cells["AL12:AM12"].Value = "Номер документа";
                getAllBorder(sheet.Cells["AL12:AM12"]);

                sheet.Cells["AL13:AM13"].Merge = true;
                sheet.Cells["AL13:AM13"].Value = 1;
                getAllBorder(sheet.Cells["AL13:AM13"]);
                //Дата составления
                sheet.Cells["AN12:AQ12"].Merge = true;
                sheet.Cells["AN12:AQ12"].Value = "Дата составления";
                getAllBorder(sheet.Cells["AN12:AQ12"]);

                sheet.Cells["AN13:AQ13"].Merge = true;
                sheet.Cells["AN13:AQ13"].Value = Invoice.Date.ToString("dd.MM.yyyy");
                getAllBorder(sheet.Cells["AN13:AQ13"]);

                sheet.Cells["A15:AQ15"].Merge = true;
                sheet.Cells["A15:AQ15"].Value = "НАКЛАДНАЯ НА ОТПУСК ЗАПАСОВ НА СТОРОНУ";
                getAlign(sheet.Cells["A15:AQ15"]);
                //Таблица клиента
                //Марка
                sheet.Row(19).Height = 45;
                sheet.Cells["A18:K18"].Merge = true;
                sheet.Cells["A18:K18"].Value = "Марка";
                getAllBorder(sheet.Cells["A18:K18"]);
                sheet.Cells["A19:K19"].Merge = true;
                sheet.Cells["A19:K19"].Value = Invoice.Client.Model.Mark.Name;
                getAllBorder(sheet.Cells["A19:K19"]);
                //Модель
                sheet.Cells["L18:V18"].Merge = true;
                sheet.Cells["L18:V18"].Value = "Модель";
                getAllBorder(sheet.Cells["L18:V18"]);
                sheet.Cells["L19:V19"].Merge = true;
                sheet.Cells["L19:V19"].Value = Invoice.Client.Model.Name;
                getAllBorder(sheet.Cells["L19:V19"]);
                //ФИО
                sheet.Cells["W18:AE18"].Merge = true;
                sheet.Cells["W18:AE18"].Value = "ФИО";
                getAllBorder(sheet.Cells["W18:AE18"]);
                sheet.Cells["W19:AE19"].Merge = true;
                sheet.Cells["W19:AE19"].Value = Invoice.Client.Name;
                getAllBorder(sheet.Cells["W19:AE19"]);
                //Телефон
                sheet.Cells["AF18:AK18"].Merge = true;
                sheet.Cells["AF18:AK18"].Value = "Телефон";
                getAllBorder(sheet.Cells["AF18:AK18"]);
                sheet.Cells["AF19:AK19"].Merge = true;
                sheet.Cells["AF19:AK19"].Value = Invoice.Client.PhoneName;
                getAllBorder(sheet.Cells["AF19:AK19"]);
                //Город
                sheet.Cells["AL18:AQ18"].Merge = true;
                sheet.Cells["AL18:AQ18"].Value = "Город";
                getAllBorder(sheet.Cells["AL18:AQ18"]);
                sheet.Cells["AL19:AQ19"].Merge = true;
                sheet.Cells["AL19:AQ19"].Value = Invoice.Client.City.Name;
                getAllBorder(sheet.Cells["AL19:AQ19"]);
                //таблица с товаром
                //номер по порядку
                sheet.Cells["A21:B22"].Merge = true;
                sheet.Cells["A21:B22"].Value = "№";
                getAllBorder(sheet.Cells["A21:B22"]);
                //Код товара
                sheet.Cells["C21:N22"].Merge = true;
                sheet.Cells["C21:N22"].Value = "Код товара";
                getAllBorder(sheet.Cells["C21:N22"]);
                //Наименование
                sheet.Cells["O21:U22"].Merge = true;
                sheet.Cells["O21:U22"].Value = "Наименование";
                getAllBorder(sheet.Cells["O21:U22"]);
                //Единица измерения
                sheet.Cells["V21:AB22"].Merge = true;
                sheet.Cells["V21:AB22"].Value = "Модель";
                getAllBorder(sheet.Cells["V21:AB22"]);
                //Количество
                sheet.Cells["AC21:AE22"].Merge = true;
                sheet.Cells["AC21:AE22"].Value = "Количество";
                getAllBorder(sheet.Cells["AC21:AE22"]);
                //Цена за единицу
                sheet.Cells["AF21:AK22"].Merge = true;
                sheet.Cells["AF21:AK22"].Value = "Цена за единицу";
                getAllBorder(sheet.Cells["AF21:AK22"]);
                //Сумма
                sheet.Cells["AL21:AQ22"].Merge = true;
                sheet.Cells["AL21:AQ22"].Value = "Сумма";
                getAllBorder(sheet.Cells["AL21:AQ22"]);
                int k = 1;
                int j = 23;
                foreach (var goods in Invoice.GoodsInvoice)
                {
                    sheet.Row(j).Height = 30;
                    //номер по порядку
                    sheet.Cells[$"A{j}:B{j}"].Merge = true;
                    sheet.Cells[$"A{j}:B{j}"].Value = k.ToString(); ;
                    getAllBorder(sheet.Cells[$"A{j}:B{j}"]);
                    sheet.Cells[$"C{j}:N{j}"].Merge = true;
                    sheet.Cells[$"C{j}:N{j}"].Value = goods.Goods.Article;
                    getAllBorder(sheet.Cells[$"C{j}:N{j}"]);
                    sheet.Cells[$"O{j}:U{j}"].Merge = true;
                    sheet.Cells[$"O{j}:U{j}"].Value = goods.Goods.Description;
                    getAllBorder(sheet.Cells[$"O{j}:U{j}"]);
                    sheet.Cells[$"V{j}:AB{j}"].Merge = true;
                    sheet.Cells[$"V{j}:AB{j}"].Value = $"{goods.Goods.GoodsModel.FirstOrDefault().Model.Mark.Name} {goods.Goods.GoodsModel.FirstOrDefault().Model.Name}";
                    getAllBorder(sheet.Cells[$"V{j}:AB{j}"]);
                    sheet.Cells[$"AC{j}:AE{j}"].Merge = true;
                    sheet.Cells[$"AC{j}:AE{j}"].Value = goods.Count;
                    getAllBorder(sheet.Cells[$"AC{j}:AE{j}"]);
                    sheet.Cells[$"AF{j}:AK{j}"].Merge = true;
                    sheet.Cells[$"AF{j}:AK{j}"].Value = goods.Goods.PriceCell;
                    getAllBorder(sheet.Cells[$"AF{j}:AK{j}"]);
                    sheet.Cells[$"AL{j}:AQ{j}"].Merge = true;
                    sheet.Cells[$"AL{j}:AQ{j}"].Value = goods.AllPrice;
                    getAllBorder(sheet.Cells[$"AL{j}:AQ{j}"]);
                    k++;
                    j++;
                }
                sheet.Cells[$"AB{j}"].Value = "Итого";
                sheet.Column(28).Width = 8;
                getAlign(sheet.Cells[$"AB{j}"]);
                sheet.Cells[$"AC{j}:AE{j}"].Merge = true;
                sheet.Cells[$"AC{j}:AE{j}"].Value = Invoice.AllCount;
                getAllBorder(sheet.Cells[$"AC{j}:AE{j}"]);

                sheet.Cells[$"AF{j}:AK{j}"].Merge = true;
                getAllBorder(sheet.Cells[$"AF{j}:AK{j}"]);
                sheet.Cells[$"AL{j}:AQ{j}"].Merge = true;
                sheet.Cells[$"AL{j}:AQ{j}"].Value = Invoice.AllPrice;
                getAllBorder(sheet.Cells[$"AL{j}:AQ{j}"]);
                package.Save();
            }
        }
        private ExcelRange getAllBorder(ExcelRange cell)
        {
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.WrapText = true;
            return cell;
        }
        private ExcelRange getAlign(ExcelRange cell)
        {
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cell.Style.WrapText = true;
            return cell;
        }
        public void AddInvoiceToDataBase(Invoice invoice, int EmpId)
        {
            var inv = new Data.Invoice(invoice);
            using var db = new Data.ConDB();
            var ware = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p => invoice.GoodsInvoice.Select(s => s.Goods.WarehouseId).Contains(p.Id)).Select(p => WarehouseTable.NewTable(p)));
            bool is_have_good = true;
            foreach (var invo in invoice.GoodsInvoice)
            {
                if (invo.Goods.Warehouse.IsVirtual == true) continue;
                var war = ware.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();
                switch(MainViewModel.Employee.CityId)
                {
                    case 1:
                        if (war.InAlmata < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                    case 2:
                        if (war.InAstana < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                    case 3:
                        if (war.InAktau < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                }
               
            }
            if (is_have_good)
            {
                if (inv.IsInvoice)
                {
                    var wares = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p => invoice.GoodsInvoice.Select(s => s.Goods.WarehouseId).Contains(p.Id)));
                    foreach (var invo in invoice.GoodsInvoice)
                    {
                        MainViewModel.AdminModel.UpdateCash(invo.AllPrice, $"Оплата товара {invo.Goods.Description}", MainViewModel.SelMain().Employee2, "Оплата товара");
                        if (invo.Goods.Warehouse.IsVirtual == true) continue;
                        var war = wares.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();
                        switch (MainViewModel.Employee.CityId)
                        {
                            case 1:
                                war.InAlmata -= invo.Count;
                                break;
                            case 2:
                                war.InAstana -= invo.Count;
                                break;
                            case 3:
                                war.InAktau -= invo.Count;
                                break;
                        }
                        
                    }
                    db.Warehouse.UpdateRange(wares);    
                }
                db.Invoices.Add(inv);
                db.SaveChanges();
                if(inv.IsInvoice)
                {
                    MarzhEmployee marzhEmployee = new MarzhEmployee(inv.AllMarz, inv.Id, EmpId );
                    db.Add(marzhEmployee);
                    db.SaveChanges();
                }
                invoice.Id = inv.Id;
            }
            else
            {
                MessageBox.Show("Выделенных товаров не хватает на складе", "Ошибка");
            }
        }
        public void AddInvoiceToDataBase(Invoice invoice)
        {
            var inv = new Data.Invoice(invoice);
            using var db = new Data.ConDB();
            var ware = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p=>invoice.GoodsInvoice.Select(s=>s.Goods.WarehouseId).Contains(p.Id)).Select(p=>WarehouseTable.NewTable(p)));
            bool is_have_good = true;
            foreach(var invo in invoice.GoodsInvoice)
            {
                if (invo.Goods.Warehouse.IsVirtual == true) continue;
                var war = ware.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();

                switch (MainViewModel.Employee.CityId)
                {
                    case 1:
                        if (war.InAlmata < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                    case 2:
                        if (war.InAstana < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                    case 3:
                        if (war.InAktau < invo.Count)
                        {
                            invo.DontHaveGoods = true;
                            is_have_good = false;
                        }
                        break;
                }
            }
            if(is_have_good)
            {
                if(inv.IsInvoice)
                {
                    var wares = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p => invoice.GoodsInvoice.Select(s => s.Goods.WarehouseId).Contains(p.Id)));
                    foreach (var invo in invoice.GoodsInvoice)
                    {
                        if(invo.Goods.TypePayId==1)
                        {
                            MainViewModel.AdminModel.UpdateCash(invo.AllPrice,"Оплата товара", MainViewModel.SelMain().Employee2,"Оплата товара");
                        }
                        if (invo.Goods.Warehouse.IsVirtual == true) continue;
                        var war = wares.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();
                        switch (MainViewModel.Employee.CityId)
                        {
                            case 1:
                                war.InAlmata -= invo.Count;
                                break;
                            case 2:
                                war.InAstana -= invo.Count;
                                break;
                            case 3:
                                war.InAktau -= invo.Count;
                                break;
                        }
                    }
                    db.Warehouse.UpdateRange(wares);    
                }
                db.Invoices.Add(inv);
                db.SaveChanges();
                invoice.Id = inv.Id;
            }
            else
            {
                MessageBox.Show("Выделенных товаров не хватает на складе", "Ошибка");
            }
        }
        public void UpdateInvoice(Invoice invoice)
        {
            using var db = new Data.ConDB();
            {

                var ware = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p => invoice.GoodsInvoice.Select(s => s.Goods.WarehouseId).Contains(p.Id)).Select(p => WarehouseTable.NewTable(p)));
                bool is_have_good = true;
                foreach (var invo in invoice.GoodsInvoice)
                {
                    var war = ware.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();
                    war.InAlmata -= invo.Count;
                    if (war.InAlmata < 0)
                    {
                        invo.DontHaveGoods = true;
                        is_have_good = false;
                    }
                }
                if (is_have_good)
                {
                    if (invoice.IsInvoice)
                    {
                        var wares = new ObservableCollection<WarehouseTable>(ViewModel.MainViewModel.WarehouseModel.GetAllWarehouse().Where(p => invoice.GoodsInvoice.Select(s => s.Goods.WarehouseId).Contains(p.Id)));
                        foreach (var invo in invoice.GoodsInvoice)
                        {
                            var war = wares.Where(p => p.Id == invo.Goods.WarehouseId).FirstOrDefault();
                            war.InAlmata -= invo.Count;
                        }
                        db.Warehouse.UpdateRange(wares);
                    }
                    var inv = new Data.Invoice(invoice);
                    db.Invoices.Update(inv);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выделенных товаров не хватает на складе", "Ошибка");
                }
            }
            Console.WriteLine("dsadasd");
        }

       
    }
}

