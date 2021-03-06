#nullable enable
#pragma warning disable CS8603 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoPartSystem.Model.Warehouse;
using Data;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class WarehouseViewModel : ReactiveObject
    {
        #region Init
        #region UserControls
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<UserControl>? _controls;
        #endregion
        #region AddNewWarehouseData
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
        private ObservableCollection<Data.Brand> _brands;
        public ObservableCollection<Data.Brand> Brands
        {
            get => _brands;
            set => this.RaiseAndSetIfChanged(ref _brands, value);
        }
        private WarehouseAdd? _warehouse;
        public WarehouseAdd? Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private WarehouseAdd? _warehouse_virtual;
        public WarehouseAdd? WarehouseVirtual
        {
            get => _warehouse_virtual;
            set => this.RaiseAndSetIfChanged(ref _warehouse_virtual, value);
        }
        private ModelAdd _model;
        public ModelAdd Model
        {
            get => _model;
            set=>this.RaiseAndSetIfChanged(ref _model, value);
        }
        #endregion
        #region WarehouseTableData
        private ObservableCollection<WarehouseTable> _warehouses_table;
        public ObservableCollection<WarehouseTable> WarehousesTable
        {
            get => _warehouses_table;
            set => this.RaiseAndSetIfChanged(ref _warehouses_table, value);
        }
        private ObservableCollection<WarehouseTable> _warehouses_virtual_table;
        public ObservableCollection<WarehouseTable> WarehousesVirtualTable
        {
            get => _warehouses_virtual_table;
            set => this.RaiseAndSetIfChanged(ref _warehouses_virtual_table, value);
        }
        public List<Data.TypePay> TypePay { get;private set; }
        #endregion
        #region Models
        public Model.MarkModel.MarkModel? MarkModel;
        public static Model.Warehouse.WarehouseModel? WarehouseModel;
        private Model.Warehouse.WarehouseInvoceModel WarehouseInvoceModel;
        private Data.Invoice _invoce;
        private bool _is_select_for_invoice;
        public bool IsSelectForInvoice
        {
            get => _is_select_for_invoice;
            set => this.RaiseAndSetIfChanged(ref _is_select_for_invoice, value);
        }
        #endregion
        #region WarehouseSortAndFind
        private bool _is_model_find;
        public bool IsModelFind
        {
            get => _is_model_find;
            set=>this.RaiseAndSetIfChanged(ref _is_model_find, value); 
        }
        private bool _is_description_find;
        public bool IsDesctiptionFind
        {
            get => _is_description_find;
            set => this.RaiseAndSetIfChanged(ref _is_description_find, value);
        }
        private bool _is_article_find;
        public bool IsArticleFind
        {
            get => _is_article_find;
            set => this.RaiseAndSetIfChanged(ref _is_article_find, value);
        }
        private bool _is_brand_find;
        public bool IsBrandFind
        {
            get => _is_brand_find;
            set => this.RaiseAndSetIfChanged(ref _is_brand_find, value);
        }
        private bool _is_all_find;
        public bool IsAllFind
        {
            get => _is_all_find;
            set => this.RaiseAndSetIfChanged(ref _is_all_find, value);
        }
        private string _text_find_all;
        public string TextFindAll
        {
            get => _text_find_all;
            set => this.RaiseAndSetIfChanged(ref _text_find_all,value);
        }
        private ObservableCollection<MarkModelFind>? _mark_model_find;
        public ObservableCollection<MarkModelFind> MarkModelFind
        {
            get => _mark_model_find;
            set => this.RaiseAndSetIfChanged(ref _mark_model_find, value);
        }
        private ObservableCollection<MarkModelFind>? _description_find;
        public ObservableCollection<MarkModelFind> DescriptionFind
        {
            get => _description_find;

            set => this.RaiseAndSetIfChanged(ref _description_find, value);
        }
        private ObservableCollection<MarkModelFind>? _brand_find;
        public ObservableCollection<MarkModelFind> BrandFind
        {
            get => _brand_find;

            set => this.RaiseAndSetIfChanged(ref _brand_find, value);
        }
        private ObservableCollection<MarkModelFind>? _article_find;
        public ObservableCollection<MarkModelFind> ArticleFind
        {
            get => _article_find;
            set => this.RaiseAndSetIfChanged(ref _article_find, value);
        }
        #endregion
        #endregion
        #region WarehouseViewModelInit
        public Data.Employee Employee { get; set; }
        public int PosId { get;private set; }
        private bool is_finded;
        private bool is_virtual_finded;
        public WarehouseViewModel(Model.MarkModel.MarkModel markModel)
        {
            Employee=MainViewModel.Employee;
            AddSale = ReactiveCommand.Create(() => {
                WarehouseInvoceModel.SetWarehouse(new ObservableCollection<WarehouseTable>(WarehousesTable.Where(p => p.IsSelected == true).Select(p => ViewModel.WarehouseTable.NewTable(p,true)).ToList()));

                InvoiceWinViewModel invoiceWinViewModel = new InvoiceWinViewModel(WarehouseInvoceModel, MarkModel);
            });
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(),new View.Warehouse.VirtualWarehouse() };
            MainControl = _controls[0]; 
            MarkModel = markModel;
            Mark = MarkModel.GetMark();
            Brands = MarkModel.GetBrand();
            Warehouse = new WarehouseAdd();
            WarehouseVirtual = new WarehouseAdd(true);
            MainViewModel.WarehouseModel = new Model.Warehouse.WarehouseModel(this);
            WarehouseModel = MainViewModel.WarehouseModel;
            PosId = MainViewModel.PositId;
            if (MainViewModel.Employee.SetCell && !MainViewModel.Employee.IsAdmin)
                WarehousesTable = WarehouseModel.GetAllWarehouseForSale();
            else
                WarehousesTable = WarehouseModel.GetAllWarehouse();
            WarehousesVirtualTable = WarehouseModel.GetWarehouseVirtualTables();
            WarehouseInvoceModel = new WarehouseInvoceModel();        
            Model = new ModelAdd();
            TypePay=WarehouseModel.GetTypePay();
            this.WhenAnyValue(s => s.MarkModel.Model).Subscribe(_ => TestModel());
            WarehouseModel.WhenAnyValue(s => s.Update).Subscribe(async _ => await UpdateWare());
           
        }
        private View.Warehouse.WarehouseWindows WarehouseWindows;
        public WarehouseViewModel(Data.Invoice Invoice, int type)
        {
            Employee = MainViewModel.Employee;
            this._invoce= Invoice;
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(_invoce), new View.Warehouse.VirtualWarehouse(), new View.Warehouse.AddVirtualGoodToInvoice() };
            MainControl = _controls[type];
            MarkModel = new Model.MarkModel.MarkModel(); ;
            WarehouseModel = new Model.Warehouse.WarehouseModel(this);
            if(type==0)
            {
                SetFilter = ReactiveCommand.Create(() =>
                {
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesFilter(MarkModelFind, DescriptionFind, ArticleFind,BrandFind));
                });
                MarkModelFind = MarkModel.MarkModelFind("");
                DescriptionFind = WarehouseModel.GetAllDesctiption("");
                ArticleFind = WarehouseModel.GetAllArticle("");
                BrandFind = MarkModel.BrandFind("");
                WarehousesTable = WarehouseModel.GetAllWarehouseForSale();
            }
            if(type==1)
            {
                SetFilter = ReactiveCommand.Create(() =>
                {
                    WarehousesTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesFilter(MarkModelFind, DescriptionFind, ArticleFind,BrandFind));
                });
                MarkModelFind = MarkModel.MarkModelFind("");
                DescriptionFind = WarehouseModel.GetAllVirtualDesctiption("");
                ArticleFind = WarehouseModel.GetAllVirtualArticle("");
                WarehousesVirtualTable = WarehouseModel.GetWarehouseVirtualForSale();
            }
            if(type==2)
            {
                Mark = MarkModel.GetMark();
                Model = new ModelAdd();
                WarehouseVirtual = new WarehouseAdd(true);
            }
            IsSelectForInvoice = true;
            this._invoce = Invoice;
            WarehouseWindows = new View.Warehouse.WarehouseWindows();
            WarehouseWindows.DataContext = this;
            WarehouseWindows.Show();
        }
        #endregion
        #region Command
        private ReactiveCommand<Unit, Unit> _add_model_to_ware_table;
        public ReactiveCommand<Unit, Unit> AddModelToWareTable
        {
            get => _add_model_to_ware_table;
            set => this.RaiseAndSetIfChanged(ref _add_model_to_ware_table, value);
        }
        public void OpenPageCommand(string page_id)
        {
            switch (page_id)
            {
                case "ZavSkladTable":
                    MainControl = _controls[0];
                    
                    MarkModelFind = MarkModel.MarkModelFind("");
                    BrandFind = MarkModel.BrandFind("");
                    DescriptionFind = WarehouseModel.GetAllDesctiption("");
                    ArticleFind = WarehouseModel.GetAllArticle("");
                    SetFilter = ReactiveCommand.Create(() => {
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesFilter(MarkModelFind, DescriptionFind, ArticleFind,BrandFind));

                    });
                    break;
                case "VirtualSkladTable":
                    MainControl = _controls[1];
                    is_virtual_finded = false;
                    MarkModelFind = MarkModel.MarkModelFind("");
                    DescriptionFind = WarehouseModel.GetAllVirtualDesctiption("");
                    ArticleFind = WarehouseModel.GetAllVirtualArticle("");
                    WarehousesVirtualTable = WarehouseModel.GetWarehouseVirtualTables();
                    SetFilter = ReactiveCommand.Create(() => {
                            WarehousesVirtualTable = new ObservableCollection<WarehouseTable>(WarehouseModel.GetWarehousesVirtualFilter(MarkModelFind, DescriptionFind, ArticleFind));
                    });
                    break;
            }
        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand); 
        private void SelectModelFromMarkCommand(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<string, Unit> AddWarehous => ReactiveCommand.Create<string>(AddWarehouseCommand);
        private void AddWarehouseCommand(string type)
        {
            switch (type)
            {
                case "Warehouse":
                    NewWarehouse(Warehouse);
                    break;
                case "Virtual":
                    WarehouseVirtual.IsVirtual = true;
                    NewWarehouse(WarehouseVirtual);
                    break;
                case "VirtualInvoice":
                    WarehouseVirtual.IsVirtual = true;
                    AddModelToWareCom(WarehouseVirtual);
                    NewWarehouseVirt(WarehouseVirtual);
                    break;
            }
        }
        private void NewWarehouse(WarehouseAdd war)
        {
            foreach (var a in war.Goods.GoodsModel)
            {
                if (a.Model.Id != 0)
                {
                    a.ModelId = a.Model.Id;
                    a.Model = null;
                }
                else
                {
                    if (a.Model.Mark.Id != 0)
                    {
                        a.Model.MarkId = a.Model.Mark.Id;
                        a.Model.Mark = null;
                    }
                }
            }
            if (war.IsVirtual == true)
                {
                war.Goods.BrandId = 1;

            }
            else
            { 
            if(war.Goods.Brand==null)
            {
                var brand = MarkModel.GetBrandFind(war.BrandName);
                if(brand!=null)
                {
                    war.Goods.BrandId=brand.Id;
                    war.Goods.Brand = null;
                }
                else
                {
                    war.Goods.Brand = new Brand(war.BrandName);
                }
            }
            else
            {
                war.Goods.BrandId = war.Goods.Brand.Id;
                war.Goods.Brand = null;

            }
            }
            WarehouseModel.AddWarehouse(war);
            war = new WarehouseAdd();
            Warehouse=new WarehouseAdd();
            WarehouseVirtual = new WarehouseAdd();
            WarehousesVirtualTable = WarehouseModel.GetWarehouseVirtualTables();
        }
        private void NewWarehouseVirt(WarehouseAdd war)
        {
            
         var ware= WarehouseModel.AddWarehouse1(war);
           
            _invoce.GoodsInvoice.Add(new GoodsInvoice(ware.Goods));
            war = new WarehouseAdd();
        }

        private ReactiveCommand<Unit, Unit> _add_sale;
        public ReactiveCommand<Unit, Unit> AddSale
        {
            get => _add_sale;
            set=>this.RaiseAndSetIfChanged(ref _add_sale, value);
        }
        public ReactiveCommand<Unit, Unit> AddBooking => ReactiveCommand.Create(() => {
            InvoiceWinViewModel invoiceWinViewModel = new InvoiceWinViewModel(new ObservableCollection<WarehouseTable>(WarehousesTable.Where(p => p.IsSelected == true).Select(p => ViewModel.WarehouseTable.NewTable(p, true)).ToList()),true);
        });
        #region WarehouseSortFilterCommand

        public ReactiveCommand<string, Unit> SelectFindModel => ReactiveCommand.Create<string>(SelectFindModelCommand);
            private void SelectFindModelCommand(string name)
            {
                is_finded=true;
                MarkModelFind = MarkModel.MarkModelFind(name);
            }

            public ReactiveCommand<string, Unit> SelectFindDesctiption => ReactiveCommand.Create<string>(SelectFindDesctiptionCommand);
            private void SelectFindDesctiptionCommand(string name)
            {
            is_finded = true;
            DescriptionFind = WarehouseModel.GetAllDesctiption(name);
            }
            public ReactiveCommand<string, Unit> SelectFindVirtualDesctiption => ReactiveCommand.Create<string>(SelectFindVirtualDesctiptionCommand);
            private void SelectFindVirtualDesctiptionCommand(string name)
            {
            is_finded = true;
            DescriptionFind = WarehouseModel.GetAllVirtualDesctiption(name);
            }
        public ReactiveCommand<string, Unit> SelectFindArticle => ReactiveCommand.Create<string>(SelectFindArticleCommand);
            private void SelectFindArticleCommand(string name)
            {
            is_finded = true;
            ArticleFind = WarehouseModel.GetAllArticle(name);
            }
        public ReactiveCommand<string, Unit> SelectFindBrand => ReactiveCommand.Create<string>(SelectFindBrandCommand);
        private void SelectFindBrandCommand(string name)
        {
            is_finded = true;
            BrandFind = MarkModel.BrandFind(name);
        }
        public ReactiveCommand<string, Unit> SortWarehouse => ReactiveCommand.Create<string>(SortWarehouseCommand);
        private void SortWarehouseCommand(string name)
            {
                switch (name)
                {
                    case "ModelDown":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => p.Goods.GoodsModel.Select(p=> $"{ p.Model.Mark.Name} {p.Model.Name}").FirstOrDefault()).ToList());
                        break;
                    case "ModelUp":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.GoodsModel.Select(p => $"{ p.Model.Mark.Name} {p.Model.Name}").FirstOrDefault()).ToList());
                        break;
                    case "DescriptionDown":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p =>p.Goods.Description).ToList());
                        break;
                    case "DescriptionUp":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Description).ToList());
                        break;
                    case "ArticleDown":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p =>p.Goods.Article).ToList());
                        break;
                    case "ArticleUp":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Article).ToList());
                        break;
                    case "BrandDown":
                            WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderByDescending(p => p.Goods.Brand.Name).ToList());
                        break;
                    case "BrandUp":
                        WarehousesTable = new ObservableCollection<WarehouseTable>(WarehousesTable.OrderBy(p => p.Goods.Brand.Name).ToList());
                        break;

            }
            }
        public ReactiveCommand<int, Unit> SelectAllModel => ReactiveCommand.Create<int>(SelectAllModelCommand);
        private void SelectAllModelCommand(int name)
            {
                var bools = MarkModelFind.FirstOrDefault(p => p.model_id == 0);
                if (name == 0)
                {
                    if (bools.IsSelected)
                    {
                        foreach (var model in MarkModelFind)
                        {
                        if (model.model_id == 0)
                        {
                            continue;
                        }
                        var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == model.model_id).FirstOrDefault(); ;
                        mod.IsSelected = true;
                            model.IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var model in MarkModelFind)
                        {
                            model.IsSelected = false;
                        if(model.model_id==0)
                        {
                            continue;
                        }
                        var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == model.model_id).FirstOrDefault(); ;
                        mod.IsSelected = false;
                    }
                    }
                }
                else
                {
                    bools.IsSelected = false;
                    var mod = MarkModel.MarkModelFinds.Where(p => p.model_id == name).FirstOrDefault();
                    mod.IsSelected=MarkModelFind.FirstOrDefault(p => p.model_id == name).IsSelected;
            }
            }
        public ReactiveCommand<int, Unit> SelectAllDesctiption => ReactiveCommand.Create<int>(SelectAllDesctiptionCommand);
        private void SelectAllDesctiptionCommand(int name)
            {
                var bools = DescriptionFind.Where(p => p.model_id == 0).FirstOrDefault();
                if (name == 0)
                {
                    if (bools.IsSelected)
                    {
                        foreach (var model in DescriptionFind)
                        {
                            model.IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var model in DescriptionFind)
                        {
                            model.IsSelected = false;
                        }
                    }
                }
                else
                {
                    bools.IsSelected = false;
                }
            }
        public ReactiveCommand<int, Unit> SelectAllArticle => ReactiveCommand.Create<int>(SelectAllArticleCommand);
        private void SelectAllArticleCommand(int name)
            {
                var bools = ArticleFind.Where(p => p.model_id == 0).FirstOrDefault();
                if (name == 0)
                {
                    if (bools.IsSelected)
                    {
                        foreach (var model in ArticleFind)
                        {
                            model.IsSelected = true;
                        }
                    }
                    else
                    {
                        foreach (var model in ArticleFind)
                        {
                            model.IsSelected = false;
                        }
                    }
                }
                else
                {
                    bools.IsSelected = false;
                }
            }
        public ReactiveCommand<int, Unit> SelectAllBrand => ReactiveCommand.Create<int>(SelectAllBrandCommand);
        private void SelectAllBrandCommand(int name)
        {
            var bools = BrandFind.Where(p => p.model_id == 0).FirstOrDefault();
            if (name == 0)
            {
                if (bools.IsSelected)
                {
                    foreach (var model in BrandFind)
                    {
                        model.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var model in BrandFind)
                    {
                        model.IsSelected = false;
                    }
                }
            }
            else
            {
                bools.IsSelected = false;
            }
        }
        private ReactiveCommand<Unit, Unit> _set_filter;
            public ReactiveCommand<Unit, Unit> SetFilter
            {
                get => _set_filter;
                set=>this.RaiseAndSetIfChanged(ref _set_filter, value);
            }
        public ReactiveCommand<Unit, Unit> SetStringFilter=>ReactiveCommand.Create(() => { 
            
            WarehousesTable= WarehouseModel.GetWarehouseTextFilter(TextFindAll); });
 
        #endregion
        public ReactiveCommand<WarehouseTable, Unit> CreatePrihod => ReactiveCommand.Create<WarehouseTable>(CreatePrihodCommand);
        private void CreatePrihodCommand(WarehouseTable table)
        {
            var ArrGoods= new View.Warehouse.ArrivelGoods(table,WarehouseModel);
            ArrGoods.Show();
        }
        public ReactiveCommand<WarehouseTable, Unit> AddToInvoice => ReactiveCommand.Create<WarehouseTable>(AddToInvoiceCommand);
        private void AddToInvoiceCommand(WarehouseTable table)
        {
            _invoce.GoodsInvoice.Add(new Data.GoodsInvoice(table.Goods));
        }
        public ReactiveCommand<Unit, Unit> AddNewWarehouseWinOpen => ReactiveCommand.Create(AddNewWarehouseWinOpenCommand);
        public void AddNewWarehouseWinOpenCommand()
        {
            View.Warehouse.AddToWarehousePage addToWarehousePage = new View.Warehouse.AddToWarehousePage();
            Warehouse = new WarehouseAdd();
            Mark = MarkModel.GetMark();
            AddModelToWareTable = ReactiveCommand.Create(() => { AddModelToWareCom(Warehouse); });
            addToWarehousePage.Show();
        }
        public void AddNewWarehouseVirtualWinOpenCommand()
        {
            View.Warehouse.AddToVirtualWarehouse addToWarehousePage = new View.Warehouse.AddToVirtualWarehouse();
            
            Mark = MarkModel.GetMark();
            AddModelToWareTable = ReactiveCommand.Create(() => { AddModelToWareCom(WarehouseVirtual); });
            addToWarehousePage.Show();
        }
        public ReactiveCommand<Unit, Unit> SearchFind => ReactiveCommand.Create(() => { IsAllFind = true; });
        private void AddModelToWareCom(WarehouseAdd war)
        {
            
                if (Model.Mark != null)
                {
                    if (Model.Model != null)
                    {
                        Model.Model.Mark = Model.Mark;
                    war.Goods.GoodsModel.Add(new Data.GoodsModel { Model= Model.Model,ModelId=Model.Id });
                    }
                    else
                    {
                        Model.Model = MarkModel.GetModelFromNameFind(Model.Name, Model.Mark.Id);
                        if (Model.Model != null)
                        {
                            Model.Model.Mark = Model.Mark;
                        war.Goods.GoodsModel.Add(new Data.GoodsModel { Model = Model.Model, ModelId = Model.Id });
                        }
                        else
                        {
                        war.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, Model.Mark.Id, Model.Mark) });
                        }
                    }
                }
                else
                {
                    Model.Mark = MarkModel.GetMarkFromNameFind(Model.MarkName);
                    if (Model.Mark != null)
                    {
                        Model.Model = MarkModel.GetModelFromNameFind(Model.Name, Model.Mark.Id);
                        if (Model.Model != null)
                        {
                            Model.Model.Mark = Model.Mark;
                        war.Goods.GoodsModel.Add(new Data.GoodsModel { Model = Model.Model, ModelId = Model.Id });
                        }
                        else
                        {
                        war.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, Model.Mark.Id,Model.Mark) });
                        }
                    }
                    else
                    {
                    war.Goods.GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model.Name, new Data.Mark(Model.MarkName)) });
                    }
                }

            
        }
        private void TestModel()
        {
            Console.WriteLine("Тип должна обновится модель тачки");
        }
        private async Task UpdateWare()
        {
            await Task.Run(() => {
            
              var   Warehousess = WarehouseModel.GetWarehouseUpdate();
                while(Warehousess == null)
                { }
                if (WarehousesTable == null) WarehousesTable = WarehouseModel.GetWarehouseUpdate();
                
                else
                {
                    if (WarehouseModel.is_filter == 1)
                    {
                        if(WarehousesTable.Count!= WarehouseModel.GetWarehouseUpdate().Count)
                        {
                            WarehousesTable = WarehouseModel.GetWarehouseUpdate();
                        }
                        else
                        {
                            foreach (var Warehouse in WarehousesTable)
                            {
                                if (Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault() == null) continue;
                                Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());

                            }
                        }
                    }
                    else
                    {

                   
                    foreach (var Warehouse in WarehousesTable)
                    {
                            if (Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault() == null) continue;
                            Warehouse.UpdateWare(Warehousess.Where(p => p.Id == Warehouse.Id).FirstOrDefault());

                    }
                    var not_in_ware = Warehousess.Where(p => !WarehousesTable.Select(s => s.Id).Contains(p.Id));
                    if(WarehouseModel.is_filter!=1)
                    {
                        foreach (var ware in not_in_ware)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                WarehousesTable.Add(ware);
                            });
                        }
                    }
                    }
                }
                if (!is_finded)
                {
                        
                    if(DescriptionFind==null|| DescriptionFind.Count==0)
                    {
                        DescriptionFind = WarehouseModel.GetAllDesctiption("");
                    }
                    if (ArticleFind==null || ArticleFind.Count == 0)
                    {
                        ArticleFind = WarehouseModel.GetAllArticle("");
                    }
                   
                       
                }
            });
        }
        
        public ReactiveCommand<Unit, Unit> UpdateTable => ReactiveCommand.Create(UpdateTableCom);
        public void UpdateTableCom()
        {
            //WarehouseModel.UpdateAll();
            if (MainViewModel.Employee.SetCell && !MainViewModel.Employee.IsAdmin)
                WarehousesTable = WarehouseModel.GetAllWarehouseForSale();
            else
                WarehousesTable = WarehouseModel.GetAllWarehouse();
        }
        public ReactiveCommand<WarehouseTable, Unit> SetWarePrice => ReactiveCommand.Create<WarehouseTable>(SetWarePriceCommand);
        public ReactiveCommand<Unit, Unit> OpenMovePage => ReactiveCommand.Create(() => {
            var MoveGoodsViewModel = new MoveGoodsViewModel(WarehouseModel,new ObservableCollection<WarehouseTable>(WarehousesTable.Select(p => ViewModel.WarehouseTable.NewTable(p)).ToList()));


        });
        private void SetWarePriceCommand(WarehouseTable table)
        {
            var SetWare = new View.Warehouse.SetWarePrice(table, WarehouseModel);
            SetWare.Show();
        }
        public ReactiveCommand<Unit, Unit> OpenAddExcel => ReactiveCommand.Create(() => {
            var addExcel = new View.Warehouse.AddWarehousefromExcel();
            var ExcelViewModel =new  ViewModel.Warehouse.AddFromExcelViewModel(WarehouseModel, MarkModel);
            addExcel.DataContext = ExcelViewModel;
            addExcel.Show();
        });
        public ReactiveCommand<WarehouseTable, Unit> OpenGoodCard=>ReactiveCommand.Create<WarehouseTable>(OpenGoodCardCommand);
        private void OpenGoodCardCommand(WarehouseTable warehouseTable)
        {
           
            var GoodCardViewModel = new ViewModel.Warehouse.GoodCardViewModel(WarehouseModel, MarkModel,  WarehouseTable.NewTable(warehouseTable));
            var CardCood = new View.Warehouse.CardGood(GoodCardViewModel, MarkModel);
            MainControl = CardCood;
        }
        public ReactiveCommand<WarehouseTable, Unit> OpenGoodCardVirtual => ReactiveCommand.Create<WarehouseTable>(OpenGoodCardCommandVirtual);
        private void OpenGoodCardCommandVirtual(WarehouseTable warehouseTable)
        {

            var GoodCardViewModel = new ViewModel.Warehouse.GoodCardViewModel(WarehouseModel, MarkModel, WarehouseTable.NewTable(warehouseTable),true);
            var CardCood = new View.Warehouse.CardGood(GoodCardViewModel, MarkModel);
            MainControl = CardCood;
        }
        #endregion
    }
}
