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
                      sheet.Column(i).Width = 3;
                      
                  }
                  sheet.Cells["N9:AJ9"].Merge = true;
                  sheet.Cells["N9:AJ9"].Value = MainViewModel.Employee.Name;
                  sheet.Cells["N9:AJ9"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                  sheet.Cells["AN9"].Value = "Телефон";
                  //Номер телефона
                  sheet.Cells["AQ9:AW9"].Merge = true;
                  sheet.Cells["AQ9:AW9"].Value = MainViewModel.Employee.PhoneNumber;
                  getAllBorder(sheet.Cells["AQ9:AW9"]);
                  //Номер документа
                  sheet.Cells["AP12:AS12"].Merge = true;
                  sheet.Cells["AP12:AS12"].Value = "Номер документа";
                  getAllBorder(sheet.Cells["AP12:AS12"]);

                  sheet.Cells["AP13:AS13"].Merge = true;
                  sheet.Cells["AP13:AS13"].Value = 1;
                  getAllBorder(sheet.Cells["AP13:AS13"]);
                  //Дата составления
                  sheet.Cells["AT12:AW12"].Merge = true;
                  sheet.Cells["AT12:AW12"].Value = "Дата составления";
                  getAllBorder(sheet.Cells["AT12:AW12"]);

                  sheet.Cells["AT13:AW13"].Merge = true;
                  sheet.Cells["AT13:AW13"].Value = Invoice.Date.ToString("dd.MM.yyyy");
                  getAllBorder(sheet.Cells["AT13:AW13"]);

                  sheet.Cells["A15:AW15"].Merge = true;
                  sheet.Cells["A15:AW15"].Value = "НАКЛАДНАЯ НА ОТПУСК ЗАПАСОВ НА СТОРОНУ";
                  //Таблица клиента
                  //Марка
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
                  sheet.Cells["AF18:AN18"].Merge = true;
                  sheet.Cells["AF18:AN18"].Value = "Телефон";
                  getAllBorder(sheet.Cells["AF18:AN18"]);
                  sheet.Cells["AF19:AN19"].Merge = true;
                  sheet.Cells["AF19:AN19"].Value = Invoice.Client.PhoneName;
                  getAllBorder(sheet.Cells["AF19:AN19"]);
                  //Город
                  sheet.Cells["AO18:AW18"].Merge = true;
                  sheet.Cells["AO18:AW18"].Value = "Город";
                  getAllBorder(sheet.Cells["AO18:AW18"]);
                  sheet.Cells["AO19:AW19"].Merge = true;
                  sheet.Cells["AO19:AW19"].Value = Invoice.Client.City.Name;
                  getAllBorder(sheet.Cells["AO19:AW19"]);
                  package.Save();
              }

          });
       private ExcelRange getAllBorder(ExcelRange cell)
        {
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            return cell;
        }
    }
}
