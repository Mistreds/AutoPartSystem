using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.Model.Client
{
    public interface IClientModel
    {
        public ObservableCollection<Data.Client> GetClient();
        public ObservableCollection<Data.Client> GetAgent();
        public ObservableCollection<Data.Client> FindClientFromNameOrPhone(string name, string phone);
        public ObservableCollection<Data.Client> FindAgentFromNameOrPhone(string name, string phone);
        public void AddClient(Data.Client client);
       

    }
   public class ClientModel: ReactiveObject, IClientModel 
    {
        private ObservableCollection<Data.Client> _client;
        public ObservableCollection<Data.Client> Client
        {
            get => _client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }
        private ObservableCollection<Data.Client> _agent;
        public  ObservableCollection<Data.Client> AgentClient
        {
            get => _agent;
            set => this.RaiseAndSetIfChanged(ref _agent, value);
        }
        public ClientModel()
        {
            UpdateAll();
        }
        private void UpdateAll()
        {
            using var db = new Data.ConDB();
            Client = new ObservableCollection<Data.Client>(db.Clients.Include(p => p.City).Include(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsAgent == false).Select(p => new Data.Client(p)).ToList());
            AgentClient = new ObservableCollection<Data.Client>(db.Clients.Include(p => p.City).Include(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.IsAgent == true).Select(p => new Data.Client(p)).ToList());
        }
        public void AddClient(Data.Client client)
        {
            var ss = new Data.Client(client);
            using var db = new Data.ConDB();
            if (ss.City.Id!=0)
            {
                ss.CityId=client.City.Id;
                ss.City = null;
            }
            ss.ModelId=client.Model.Id;
            ss.Model = null;
            if (ss.Id != 0) return;
            db.Clients.Add(ss);
            db.SaveChanges();
            client.Id=ss.Id;
            UpdateAll();
            ViewModel.MainViewModel.AdminModel.UpdateCity();
        }
        public void AddClientAgent(Data.Client client)
        {
            var ss = new Data.Client(client, true);
            using var db = new Data.ConDB();
            if (ss.City.Id != 0)
            {
                ss.CityId = client.City.Id;
                ss.City = null;
            }
            if (ss.Id != 0) return;
            db.Clients.Add(ss);
            db.SaveChanges();
            var clientt = db.Clients.Include(p => p.City).Include(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.Id == ss.Id).Select(p => new Data.Client(p)).FirstOrDefault();
            AgentClient.Add(clientt);
            client.Id = ss.Id;
            UpdateAll();
            ViewModel.MainViewModel.AdminModel.UpdateCity();
        }

        public ObservableCollection<Data.Client> FindAgentFromNameOrPhone(string name, string phone)
        {
            if (phone == null) phone = "";
            if (name == null) name = "";
            return new ObservableCollection<Data.Client>(AgentClient.Where(p => p.Name.ToLower().Contains(name.ToLower()) && p.PhoneName.ToLower().Contains(phone.ToLower())));
        }

        public ObservableCollection<Data.Client> FindClientFromNameOrPhone(string name, string phone)
        {
            if (phone == null) phone = "";
            if (name == null) name = "";
            return new ObservableCollection<Data.Client>(Client.Where(p => p.Name.ToLower().Contains(name.ToLower()) && p.PhoneName.ToLower().Contains(phone.ToLower())));
        }

        public ObservableCollection<Data.Client> GetAgent()
        {
            return AgentClient;
        }

        public ObservableCollection<Data.Client> GetClient()
        {
            return Client;
        }
    }
}
