using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoPartSystem.ViewModel
{
    public class AdminViewModel:ReactiveObject
    {
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set => this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private UserControl? _emp_control;
        public UserControl? EmpControl
        {
            get => _emp_control;
            set => this.RaiseAndSetIfChanged(ref _emp_control, value);
        }
        private bool _is_add;
        public bool IsAdd
        {
            get => _is_add;
            set
            {
                if(value)
                    EmpControl = _controls_emp[1];
                else
                    EmpControl = _controls_emp[0];
                this.RaiseAndSetIfChanged(ref _is_add, value);
            }
        }
        private List<Data.Position> _positions;
        public List<Data.Position> Positions
        {
            get=> _positions;
        }
        private List<Data.City> _cities;
        public List<Data.City> Cities
        {
            get => _cities;
        }
        private ObservableCollection<Data.City> _client_cities;
        public ObservableCollection<Data.City> ClientCities
        {
            get => _client_cities;
            set => this.RaiseAndSetIfChanged(ref _client_cities,value);
        }
        private ObservableCollection<Data.Employee>? _employers_table;
        public ObservableCollection<Data.Employee>? EmployersTable
        {
            get => _employers_table;
            set => this.RaiseAndSetIfChanged(ref _employers_table, value); 
        }
        private ObservableCollection<Data.Mark>? _mark_table;
        public ObservableCollection<Data.Mark>? MarkTable
        {
            get => _mark_table;
            set => this.RaiseAndSetIfChanged(ref _mark_table, value);
        }
        private ObservableCollection<Data.Model> _model_table;
        public ObservableCollection<Data.Model> ModelTable
        {
            get => _model_table;
            set => this.RaiseAndSetIfChanged(ref _model_table, value);
        }
        private ObservableCollection<Data.Brand> _brand_table;
        public ObservableCollection<Data.Brand> BrandTable
        {
            get => _brand_table;
            set => this.RaiseAndSetIfChanged(ref _brand_table, value);
        }
        private readonly ObservableCollection<UserControl> _controls_emp;
        private readonly ObservableCollection<UserControl> _controls;
        private Model.Admin.AdminModel AdminModel;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Employee add_mark;
        private Data.Model _model;
        public Data.Model AddModel
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        public Data.Employee AddEmployee
        {
            get => add_mark;
            set=>this.RaiseAndSetIfChanged(ref add_mark, value);
        }

        private string _almata_addres;

        public string AlmataAddres
        {
            get => _almata_addres;
            set => this.RaiseAndSetIfChanged(ref _almata_addres, value);
        }

        private string _astana_addres;
        public string AstanaAddres
        {
            get => _astana_addres;
            set => this.RaiseAndSetIfChanged(ref _astana_addres, value);
        }
        private string _aktau_addres;
        public string AktauAddres
        {
            get => _aktau_addres;
            set => this.RaiseAndSetIfChanged(ref _aktau_addres, value);
        }
        
        public AdminViewModel(Model.MarkModel.MarkModel markModel)
        {
            AdminModel = MainViewModel.AdminModel;
            MarkModel = markModel;
            _positions = AdminModel.GetPositions();
            _cities = AdminModel.GetEmpCity();
            ClientCities = AdminModel.GetCityClient();
            EmployersTable = AdminModel.GetEmployees();
            MarkTable = MarkModel.GetMark();
            ModelTable = MarkModel.GetModels();
            BrandTable = MarkModel.GetBrand();
            _controls_emp = new ObservableCollection<UserControl> { new View.Admin.AdminTable(), new View.Admin.AddUser() };
            _controls = new ObservableCollection<UserControl> { new View.Admin.MainAdmin(), new View.Admin.MarkEdit(MarkModel), new View.Admin.CitySetting() };
            MainControl = _controls[0];
            EmpControl = _controls_emp[0];
            AddEmployee = new Data.Employee();
            AdminModel.GetAddres(out _almata_addres, out _almata_addres, out _aktau_addres);
            AddModel=new Data.Model();
        }
        public ReactiveCommand<Unit, Unit> AddEmmployeeCommand => ReactiveCommand.Create(() => {

            EmployersTable = AdminModel.AddEmployers(AddEmployee);
        });
      
        public void OpenPageCommand(string page_id)
        {
            switch(page_id)
            {
                case "AdminEmp":
                    MainControl = _controls[0];
                    break;
                case "AdminModel":
                    MainControl = _controls[1];
                    break;
                case "AdminCity":
                    MainControl = _controls[2];
                    break;
            }
        }
        public ReactiveCommand<Unit,Unit> AddOrTable => ReactiveCommand.Create(() => { IsAdd = !IsAdd; });
        public ReactiveCommand<string, Unit> AddMarkCommand => ReactiveCommand.Create<string>(AddMark);
        private void AddMark(string name)
        {
            MarkModel.AddMark(new Data.Mark(name));
            MarkTable = MarkModel.GetMark();
            var a = _controls[1] as View.Admin.MarkEdit;
            a.MarkText.Clear();
        }
        public ReactiveCommand<string, Unit> AddCityCommand => ReactiveCommand.Create<string>(AddCity);
        private void AddCity(string name)
        {
            AdminModel.AddCity(name);
            var a = _controls[1] as View.Admin.CitySetting;
            a.CityText.Clear();

        }
        public ReactiveCommand<Unit, Unit> SaveCityAddres => ReactiveCommand.Create(() => { AdminModel.SaveAddress(AlmataAddres, AstanaAddres,AktauAddres);});
        public ReactiveCommand<Unit, Unit> AddModelCommand => ReactiveCommand.Create(() => {
        
            MarkModel.AddModel(AddModel);
            AddModel = new Data.Model();
            ModelTable = MarkModel.GetModels();
        });
    }
}
