using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoPartSystem.ViewModel;
using Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
namespace AutoPartSystem.Model
{
    interface IInvoiceModel
    {
        public ObservableCollection<Data.Invoice> SelectComInvoiceFromDataBase(int EmpId);
        public ObservableCollection<Data.Invoice> SelectInvoiceFromDataBase(int EmpId);

        public ObservableCollection<Data.Invoice> FindInvoice(ViewModel.InvoiceViewModel.FindInvoice find,
            bool is_invoice);
        public bool IsHaveInvoice(DateTime date1, DateTime date2, int emp_id);
    }
    public  class InvoiceModel:ReactiveObject, IInvoiceModel
    {
        private ObservableCollection<Data.Invoice> _com_invoice;
        public ObservableCollection<Data.Invoice > ComInvoice
        {
            get => _com_invoice;
            set=>this.RaiseAndSetIfChanged(ref _com_invoice, value);
        }
        private ObservableCollection<Data.Invoice> _invoice_table;
        public ObservableCollection<Data.Invoice> InvoiceTable
        {
            get => _invoice_table;
            set => this.RaiseAndSetIfChanged(ref _invoice_table, value);
        }

        public InvoiceModel()
        {
            ComInvoice = new ObservableCollection<Data.Invoice>();
            InvoiceTable = new ObservableCollection<Data.Invoice>();
        }

        public  ObservableCollection<Data.Invoice> SelectComInvoiceFromDataBase(int EmpId)
        {
            using var db = new Data.ConDB();
            ComInvoice = new ObservableCollection<Data.Invoice>(db.Invoices.Include(p=>p.Client).ThenInclude(p=>p.Model).ThenInclude(p=>p.Mark).Include(p=>p.Employee).ThenInclude(p=>p.City).Include(p=>p.Employee).ThenInclude(p=>p.Position).Include(p=>p.Client.City).Include(p=>p.GoodsInvoice).ThenInclude(p=>p.Goods).Include(p=>p.GoodsInvoice).ThenInclude(p=>p.Model).ThenInclude(p=>p.Mark).Where(p=>(EmpId==0 || p.EmployeeId==EmpId) && p.IsInvoice==false).Select(p=>new Data.Invoice(p, "Com")));
           return ComInvoice;
        }
        public ObservableCollection<Invoice> SelectInvoiceFromDataBase(int EmpId)
        {
            using var db = new Data.ConDB();
            InvoiceTable = new ObservableCollection<Data.Invoice>(db.Invoices.Include(p => p.Client).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Include(p => p.Employee).ThenInclude(p => p.City).Include(p => p.Employee).ThenInclude(p => p.Position).Include(p => p.Client.City).Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).Include(p => p.GoodsInvoice).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => (EmpId == 0 || p.EmployeeId == EmpId) && p.IsInvoice == true).Select(p => new Data.Invoice(p, "Inv")));
            return InvoiceTable;
        }

        public ObservableCollection<Invoice> FindInvoice(InvoiceViewModel.FindInvoice find, bool is_invoice)
        {
            using var db = new Data.ConDB();
            InvoiceTable = new ObservableCollection<Data.Invoice>(db.Invoices.Include(p => p.Client).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Include(p => p.Employee).ThenInclude(p => p.City).Include(p => p.Employee).ThenInclude(p => p.Position).Include(p => p.Client.City).Include(p => p.GoodsInvoice).ThenInclude(p => p.Goods).Include(p => p.GoodsInvoice).ThenInclude(p => p.Model).ThenInclude(p => p.Mark)
                .Where(p => (find.EmpId == 0 || p.EmployeeId == find.EmpId) && p.Client.PhoneName.ToLower().Contains(find.Phone.ToLower()) && p.Client.Name.ToLower().Contains(find.Fio.ToLower()) && ((find.Date1.ToString("dd-MM-yyyy")== "01.01.0001" || find.Date2.ToString("dd-MM-yyyy") == "01.01.0001") || (p.Date>=find.Date1 && p.Date<=find.Date2)) && p.IsInvoice == is_invoice && (find.Id==0 || p.Id==find.Id)).Select(p => new Data.Invoice(p, "Inv")));
            return InvoiceTable;
        }

        public bool IsHaveInvoice(DateTime date1, DateTime date2, int emp_id)
        {
            using var db = new Data.ConDB();
            var inv = db.Invoices.Where(p => p.Date >= date1 && p.Date <= date2 && p.EmployeeId == emp_id).Count();
            if (inv > 0)
            {

                return true;
            }
            return false;
        }
    }
}
