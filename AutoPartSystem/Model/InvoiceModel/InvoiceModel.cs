using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
namespace AutoPartSystem.Model
{
    interface IInvoiceModel
    {
        public ObservableCollection<Data.Invoice> UpdateComInvoiceFromDataBase(int EmpId);
    }
    public  class InvoiceModel:ReactiveObject, IInvoiceModel
    {
        private ObservableCollection<Data.Invoice> _com_invoice;
        public ObservableCollection<Data.Invoice > ComInvoice
        {
            get => _com_invoice;
            set=>this.RaiseAndSetIfChanged(ref _com_invoice, value);
        }
       
        public InvoiceModel()
        {
            ComInvoice = new ObservableCollection<Data.Invoice>();
        }

        public  ObservableCollection<Data.Invoice> UpdateComInvoiceFromDataBase(int EmpId)
        {
            using var db = new Data.ConDB();
            ComInvoice = new ObservableCollection<Data.Invoice>(db.Invoices.Include(p=>p.Client).ThenInclude(p=>p.Model).ThenInclude(p=>p.Mark).Include(p=>p.Employee).ThenInclude(p=>p.City).Include(p=>p.Client.City).Include(p=>p.GoodsInvoice).ThenInclude(p=>p.Goods).Include(p=>p.GoodsInvoice).ThenInclude(p=>p.Model).ThenInclude(p=>p.Mark).Where(p=>EmpId==0 || p.EmployeeId==EmpId));
           return ComInvoice;
        }
    }
}
