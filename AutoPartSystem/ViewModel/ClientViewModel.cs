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
        private ObservableCollection<Data.Client> _client_agent;
        public ObservableCollection<Data.Client> ClientAgent
        {
            get => _client_agent;
            set => this.RaiseAndSetIfChanged(ref _client_agent, value);
        }
        private ObservableCollection<UserControl> _control;
        private UserControl _main_user;
        public UserControl MainControl
        {
            get => _main_user;
            set => this.RaiseAndSetIfChanged(ref _main_user, value);
        }

        private string _client_name;
        public string ClientName
        {
            get => _client_name;
            set=>this.RaiseAndSetIfChanged(ref _client_name, value);
        }
        private string _client_phone;
        public string ClientPhone
        {
            get => _client_phone;
            set=>this.RaiseAndSetIfChanged(ref _client_name,value);
        }
        public ClientViewModel(Model.MarkModel.MarkModel markModel)
        {

            _control = new ObservableCollection<UserControl> { new View.Client.ClientTable(), new View.Client.AgentTable() };
            MarkModel = markModel;
            AdminModel = MainViewModel.AdminModel;
            Mark = MarkModel.GetMark();
            Client = new Data.Client();
            
            Client.new_mark_model();
            _cities = AdminModel.GetCities();
            MainControl = _control[0];
            ClientTable = MainViewModel.ClientModel.GetClient();
            ClientAgent = MainViewModel.ClientModel.GetAgent();
            MainViewModel.ClientModel.WhenAnyValue(p => p.AgentClient.Count).Subscribe(_ => UpdateAgent());
            MainViewModel.ClientModel.WhenAnyValue(p => p.Client.Count).Subscribe(_ => UpdateClient());
        }

        public ClientViewModel(Data.Invoice invoice)
        {
            
            ClientTable = MainViewModel.ClientModel.GetClient();
            View.Client.ClientWindow clientWindow = new View.Client.ClientWindow(MainViewModel.ClientModel.GetClient(), invoice, this);
            _control = new ObservableCollection<UserControl> { new View.Client.ClientTable(invoice, clientWindow) };
            MainControl = _control[0];
            clientWindow.Show();
        }
        public ClientViewModel(Data.Invoice invoice,bool isagent)
        {

            ClientAgent = MainViewModel.ClientModel.GetAgent();
            View.Client.ClientWindow clientWindow = new View.Client.ClientWindow(MainViewModel.ClientModel.GetClient(), invoice, this);
            _control = new ObservableCollection<UserControl> { new View.Client.AgentTable(invoice, clientWindow) };
            MainControl = _control[0];
            clientWindow.Show();
        }
        private void UpdateModels(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
        private void UpdateAgent() 
        {
            ClientAgent = MainViewModel.ClientModel.GetAgent();
        }
        private void UpdateClient()
        {
            ClientTable = MainViewModel.ClientModel.GetClient();
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        public void OpenPageCommand(string command)
        {
            switch (command)
            {
                case "OpenClientTable":
                    MainControl = _control[0];
                    break;
                case "OpenAgentClientTable":
                    MainControl = _control[1];
                    break;
                case "NewClient":
                    Client = new Data.Client();
                    Client.new_mark_model();
                    this.WhenAnyValue(vm => vm.Client.Mark).Subscribe(x => UpdateModels(x.Id));
                     new_client = new View.Client.NewClient();
                    new_client.Show();
                    break;
                case "NewAgentClient":
                    Client = new Data.Client();
                    Client.ModelId = 1;
                    Client.IsAgent = true;
                     new_client_ag = new View.Client.NewClient();
                    new_client_ag.Show();
                    break;
            }
        }
        private View.Client.NewClient new_client;
        private View.Client.NewClient new_client_ag;
        public ReactiveCommand<Unit, Unit> AddClient => ReactiveCommand.Create(() => { 
        
                MainViewModel.ClientModel.AddClient(Client);
            new_client.Close();
        });
        public ReactiveCommand<Unit, Unit> AddAgent => ReactiveCommand.Create(() => {

            MainViewModel.ClientModel.AddClientAgent(Client);
            new_client_ag.Close();

        });
        public ReactiveCommand<Unit, Unit> FindClient => ReactiveCommand.Create(() => {

            ClientTable = MainViewModel.ClientModel.FindClientFromNameOrPhone(ClientName, ClientPhone);
        });
        public ReactiveCommand<Unit, Unit> FindAgent => ReactiveCommand.Create(() => {

           ClientAgent = MainViewModel.ClientModel.FindAgentFromNameOrPhone(ClientName, ClientPhone);
        });
    }
}
