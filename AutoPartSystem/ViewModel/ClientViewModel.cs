using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class ClientViewModel : ReactiveObject
    {
        private Model.MarkModel.MarkModel MarkModel;
        private Model.Admin.AdminModel AdminModel;
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
        private Data.Client _client;
        public Data.Client Client
        {
            get => _client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }
        private ObservableCollection<Data.Client> _client_table;
        public ObservableCollection<Data.Client> ClientTable
        {
            get => _client_table;
            set => this.RaiseAndSetIfChanged(ref _client_table, value);
        }
        
        private ObservableCollection<UserControl> _control;
        private UserControl _main_user;
        public UserControl MainControl
        {
            get => _main_user;
            set => this.RaiseAndSetIfChanged(ref _main_user, value);
        }
        public ClientViewModel(Model.MarkModel.MarkModel markModel)
        {

            _control = new ObservableCollection<UserControl> { new View.Client.ClientTable() };
            MarkModel = markModel;
            AdminModel = MainViewModel.AdminModel;
            Mark = MarkModel.GetMark();
            Client = new Data.Client();
            Client.new_mark_model();
            _cities = AdminModel.GetCities();
            MainControl = _control[0];
            ClientTable = MainViewModel.ClientModel.GetClient();

        }
        private void UpdateModels(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        public void OpenPageCommand(string command)
        {
            switch (command)
            {
                case "OpenClientTable":
                    MainControl = _control[0];
                    break;
                case "NewClient":
                    Client = new Data.Client();
                    Client.new_mark_model();
                    this.WhenAnyValue(vm => vm.Client.Mark).Subscribe(x => UpdateModels(x.Id));
                    var new_client = new View.Client.NewClient();
                    new_client.Show();
                    break;
            }
        }
        public ReactiveCommand<Unit, Unit> AddClient => ReactiveCommand.Create(() => { 
        
                MainViewModel.ClientModel.AddClient(Client);
                ClientTable = MainViewModel.ClientModel.GetClient();


        });
    }
}
