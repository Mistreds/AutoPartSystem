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
        private int _emp_month;
        public int EmpMonth
        {
            get => _emp_day;
            set => this.RaiseAndSetIfChanged(ref _emp_day, value);
        }
        private int _month_name;
        public int MonthName
        {
            get => _month_name;
            set=>this.RaiseAndSetIfChanged(ref _month_name, value); 
        }
        private string _year_month;
        public string YearMonth
        {
            get => _year_month;
            set=>this.RaiseAndSetIfChanged(ref _year_month, value);
        }
        private DateTime _date_day;
        public DateTime DateDay
        {  
            get => _date_day;
            set => this.RaiseAndSetIfChanged(ref _date_day, value);
        }
        public List<Model.MonthYear> Month { get; set; }
        public List<Model.MonthYear> Year { get; set; }
        public ReportViewModel()
        {
            Month = new List<Model.MonthYear> { new Model.MonthYear(1, "Январь"), new Model.MonthYear(2, "Февраль"), new Model.MonthYear(3, "Март"), new Model.MonthYear(4, "Апрель"), new Model.MonthYear(5, "Май"), new Model.MonthYear(6, "Июнь"), new Model.MonthYear(7, "Июль"), new Model.MonthYear(8, "Август"), new Model.MonthYear(9, "Сентябрь"), new Model.MonthYear(10, "Октябрь"), new Model.MonthYear(11, "Ноябрь"), new Model.MonthYear(12, "Декабрь"), };
            Year = new List<Model.MonthYear> { new Model.MonthYear(1, "2021"), new Model.MonthYear(1, "2022") , new Model.MonthYear(1, "2023") , new Model.MonthYear(1, "2024") };
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
                var openCloseCashList = db.OpenCloseCash.Where(p => p.EmployeeId == EmpDay && p.OpenDate >= day_start && p.OpenDate <= day_end && p.Status == 2).ToList();
                int cash = 0;
                int i = 1;
                bool first = true;
                if (openCloseCashList == null)
                    sheet.Cells[$"B{i}"].Value = $"{DateDay.ToString("dd-MM-yyyy")} - касса не открыта, либо не закрыта";
                else
                {

                    foreach(var openCloseCash in openCloseCashList)
                    {

                    sheet.Cells[$"B{i}"].Value = $"{DateDay.ToString("dd-MM-yyyy")} - касса  {openCloseCash.OpenCash}";
                        i += 2;
                    cash = openCloseCash.OpenCash;
                sheet.Cells[$"A{i}"].Value = "№";
                sheet.Cells[$"B{i}"].Value = "описание товара";
                sheet.Cells[$"C{i}"].Value = "склад";
                sheet.Cells[$"D{i}"].Value = "количества";
                sheet.Cells[$"E{i}"].Value = "сумма ";
                sheet.Cells[$"F{i}"].Value = "поступление";
                sheet.Cells[$"G{i}"].Value = "маржа";
                sheet.Cells[$"H{i}"].Value = "";
                var date_start = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                var date_end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                var invoices = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= openCloseCash.OpenDate && p.Date <= openCloseCash.CloseData && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpDay).ToList();
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
                if(first)
                        {

                     
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
                            first = false;
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
                if(add_to_open_cash!=null)
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
                    }
                }
                package.Save();
            }


        });
        public ReactiveCommand<Unit, Unit> CreateMonthReport => ReactiveCommand.Create(() => {
        var saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        saveFileDialog1.Filter = "Excel (*.xlsx)|*.xlsx";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            return;
        var newFile = new FileInfo(saveFileDialog1.FileName);
           
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                string month_names = Month.Where(p => p.Id == MonthName).FirstOrDefault().Name;
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add(month_names);
                var day = new DateTime(Convert.ToInt32(YearMonth), MonthName, 1, 0, 0, 0);
                using var db = new Data.ConDB();
                var emp = db.Employees.Include(p => p.City).Where(p => p.Id == EmpMonth).FirstOrDefault();
                sheet.Cells[$"A1:D1"].Merge = true;
                sheet.Cells[$"A1:D1"].Value = emp.Name;
                sheet.Cells[$"A2:A3"].Merge = true;
                sheet.Cells[$"A2:A3"].Value = "Дата";
                sheet.Cells[$"B2:B3"].Merge = true;
                sheet.Cells[$"B2:B3"].Value = "Доход";
                sheet.Cells[$"C2:D2"].Merge = true;
                sheet.Cells[$"C2:D2"].Value = "Отправленные";
                sheet.Cells[$"C3:C3"].Value = "Наличными";
                sheet.Cells[$"D3:D3"].Value = "Со счета";
                int i = 4;
                int all_price = 0;
                int all_nal = 0;
                int all_account = 0;
                DateTime end_day=new DateTime();
                while(day.Month==MonthName)
                {
                    end_day = new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
                    sheet.Cells[$"A{i}"].Value = day.ToString("dd.MM.yyyy");
                    var price=db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= day && p.Date <= end_day && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                    
                    sheet.Cells[$"B{i}"].Value = price;
                    all_price += price;
                    var nal = db.GoodsInvoices.Include(p => p.Invoice).Where(p => p.Invoice.Date >= day && p.Invoice.Date <= end_day && p.Invoice.IsInvoice && p.Invoice.IsEnd == false && p.TypePayId == 1 && p.Invoice.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                    all_nal += nal;
                    sheet.Cells[$"C{i}"].Value = nal;
                    var acc = db.GoodsInvoices.Include(p => p.Invoice).Where(p => p.Invoice.Date >= day && p.Invoice.Date <= end_day && p.Invoice.IsInvoice && p.Invoice.IsEnd == false && p.TypePayId == 2 && p.Invoice.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                    sheet.Cells[$"D{i}"].Value = acc;
                    all_account += acc;
                    day =day.AddDays(1);
                    i++;
                }
                sheet.Cells[$"A{i}"].Value = "Итого за месяц: ";
                sheet.Cells[$"B{i}"].Value = all_price;
                sheet.Cells[$"D{i}"].Value = all_account;
                i++;
                sheet.Cells[$"A{i}:B{i}"].Merge = true;
                sheet.Cells[$"A{i}:B{i}"].Value = "Остаток: ";
                sheet.Cells[$"c{i}"].Value = all_nal;

                i = 3;
                sheet.Cells[$"G{i}:J{i}"].Merge = true;
                sheet.Cells[$"G{i}:J{i}"].Value = $"Отчет за {month_names}";
                i++;
                sheet.Cells[$"G{i}:J{i}"].Merge = true;
                sheet.Cells[$"G{i}:J{i}"].Value = $"доход за месяц";
                i++;
                var daystart = new DateTime(Convert.ToInt32(YearMonth), MonthName, 1, 0, 0, 0);
                var day_mid_1_end = new DateTime(Convert.ToInt32(YearMonth), MonthName, 10, 23, 59, 59);
                var day_mid_1_start = new DateTime(Convert.ToInt32(YearMonth), MonthName, 11, 0, 0, 0);
                var day_mid_2_end = new DateTime(Convert.ToInt32(YearMonth), MonthName, 20, 23, 59, 59);
                var day_mid_2_start = new DateTime(Convert.ToInt32(YearMonth), MonthName, 21, 0, 0, 0);
                sheet.Cells[$"G{i}"].Value = $"c {daystart.Day} по {day_mid_1_end.ToString("dd.MM.yyyy")}";
                sheet.Cells[$"H{i}:J{i}"].Merge=true;
                sheet.Cells[$"H{i}:J{i}"].Value= db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= daystart && p.Date <= day_mid_1_end && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                i++;
                sheet.Cells[$"G{i}"].Value = $"c {day_mid_1_start.Day} по {day_mid_2_end.ToString("dd.MM.yyyy")}";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= day_mid_1_start && p.Date <= day_mid_2_end && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                i++;
                sheet.Cells[$"G{i}"].Value = $"c {day_mid_2_start.Day} по {end_day.ToString("dd.MM.yyyy")}";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = db.Invoices.Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).ThenInclude(p => p.Warehouse).Include(p => p.GoodsInvoice).Where(p => p.Date >= day_mid_2_start && p.Date <= end_day && p.IsEnd == false && p.IsInvoice && p.EmployeeId == EmpMonth).Sum(p => p.AllPrice);
                i++;
                i++;
                sheet.Cells[$"G{i}"].Value = "Дата";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = $"Расходы за месяц";
                i++;
                int all_exp = 0;
                var expoise = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.Date >= daystart && p.Date <= end_day && p.EmployeeId == EmpMonth && p.TypeExpensesId!=5 && p.TypeExpensesId!=3 && p.TypeExpensesId!=2).ToList();
                foreach(var exp in expoise)
                {
                    sheet.Cells[$"G{i}"].Value = $"{exp.Date.ToString("dd.MM.yyyy")}";
                    sheet.Cells[$"H{i}:I{i}"].Merge = true;
                    sheet.Cells[$"H{i}:I{i}"].Value = $"{exp.TypeExpenses.Name}";
                    sheet.Cells[$"J{i}"].Value = $"{exp.Cash}";
                    all_exp+=exp.Cash;
                    i++;
                }
                sheet.Cells[$"G{i}"].Value = "Итого";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = all_exp;
                i++;
                sheet.Cells[$"G{i}"].Value = "Дата";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = $"Реклама";
                int all_exp_rekl = 0;
                i++;
                var expoise_rekl = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.Date >= daystart && p.Date <= end_day && p.EmployeeId == EmpMonth && p.TypeExpensesId == 3).ToList();
                foreach (var exp in expoise_rekl)
                {
                    sheet.Cells[$"G{i}"].Value = $"{exp.Date.ToString("dd.MM.yyyy")}";
                    sheet.Cells[$"H{i}:I{i}"].Merge = true;
                    sheet.Cells[$"H{i}:I{i}"].Value = $"{exp.TypeExpenses.Name}";
                    sheet.Cells[$"J{i}"].Value = $"{exp.Cash}";
                    all_exp_rekl += exp.Cash;
                    i++;
                }
                sheet.Cells[$"G{i}"].Value = "Итого за рекламу";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = all_exp_rekl;
                i++;
                sheet.Cells[$"G{i}"].Value = "Дата";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = $"Возвраты";
                i++;
                int exp_back = 0;
                var expoise_back = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.Date >= daystart && p.Date <= end_day && p.EmployeeId == EmpMonth && p.TypeExpensesId == 5).ToList();
                foreach (var exp in expoise_back)
                {
                    sheet.Cells[$"G{i}"].Value = $"{exp.Date.ToString("dd.MM.yyyy")}";
                    sheet.Cells[$"H{i}:I{i}"].Merge = true;
                    sheet.Cells[$"H{i}:I{i}"].Value = $"{exp.Name}";
                    sheet.Cells[$"J{i}"].Value = $"{exp.Cash}";
                    exp_back += exp.Cash;
                    i++;
                }
                sheet.Cells[$"G{i}"].Value = "Итого за возвраты";
                sheet.Cells[$"H{i}:J{i}"].Merge = true;
                sheet.Cells[$"H{i}:J{i}"].Value = exp_back;
                var all_expoise = all_exp + exp_back + all_exp_rekl;
                i++;
                i++;
                sheet.Cells[$"G{i}"].Value = "Итого доход";
                sheet.Cells[$"G{i+1}"].Value = all_price;
                sheet.Cells[$"H{i}"].Value = "Итого за аренду";
                sheet.Cells[$"H{i+1}"].Value = db.Expenses.Include(p => p.TypeExpenses).Where(p => p.Date >= daystart && p.Date <= end_day && p.EmployeeId == EmpMonth && p.TypeExpensesId == 2).Sum(p=>p.Cash);
                all_expoise += db.Expenses.Include(p => p.TypeExpenses).Where(p => p.Date >= daystart && p.Date <= end_day && p.EmployeeId == EmpMonth && p.TypeExpensesId == 2).Sum(p => p.Cash);
                sheet.Cells[$"I{i}"].Value = "Счет %";
                sheet.Cells[$"I{i + 1}"].Value =  Convert.ToInt32(all_account * 0.01);
                all_expoise += Convert.ToInt32(all_account * 0.01);
                sheet.Cells[$"J{i}"].Value = "Расходы";
                sheet.Cells[$"J{i + 1}"].Value = all_exp;
                sheet.Cells[$"K{i}"].Value = "Возвраты";
                sheet.Cells[$"K{i + 1}"].Value = exp_back;
                sheet.Cells[$"L{i}"].Value = "OLX\\Реклама";
                sheet.Cells[$"L{i + 1}"].Value = all_exp_rekl;
                sheet.Cells[$"M{i}"].Value = "Итого расход";
                sheet.Cells[$"M{i + 1}"].Value = all_expoise;
                i++;
                i++;
                i++;
                sheet.Cells[$"K{i}"].Value = "Итого";
                sheet.Cells[$"K{i + 1}"].Value = all_price-all_expoise;
                package.Save();
                

            }

        });
    
    }

}
