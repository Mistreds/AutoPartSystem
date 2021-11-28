using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using OfficeOpenXml;
using System.Threading.Tasks;
using ReactiveUI;
using System.IO;
using OfficeOpenXml.Style;

namespace AutoPartSystem.ViewModel
{
    
    public class InvoiceWinViewModel:ReactiveObject
    {
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Invoice invoice;
        public Data.Invoice Invoice
        {
            get=> invoice;
            set=>this.RaiseAndSetIfChanged(ref invoice, value);
        }
        private ObservableCollection<Data.Model>? _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set => this.RaiseAndSetIfChanged(ref _models, value);
        }
        private ObservableCollection<Data.Mark>? _mark;
        public ObservableCollection<Data.Mark>? Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private List<Data.City> _cities;
        public List<Data.City> Cities
        {
            get => _cities;
        }

        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel, Model.MarkModel.MarkModel MarkModel)
        {
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            this.MarkModel = MarkModel;
            Mark = MarkModel.GetMark();
            _cities=MainViewModel.AdminModel.GetCities();
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()),MainViewModel.Employee);
            this.WhenAnyValue(vm => vm.Invoice.Client.Mark).Subscribe(x => UpdateModels(x.Id));
            View.Warehouse.InvoiceGood invoiceGood = new View.Warehouse.InvoiceGood(this);
            invoiceGood.Show();
        }
        private void UpdateModels(int mark_id)
        {
            Models=MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<Unit, Unit> SelectNewClient => ReactiveCommand.Create(() => {

            View.Client.ClientWindow clientWindow = new View.Client.ClientWindow(MainViewModel.ClientModel.GetClient(), Invoice);
            clientWindow.Show();
        });
        public ReactiveCommand<Unit, Unit> CreateInvoiceExcel => ReactiveCommand.Create(() =>
          {
              ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
              var name = "text.xlsx";
          var newFile = new FileInfo(name);
              using (ExcelPackage package = new ExcelPackage(newFile))
              {
                  ExcelWorksheet sheet = package.Workbook.Worksheets.Add("MySheet");

                 
                  sheet.Cells["A9:M9"].Merge = true;
                  sheet.Cells["A9:M9"].Value = "Менеджер";
                  for (int i =  1;i<=60;i++)
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
                  foreach(var goods in Invoice.GoodsInvoice)
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
                      sheet.Cells[$"V{j}:AB{j}"].Value = "Модель";
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

          });
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
    }
}
