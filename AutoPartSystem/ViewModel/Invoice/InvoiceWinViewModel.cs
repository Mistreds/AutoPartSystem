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
using System.Windows.Controls;
using System.Windows;
using Microsoft.EntityFrameworkCore;

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
        public List<Data.TypePay> TypePay { get; private set; }
        private ObservableCollection<Data.City> _cities;
        public ObservableCollection<Data.City> Cities
        {
            get => _cities;
            set=>this.RaiseAndSetIfChanged(ref _cities, value);
        }
        private UserControl main_control;
        public UserControl MainControl
        {
            get => main_control;
            set=>this.RaiseAndSetIfChanged(ref main_control, value);
        }
        public bool IsEdit { get; set; }
        public bool IsInvoice { get; set; }
        private View.Invoice.CreateInvoice CreateInvoice;
        private View.Invoice.AgentInvoice AgentInvoice;
        private View.Invoice.InvoceTable InvoiceTable;
        public List<Data.Employee> Employees { get; private set; }
        private bool _is_new_client;
        public bool IsNewClient
        {
            get => _is_new_client;
            set
            {

                //InitNewAgentCommand();
                this.RaiseAndSetIfChanged(ref _is_new_client, value);
            }
        }
        private ReactiveCommand<Unit, Unit> InitNewAgent;
        private Data.Client _client;
        public Data.Client Client
        {
            get => _client;
            set=>this.RaiseAndSetIfChanged(ref _client, value);
        }
        private bool _is_marzh;
        public bool IsMarzh
        {
            get => _is_marzh;
            set=>this.RaiseAndSetIfChanged(ref _is_marzh, value);  
        }
        private int _emp_id;
        public int EmpId
        {
            get => _emp_id;
            set=>this.RaiseAndSetIfChanged(ref _emp_id , value);
        }
        private string _city_name;
        public string CityName
        {
            get => _city_name;
            set=>this.RaiseAndSetIfChanged(ref _city_name , value);
        }
        private bool _is_not_new_client;
        public bool IsNotNewClient
        {
            get => _is_not_new_client;
            set=>this.RaiseAndSetIfChanged(ref _is_not_new_client, value);
        }
        delegate void InitNewAgentCommandDelegate();
        InitNewAgentCommandDelegate InitNewAgentCommand;
        public InvoiceWinViewModel(Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel, Model.MarkModel.MarkModel MarkModel)
        {
            BackInvoice = ReactiveCommand.Create(() => {
                MainControl = CreateInvoice;
            });
            SelectNewClient = ReactiveCommand.Create(() => {

                   ClientViewModel clientView = new ClientViewModel(Invoice);

               });
            InitNewAgentCommand =delegate() {
                Client = new Data.Client();
                CityName = "";
                Mark = MarkModel.GetMark();
                Invoice.Client = new Data.Client();
                Client.new_mark_model();
                Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            };
            Client = new Data.Client();
            CityName = "";
            Action action = new Action(InitNewAgentCommand);
            InitNewAgent = ReactiveCommand.Create(action);
            this.WarehouseInvoceModel = WarehouseInvoceModel;
            Employees = MainViewModel.AdminModel.GetEmployeeMeneger(MainViewModel.Employee.Id);
            this.MarkModel = MarkModel;
            Mark = MarkModel.GetMark();
            Client = new Data.Client();
            Client.new_mark_model();
            CreateInvoice = new View.Invoice.CreateInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = CreateInvoice;
            Cities=MainViewModel.AdminModel.GetCitiesFromText("");
            CreateInvoiceBase = ReactiveCommand.Create(() =>
             {
                 if (IsNewClient)
                 {
                     if (!string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName) && (Invoice.IsAgent == true || Client.ModelId != 0))
                     {
                         IsNotNewClient = true;

                     }
                     else
                     {
                         IsNotNewClient = false;
                         if (Invoice.IsAgent == true)
                         {
                             MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                             return;
                         }
                     }
                     MainViewModel.ClientModel.AddClient(Client);
                 }
                 if(Invoice.IsDelMarzh)
                     WarehouseInvoceModel.AddInvoiceToDataBase(Invoice, EmpId);
                 else
                    WarehouseInvoceModel.AddInvoiceToDataBase(Invoice);
             });
            
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()),MainViewModel.Employee);
            try
            {
                this.WhenAnyValue(vm => vm.Client.MarkId).WhereNotNull().Subscribe(x => UpdateModelsNew(x));
                this.WhenAnyValue(vm => vm.Invoice.Client.MarkId).WhereNotNull().Subscribe(x => UpdateModels(x));
            }
            catch { }
            invoiceGood = new View.Warehouse.InvoiceGood(this);
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            invoiceGood.Show();      
        }
        public InvoiceWinViewModel(ObservableCollection<WarehouseTable> warehouseTables,bool isagent)
        {
            SelectNewClient = ReactiveCommand.Create(() => {

                ClientViewModel clientView = new ClientViewModel(Invoice,true);

            });
            BackInvoice = ReactiveCommand.Create(() => {
                MainControl = AgentInvoice;
            });
            InitNewAgentCommand = delegate ()
            {
                Console.WriteLine("sdadas");
                Client = new Data.Client(1);
                CityName = "";
                Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            };
            Client = new Data.Client(1);
            CityName = "";
            this.WarehouseInvoceModel = new Model.Warehouse.WarehouseInvoceModel();
            WarehouseInvoceModel.SetWarehouse(warehouseTables);
            Employees = MainViewModel.AdminModel.GetEmployeeMeneger(MainViewModel.Employee.Id);
            AgentInvoice = new View.Invoice.AgentInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = AgentInvoice;
            Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            CreateInvoiceBase = ReactiveCommand.Create(() =>
            {
                if (IsNewClient)
                {
                    if (!string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName) && (Invoice.IsAgent == true || Client.ModelId != 0))
                    {
                        IsNotNewClient = true;
                        
                    }
                    else
                    {
                        IsNotNewClient = false;
                        if (Invoice.IsAgent == true)
                        {
                            MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    var city = MainViewModel.AdminModel.GetCityFromName(CityName);
                    if (city == null)
                    {
                        city = new Data.City { Name = CityName };
                    }
                    Client.IsAgent = true;
                    Client.City = city;
                    Invoice.Client = Client;
                    CityName = Client.City.Name;
                }
                invoice.IsAgent = false;
                invoice.IsInvoice = true;
                WarehouseInvoceModel.UpdateInvoice(Invoice);
            });
            Invoice = new Data.Invoice(new ObservableCollection<Data.Warehouse>(WarehouseInvoceModel.GetWarehouse()), MainViewModel.Employee);
            Invoice.IsAgent = true;
            invoiceGood = new View.Warehouse.InvoiceGood(this);
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            invoiceGood.Show();
        } 
        public InvoiceWinViewModel(Data.Invoice invoice)
        {
            SelectNewClient = ReactiveCommand.Create(() => {

                ClientViewModel clientView = new ClientViewModel(Invoice);

            });
            BackInvoice = ReactiveCommand.Create(() => {
                MainControl = CreateInvoice;
            });
            InitNewAgentCommand = delegate ()
            {
                Client = new Data.Client();
                CityName = "";
                Mark = MarkModel.GetMark();
                Invoice.Client = new Data.Client();
                Client.new_mark_model();
                Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            }; 
            CityName = "";
            Client = new Data.Client();
          
            IsEdit = true;
            Invoice = invoice;
            IsInvoice = Invoice.IsInvoice;
            this.WarehouseInvoceModel = new Model.Warehouse.WarehouseInvoceModel();
            this.MarkModel = MainViewModel._markModel;
            if(IsInvoice && MainViewModel.PositId==1)
            {
                CreateInvoiceBase = ReactiveCommand.Create(() =>
                {
                    //if (MessageBox.Show("После возврата товаров, удалить накладную из базы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //{
                    //    WarehouseInvoceModel.ReturnInvoice(Invoice,true); ;
                        
                    //}
                    //else
                    //{
                    //    WarehouseInvoceModel.ReturnInvoice(Invoice,false);
                    //}
                    //MessageBox.Show("Товары возвращены на склад", "Успех");
                    //invoiceGood.Close();
                    View.Invoice.BackInvoice backInvoice= new View.Invoice.BackInvoice();
                    backInvoice.DataContext = this;
                    backInvoice.Show();
                });
            }
            else
            {
                CreateInvoiceBase = ReactiveCommand.Create(() =>
                {
                    if (IsNewClient && !string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName) && (Invoice.IsAgent == true || Client.ModelId != 0))
                    {
                        IsNotNewClient = true;
                        if (Invoice.IsAgent == true)
                        {
                            MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    WarehouseInvoceModel.UpdateInvoice(Invoice);
                });
            }
            
            try
            {
                this.WhenAnyValue(vm => vm.Invoice.Client.Model.MarkId).WhereNotNull().Subscribe(x => UpdateModels(x));
            }
            catch { }
            CreateInvoice = new View.Invoice.CreateInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = InvoiceTable;
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            invoiceGood = new View.Warehouse.InvoiceGood(this);
            //this.WhenAnyValue(vm =>ViewModel.MainViewModel.AdminModel.Cities.Count).Subscribe(x=> UpdateCity());
            invoiceGood.Show();
        }
        private View.Warehouse.InvoiceGood invoiceGood;
        public InvoiceWinViewModel(Data.Invoice invoice, bool IsAgent)
        {
            BackInvoice = ReactiveCommand.Create(() => {
                MainControl = AgentInvoice;
            });
            Invoice = invoice;
            SelectNewClient = ReactiveCommand.Create(() => {

                ClientViewModel clientView = new ClientViewModel(Invoice, true);

            });
            InitNewAgentCommand = delegate ()
            {
                Client = new Data.Client(1);
                CityName = "";
                Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            }; CityName = "";
            Client = new Data.Client(1);
            this.WarehouseInvoceModel = new Model.Warehouse.WarehouseInvoceModel();
            Employees = MainViewModel.AdminModel.GetEmployeeMeneger(MainViewModel.Employee.Id);
            AgentInvoice = new View.Invoice.AgentInvoice(this);
            InvoiceTable = new View.Invoice.InvoceTable();
            MainControl = AgentInvoice;
            Cities = MainViewModel.AdminModel.GetCitiesFromText("");
            CreateInvoiceBase = ReactiveCommand.Create(() =>
            {
                if (IsNewClient)
                {
                    if (!string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName))
                    {
                        IsNotNewClient = true;

                    }
                    else
                    {
                        IsNotNewClient = false;
                        if (Invoice.IsAgent == true)
                        {
                            MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    var city = MainViewModel.AdminModel.GetCityFromName(CityName);
                    if (city == null)
                    {
                        city = new Data.City { Name = CityName };
                    }
                    Client.IsAgent = true;
                    Client.City = city;
                    Invoice.Client = Client;
                    CityName = Client.City.Name;
                }
                invoice.IsAgent=false;
                invoice.IsInvoice = true;
                WarehouseInvoceModel.UpdateInvoice(Invoice);
            });
            invoiceGood = new View.Warehouse.InvoiceGood(this);
            TypePay = MainViewModel.WarehouseModel.GetTypePay();
            IsEdit = true;
            invoiceGood.Show();
        }
        private void UpdateCity()
        {
            Cities = MainViewModel.AdminModel.GetCitiesFromText("");
        }
        private void UpdateModels(int mark_id)
        {
            
            Models=MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<Unit, Unit> BackInvoiceCommand => ReactiveCommand.Create(() => {



            foreach (var inv in Invoice.GoodsInvoice)
            {
                using var db = new Data.ConDB();
                var back_ind = db.backInvoices.Where(p => p.GoodsInvoiceId == inv.Id).FirstOrDefault();
                if (inv.BackCount == 0)
                {
                    continue;
                }
                if (back_ind==null)
                {
                    if (inv.BackCount > inv.Count)
                    {
                        inv.DontHaveGoods = true;
                        MessageBox.Show("Возвращаемого товара не может быть больше проданного","",MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                    
                    else
                        inv.DontHaveGoods = false;
                    back_ind = new Data.BackInvoice { Count = inv.BackCount, Date = DateTime.Now, EmployeeId = Invoice.EmployeeId, GoodsInvoiceId = inv.Id };
                    Data.Expenses expenses = new Data.Expenses { Name=inv.Goods.Description, TypePayId=inv.TypePayId, EmployeeId=invoice.EmployeeId, Date=DateTime.Now, Cash=inv.BackCount*inv.Price, TypeExpensesId=5  };
                    db.Expenses.Add(expenses);
                    db.backInvoices.Add(back_ind);
                    
                  var ware=  db.Warehouse.Include(p => p.Goods).Where(p => p.Goods.Id == inv.GoodsId).FirstOrDefault();
                    switch(Invoice.Employee.CityId)
                    {
                        case 1:
                            ware.InAlmata += inv.BackCount;
                            break;
                        case 2:
                            ware.InAstana += inv.BackCount;
                            break;
                        case 3:
                            ware.InAktau+=inv.BackCount;
                            break;

                    }
                    db.Update(ware);
                    db.SaveChanges();
                }
                else
                {
                    if (inv.BackCount > (inv.Count- back_ind.Count))
                    {

                        inv.DontHaveGoods = true;
                        MessageBox.Show("Возвращаемого товара не может быть больше проданного и которого вернули", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue;
                    }
                    back_ind.Count = inv.BackCount;
                    Data.Expenses expenses = new Data.Expenses { Name = inv.Goods.Description, TypePayId = inv.TypePayId, EmployeeId = invoice.EmployeeId, Date = DateTime.Now, Cash = inv.BackCount * inv.Price, TypeExpensesId = 5 };
                    db.Expenses.Update(expenses);
                    db.backInvoices.Add(back_ind);

                    var ware = db.Warehouse.Include(p => p.Goods).Where(p => p.Goods.Id == inv.GoodsId).FirstOrDefault();
                    switch (Invoice.Employee.CityId)
                    {
                        case 1:
                            ware.InAlmata += inv.BackCount;
                            break;
                        case 2:
                            ware.InAstana += inv.BackCount;
                            break;
                        case 3:
                            ware.InAktau += inv.BackCount;
                            break;

                    }
                    db.Update(ware);
                    
                    db.SaveChanges();
                }
            }
        });
        public ReactiveCommand<Data.GoodsInvoice, Unit> DeleteGoodInvoice => ReactiveCommand.Create<Data.GoodsInvoice>(DeleteGoodInvoiceCom);
        private void DeleteGoodInvoiceCom(Data.GoodsInvoice goodsInvoice)
        {
            invoice.GoodsInvoice.Remove(goodsInvoice);
            WarehouseInvoceModel.RemoveGoodInvoice(goodsInvoice);
            if (invoice.GoodsInvoice.Count==0)
            {
                if (MessageBox.Show("В заявке не осталось товаров, удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    WarehouseInvoceModel.RemoveInvoce(Invoice);
                    invoiceGood.Close();   
                }
            }
        }
        public ReactiveCommand<Unit, Unit> UpdateBooking => ReactiveCommand.Create(() => {

            if (IsNewClient)
            {
                
                if (!string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName))
                {
                    IsNotNewClient = true;

                }
                else
                {
                    IsNotNewClient = false;
                    if (Invoice.IsAgent == true)
                    {
                        MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                var city = MainViewModel.AdminModel.GetCityFromName(CityName);
                if (city == null)
                {
                    city = new Data.City { Name = CityName };
                }
                Client.City = city;
                Client.IsAgent = true;
                Invoice.Client = Client;
                CityName= Client.City.Name;
               
            }
            WarehouseInvoceModel.AddBookingToDatabase(Invoice);
        });
        private void UpdateModelsNew(int mark_id)
        {

            Models = MarkModel.GetModelFromMarkId(mark_id);
            Client.ModelId = 0;
        }
        public ReactiveCommand<Unit, Unit> SelectNewClient { get; set; }
        public ReactiveCommand<string, Unit> CreateInvoiceCommercial=>ReactiveCommand.Create<string>(CreateInvoiceCommercialCommand);
        private void CreateInvoiceCommercialCommand(string inv_com)
        {
            if(IsNewClient)
            {
                if (!string.IsNullOrEmpty(Client.PhoneName) && !string.IsNullOrEmpty(Client.Name) && !string.IsNullOrEmpty(CityName) && (Invoice.IsAgent==true || Client.ModelId!=0) )
                {
                    IsNotNewClient = true;
                    if(Invoice.IsAgent==true)
                    {
                        MessageBox.Show("Не введены данные нового клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    IsNotNewClient=false;
                }
                    var city=MainViewModel.AdminModel.GetCityFromName(CityName);
                if(city==null)
                {
                    city = new Data.City { Name = CityName };
                }
                Client.City=city;
                Invoice.Client = Client;
                CityName = Client.City.Name;
            }
            if(inv_com=="Invoice")
            {
                Invoice.IsInvoice = true;
            }
            if(inv_com=="Commercial")
            {
                Invoice.IsInvoice = false;
            }
            MainControl = InvoiceTable;
        }
        public ReactiveCommand<Unit, Unit> BackInvoice { get; set; }
        private ReactiveCommand<Unit, Unit> _create_invoice_base;
        public ReactiveCommand<Unit, Unit> CreateInvoiceBase
        {
            get => _create_invoice_base;
            set => this.RaiseAndSetIfChanged(ref _create_invoice_base, value);
        }
        public ReactiveCommand<Unit, Unit> AddNewGoods=>ReactiveCommand.Create(()=>{
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,0);
        
        });
        public ReactiveCommand<Unit, Unit> AddVirtualGoods => ReactiveCommand.Create(() => {
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,1);

        });
        public ReactiveCommand<Unit, Unit> AddNewVirtualGoods => ReactiveCommand.Create(() => {
            var ware_view = new ViewModel.WarehouseViewModel(Invoice,2);

        });
        public ReactiveCommand<Unit, Unit> CreateExcel => ReactiveCommand.Create(() => { WarehouseInvoceModel.CreateExcelFile(Invoice); });

        }
}
