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
            DateDay = DateTime.Now;
        }
        public ReactiveCommand<Unit, Unit> CreateDayReport => ReactiveCommand.Create(() => {

            var saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "Excel (*.xlsx)|*.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;
            var newFile = new FileInfo(saveFileDialog1.FileName);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                using var db = new Data.ConDB();
                var emp = db.Employees.Include(p => p.City).Where(p => p.Id == EmpDay).FirstOrDefault();
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add(emp.Name);
                int input = 0;
                int all_marz = 0;
                int account_cashe = 0;
                var date = new DateTime(DateDay.Year, DateDay.Month, DateDay.Day);
                var day_start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                var day_end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                var openCloseCash = db.OpenCloseCash.Where(p => p.EmployeeId == EmpDay && p.OpenDate >= day_start && p.OpenDate <= day_end && p.Status == 2).FirstOrDefault();
                int cash = 0;
                if (openCloseCash == null)
                    sheet.Cells["B1"].Value = $"{DateDay.ToString("dd-MM-yyyy")} - касса не открыта, либо не закрыта";
                else
                {
                    sheet.Cells["B1"].Value = $"{DateDay.ToString("dd-MM-yyyy")} - касса  {openCloseCash.OpenCash}";
                    cash = openCloseCash.OpenCash;
                }
                    
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
                var invoices = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpDay).ToList();
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
                            account_cashe += good.AllPrice;
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
                if(openCloseCash == null  || openCloseCash.CloseData<openCloseCash.OpenDate)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge=true;
                    sheet.Cells[$"B{i}:G{i}"].Value = "Касса за день не была открыта, либо не была закрыта";
                    package.Save();
                    return;
                }
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value ="Изменение в кассе при открытии кассы";
                i++;
                var add_to_open_cash=db.InsertOutCash.Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && (p.Type == "Добавление в кассу")).FirstOrDefault();
                if(add_to_open_cash==null)
                {
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"Добавление в кассу {add_to_open_cash.OldCash} + {add_to_open_cash.Cash} = {add_to_open_cash.NewCash}";
                    i++;
                }
                var expenses_open = db.Expenses.Include(p=>p.TypeExpenses).Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && p.IsOpenDay == true && p.TypeExpensesId!=5).ToList();
                int cash_dont_rash=0;
                if(expenses_open.Count>0)
                {
                    cash_dont_rash=expenses_open.Sum(p=>p.Cash);
                    cash_dont_rash += openCloseCash.OpenCash;
                }
                
                foreach(var e in expenses_open)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"Расходы {cash_dont_rash}-{e.Cash}-{cash_dont_rash -= e.Cash} причина {e.Name} {e.TypeExpenses.Name}";
                    i++;
                }
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"Открытие кассы на {openCloseCash.OpenDate.ToString("dd-MM-yyyy")} - {openCloseCash.OpenCash}";
                i++;
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"Изменения в кассе, расчитанные программой:";
                i++;
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"{cash} + {input} = {input+=cash}";
                i++;
                var in_out_cash = db.InsertOutCash.Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && (p.Type == "Добавить" || p.Type == "Вывод")).ToList();
                cash_dont_rash =input; 
                foreach (var cash_in_out in in_out_cash)
                {
                    input+=cash_in_out.Cash;
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"{cash_in_out.Type} причина {cash_in_out.Name} сумма {cash_in_out.Cash} касса {input} дата и время {cash_in_out.Date.ToString("dd.MM.yyyy HH:mm:ss")}";
                    i++;
                }
                
                if(input-all_marz>=cash)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"{input} - {all_marz} = {input-all_marz}";
                    input -= all_marz;
                }
                else
                {
                    var marz1=input-all_marz;
                    var cash1 = cash - marz1;
                    sheet.Cells[$"B{i}:D{i}"].Merge = true;
                    sheet.Cells[$"B{i}:D{i}"].Value = $"{input} - {all_marz} = {input - (all_marz - cash1)}, избыток {cash1}";
                    input = input - (all_marz - cash1);
                }
                
                i++;
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"Изменения в кассе, cовершенные пользователем";
                i++;
               
                var cash_to_buch = db.InsertOutCash.Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && (p.Type == "Передача бухгалтеру")).FirstOrDefault();
                var remove_marz = db.InsertOutCash.Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && (p.Type == "Снятая маржа")).FirstOrDefault();
                if(cash_to_buch!=null)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"Переданно бухгалтеру {cash_to_buch.OldCash}-{cash_to_buch.Cash} = { cash_dont_rash+=cash_to_buch.Cash}";
                    i++;
                }
                if (remove_marz != null)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"Cнятие маржи {remove_marz.OldCash}-{remove_marz.Cash} = { cash_dont_rash += remove_marz.Cash}";
                    i++;
                }
                if (openCloseCash.Shortage > 0)
                {
                    sheet.Cells[$"B{i}:D{i}"].Merge = true;
                    sheet.Cells[$"B{i}:D{i}"].Value = $"Недосдача  {openCloseCash.Shortage}";
                    i++;
                }
                var expenses_close = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && p.IsOpenDay == false && p.TypePayId == 1 && p.TypeExpensesId != 5).ToList();
                foreach (var e in expenses_close)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"Расходы {cash_dont_rash}-{e.Cash}-{cash_dont_rash -= e.Cash} причина {e.Name} {e.TypeExpenses.Name}";
                    i++;
                }
                i++;
                i++;
                sheet.Cells[$"B{i}:D{i}"].Merge = true;
                sheet.Cells[$"B{i}:D{i}"].Value = $"Закрытие кассы: {openCloseCash.CloseCash} расходы {openCloseCash.Shortage} маржа {openCloseCash.Marz}";
                i++;
                sheet.Cells[$"B{i}:G{i}"].Merge = true;
                sheet.Cells[$"B{i}:G{i}"].Value = $"Счета {date.ToString("dd.MM.yyyy")} - {account_cashe}";
                i++;
                foreach (var inv in invoices)
                {
                    foreach (var good in inv.GoodsInvoice)
                    {

                        
                        if (good.TypePayId == 2)
                        {
                            Console.WriteLine(good.Goods.Description);
                            sheet.Cells[$"A{i}"].Value = good.AllPrice;
                            sheet.Cells[$"B{i}"].Value = good.Goods.Description;
                        }
                        i++; ;
                    }

                }
                var expenses_shtet = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.EmployeeId == EmpDay && p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && p.IsOpenDay == false && p.TypePayId==2 && p.TypeExpensesId != 5).ToList();
                foreach (var e in expenses_shtet)
                {
                    sheet.Cells[$"B{i}:G{i}"].Merge = true;
                    sheet.Cells[$"B{i}:G{i}"].Value = $"Расходы {e.Cash} причина {e.Name} {e.TypeExpenses.Name}";
                    i++;
                }
                package.Save();
            }


        });
    }
}
