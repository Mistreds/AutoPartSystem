using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.ViewModel
{
    public class ReportViewModel : ReactiveObject
    {
        private ObservableCollection<Data.Employee> _employers_table;
        public ObservableCollection<Data.Employee> EmployersTable
        {
            get => _employers_table;
            set => this.RaiseAndSetIfChanged(ref _employers_table, value);
        }
        private int _emp_day;
        public int EmpDay
        {
            get => _emp_day;
            set => this.RaiseAndSetIfChanged(ref _emp_day, value);
        }
        private DateTime _date_day;
        public DateTime DateDay
        {
            get => _date_day;
            set => this.RaiseAndSetIfChanged(ref _date_day, value);
        }
        public ReportViewModel()
        {
            EmployersTable = MainViewModel.AdminModel.GetManagerEmp();
        }
        public ReactiveCommand<Unit, Unit> CreateDayReport => ReactiveCommand.Create(() => {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var name = "text1.xlsx";
            var newFile = new FileInfo(name);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                using var db = new Data.ConDB();
                var emp = db.Employees.Include(p => p.City).Where(p => p.Id == EmpDay).FirstOrDefault();
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add(emp.Name);
                double input = 0;
                double all_marz = 0;
                var date = new DateTime(DateDay.Year, DateDay.Month, DateDay.Day);
                var cash = db.CashDay.Where(p => p.Date == date && p.EmployeeId == EmpDay).FirstOrDefault();
                if (cash == null)
                    cash = new Data.CashDay(0, EmpDay);
                sheet.Cells["B1"].Value = $"{DateDay.ToString("dd-MM-yyyy")} - касса {cash.Cash}";
                sheet.Cells["A3"].Value = "№";
                sheet.Cells["B3"].Value = "описание товара";
                sheet.Cells["C3"].Value = "склад";
                sheet.Cells["D3"].Value = "количества";
                sheet.Cells["E3"].Value = "сумма ";
                sheet.Cells["F3"].Value = "поступление";
                sheet.Cells["G3"].Value = "маржа";
                sheet.Cells["H3"].Value = "";
                var date_start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                var date_end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                var invoices = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= date_start && p.Date <= date_end && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpDay).ToList();
                int i = 4;
                int i1 = 1;
                foreach (var inv in invoices)
                {
                    foreach (var good in inv.GoodsInvoice)
                    {
                        sheet.Cells[$"A{i}"].Value = i1;
                        sheet.Cells[$"B{i}"].Value = good.Goods.Description;
                        if (good.Goods.Warehouse.IsVirtual == true)
                        {
                            sheet.Cells[$"C{i}"].Value = $"Виртуальный склад магазин {good.Goods.Warehouse.Note}";
                        }

                        else
                        {
                            sheet.Cells[$"C{i}"].Value = emp.City.Name;

                        }

                        sheet.Cells[$"D{i}"].Value = good.Count;
                        sheet.Cells[$"E{i}"].Value = good.AllPrice;
                        if (good.TypePayId == 1)
                        {
                            sheet.Cells[$"F{i}"].Value = good.AllPrice;
                            input += good.AllPrice;
                        }
                        else
                        {
                            sheet.Cells[$"F{i}"].Value = "Счет";
                        }

                        if (inv.IsDelMarzh)
                        {
                            sheet.Cells[$"G{i}"].Value = good.Marz / 2;
                            var marz_emp = db.MarzhEmployee.Include(p => p.Employee).Where(p => p.InvoiceId == inv.Id).FirstOrDefault();
                            sheet.Cells[$"H{i}"].Value = $"Доля 50%\\{marz_emp.Employee.Name}";
                            all_marz += good.Marz / 2;
                        }
                        else
                        {
                            sheet.Cells[$"G{i}"].Value = good.Marz;
                            all_marz += good.Marz;

                        }
                        i++;
                        i1++;

                    }

                }
                var marz_emps = db.MarzhEmployee.Include(p => p.Invoice).ThenInclude(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.Invoice.Employee).ThenInclude(p => p.City).Where(p => p.Invoice.Date >= date_start && p.Invoice.Date <= date_end && p.EmployeeId == EmpDay).ToList();
                foreach (var mar in marz_emps)
                {
                    foreach (var good in mar.Invoice.GoodsInvoice)
                    {
                        sheet.Cells[$"A{i}"].Value = i1;
                        sheet.Cells[$"B{i}"].Value = good.Goods.Description;
                        if (good.Goods.Warehouse.IsVirtual == true)
                        {
                            sheet.Cells[$"C{i}"].Value = $"Виртуальный склад магазин {good.Goods.Warehouse.Note}";
                        }

                        else
                        {
                            sheet.Cells[$"C{i}"].Value = mar.Employee.City.Name;

                        }

                        sheet.Cells[$"D{i}"].Value = good.Count;
                        sheet.Cells[$"E{i}"].Value = good.AllPrice;

                        sheet.Cells[$"F{i}"].Value = "-";
                        sheet.Cells[$"G{i}"].Value = good.Marz / 2;

                        sheet.Cells[$"H{i}"].Value = $"Доля 50%\\{mar.Employee.Name}";
                        all_marz += good.Marz / 2;

                        i++;
                        i1++;

                    }
                    
                }
                i++;
                sheet.Cells[$"F{i}"].Value = input;
                sheet.Cells[$"G{i}"].Value = all_marz;
                i++;
                sheet.Cells[$"B{i}"].Value ="Изменение в кассе";
                i++;
                sheet.Cells[$"B{i}"].Value = $"{cash.Cash}+{input}={cash.Cash+input}";
                input += cash.Cash;
                i++;
                var in_out_cash = db.InsertOutCash.Where(p => p.EmployeeId == EmpDay && p.Date >= date_start && p.Date <= date_end && (p.Type == "Добавить" || p.Type == "Вывод")).ToList();
                foreach(var cash_in_out in in_out_cash)
                {
                    input+=cash_in_out.Cash;
                    sheet.Cells[$"B{i}"].Value = $"{cash_in_out.Type} причина {cash_in_out.Name} сумма {cash_in_out.Cash} касса {input} ";
                    i++;
                }
                package.Save();
            }


        });
    }
}
