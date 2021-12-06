using Microsoft.EntityFrameworkCore;
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
        public void AddClient(Data.Client client);
       

    }
   public class ClientModel: IClientModel
    {
        private ObservableCollection<Data.Client> Client;
        public ClientModel()
        {
            using var db=new Data.ConDB();
            Client=new ObservableCollection<Data.Client>(db.Clients.Include(p=>p.City).Include(p=>p.Model).ThenInclude(p=>p.Mark).Select(p=>new Data.Client(p)).ToList());
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
            var clientt = db.Clients.Include(p => p.City).Include(p => p.Model).ThenInclude(p => p.Mark).Where(p=>p.Id== ss.Id).Select(p => new Data.Client(p)).FirstOrDefault();
            Client.Add(clientt);
            client.Id=ss.Id;
            ViewModel.MainViewModel.AdminModel.UpdateCity();
        }

        public ObservableCollection<Data.Client> GetClient()
        {
            return Client;
        }
    }
}
