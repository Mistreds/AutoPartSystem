using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using OfficeOpenXml;
using ReactiveUI;
namespace AutoPartSystem.ViewModel.Warehouse
{
    public class AddFromExcelViewModel:ReactiveObject
    {
        private string _file_name;
        public  string FileName
        {
            get=> _file_name;
            set=>this.RaiseAndSetIfChanged(ref _file_name, value);
        }
        public List<Data.City> Cities
        {
            get; private set;
        }
        private int _city_id;
        public int CityId
        {
            get=> _city_id;
            set=>this.RaiseAndSetIfChanged(ref _city_id, value);
        }
        private Model.Warehouse.WarehouseModel WarehouseModel;
        private Model.MarkModel.MarkModel MarkModel;
        private double progress;
        public double Progress
        {
            get => progress;
            set=>this.RaiseAndSetIfChanged(ref progress, value);
        }
       
        public AddFromExcelViewModel(Model.Warehouse.WarehouseModel WarehouseModel, Model.MarkModel.MarkModel MarkModel)
        {
            Cities = MainViewModel.AdminModel.GetEmpCity();
            FileName = "";
            this.WarehouseModel = WarehouseModel;
            this.MarkModel = MarkModel;
        }

      
        public ReactiveCommand<Unit, Unit> SelectFile => ReactiveCommand.Create(() => {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel файлы(*.xlsx)|*.xlsx" };
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        });
        public ReactiveCommand<Unit, Unit> AddToWarehouse => ReactiveCommand.CreateFromTask(AddToWarehouseStart);
        private async Task AddToWarehouseStart()
        {
            await Task.Run(() => {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPack = new ExcelPackage())
                {
                    //Load excel stream
                    FileInfo fileInfo = new FileInfo(FileName);
                    ExcelPackage package = new ExcelPackage(fileInfo);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    // get number of rows and columns in the sheet
                    int rows = worksheet.Dimension.Rows; // 20
                    int columns = worksheet.Dimension.Columns; // 7
                    Progress = 0;
                    double step = (double)100/ (double)(rows - 1);
                    // loop through the worksheet rows and columns
                    ObservableCollection<Data.Warehouse> warehouses = new ObservableCollection<Data.Warehouse>();
                    for (int i = 2; i <= rows; i++)
                    {
                        Data.Warehouse warehouse = new Data.Warehouse();
                        warehouse.Goods = new Data.Goods();
                        warehouse.Goods.GoodsModel = new ObservableCollection<Data.GoodsModel>();
                        Data.GoodsModel goodsModel = new Data.GoodsModel();
                        Data.Model model = new Data.Model();
                        for (int j = 2; j <= columns; j++)
                        {
                            if (worksheet.Cells[i, j].Value == null) continue;
                            string content = worksheet.Cells[i, j].Value.ToString();
                            switch (j)
                            {
                                case 2:
                                    Data.Brand brand = MarkModel.GetBrandFind(content);
                                    if (brand != null)
                                    {
                                        warehouse.Goods.BrandId = brand.Id;
                                    }
                                    else
                                    {
                                        warehouse.Goods.Brand = new Data.Brand(content);
                                    }
                                    break;
                                case 3:
                                    Data.Mark Mark = MarkModel.GetMarkFromNameFind(content);
                                    if (Mark != null)
                                    {
                                        model.MarkId = Mark.Id;
                                    }
                                    else
                                    {
                                        model.Mark = new Data.Mark(content);
                                    }
                                    break;
                                case 4:
                                    if (model.MarkId == 0)
                                    {
                                        model.Name = content;
                                        goodsModel.Model = model;
                                        warehouse.Goods.GoodsModel.Add(goodsModel);
                                    }
                                    else
                                    {
                                        Data.Model mod = MarkModel.GetModelFromNameFind(content, model.MarkId);
                                        if (mod != null)
                                        {
                                            goodsModel.ModelId = mod.Id;
                                            warehouse.Goods.GoodsModel.Add(goodsModel);
                                        }
                                        else
                                        {
                                            model.Name = content;
                                            goodsModel.Model = model;
                                            warehouse.Goods.GoodsModel.Add(goodsModel);
                                        }
                                    }
                                    break;
                                case 5:
                                    warehouse.Goods.Description = content;
                                    break;
                                case 6:
                                    warehouse.Goods.Article = content;
                                    break;
                                case 7:
                                    warehouse.Note = content;
                                    break;
                                case 8:
                                    switch (CityId)
                                    {
                                        case 1:
                                            warehouse.InAlmata = Convert.ToInt32(content);
                                            break;
                                        case 2:
                                            warehouse.InAstana = Convert.ToInt32(content);
                                            break;
                                        case 3:
                                            warehouse.InAktau = Convert.ToInt32(content);
                                            break;
                                    }
                                    break;
                                case 9:
                                    warehouse.WarehousePlace = content;
                                    break;
                                case 10:
                                    warehouse.Goods.InputPrice = Convert.ToInt32(content.Replace(" ",""));
                                    break;
                                case 11:
                                    warehouse.Goods.RecomPrice = Convert.ToInt32(content.Replace(" ", ""));
                                    break;
                                case 12:
                                    warehouse.Goods.InputAstana=Convert.ToInt32(content.Replace(" ", ""));
                                    break;
                                case 13:
                                    warehouse.Goods.InputAktau=Convert.ToInt32(content.Replace(" ", ""));
                                    break;

                            }
                        }
                        Data.Warehouse war_update = WarehouseModel.GetWarehouseFromArticleAndDes(warehouse.Goods.Article, warehouse.Goods.Description);
                        if (war_update != null)
                        {
                            switch (CityId)
                            {
                                case 1:
                                    war_update.InAlmata += warehouse.InAlmata;
                                    break;
                                case 2:
                                    war_update.InAstana += warehouse.InAstana;
                                    break;
                                case 3:
                                    war_update.InAktau += warehouse.InAktau;
                                    break;
                            }
                            war_update.Goods.InputPrice = warehouse.Goods.InputPrice;
                            war_update.Goods.RecomPrice = warehouse.Goods.RecomPrice;
                            war_update.Goods.InputAktau = warehouse.Goods.InputAktau;
                            war_update.Goods.InputAstana = warehouse.Goods.InputAstana;
                            WarehouseModel.UpdateWarehouse(war_update);
                        }
                        else
                        {
                            WarehouseModel.AddWarehouse(warehouse);
                        }
                        MarkModel.UpdateAll();
                        Progress += step;

                    }            
                    Progress = 100;
                    WarehouseModel.UpdateAll();
                    MainViewModel.WarehouseViewModel.UpdateTableCom();
                    MessageBox.Show("Загрузка данных завершена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
    }
}
