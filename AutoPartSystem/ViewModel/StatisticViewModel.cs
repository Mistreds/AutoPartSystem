using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.ViewModel
{
   
   public class StatisticViewModel:ReactiveObject
    {
        [Reactive]
        public DateTime Date1 { get; set; }
        [Reactive]
        public DateTime Date2 { get; set; }

        [Reactive]
        public List<string> StatisticsEmp { get; set; }
         public StatisticViewModel()
        {

        }
        public ReactiveCommand<Unit, Unit> GoStat => ReactiveCommand.Create(() => {
            StatisticsEmp = new List<string>();
            using var db = new Data.ConDB();
            foreach (var emp in db.Employees.Where(p=>p.SetCell))
            {

                string emp_stat = $"{emp.Name} продажи ";
                int count = db.Invoices.Where(p => p.Date >= Date1 && p.Date <= Date1 && p.EmployeeId == emp.Id).Count();
                emp_stat += count;
                int count_1 = db.Invoices.Where(p => p.Date >= Date1 && p.Date <= Date1 && p.EmployeeId == emp.Id).Sum(p => p.AllCount);
                
                int marz = db.Invoices.Where(p => p.Date >= Date1 && p.Date <= Date1 && p.EmployeeId == emp.Id).Sum(p => p.AllMarz);
            }

        });
    }
}
