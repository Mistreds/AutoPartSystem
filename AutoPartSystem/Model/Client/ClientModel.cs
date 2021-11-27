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
            using var db = new Data.ConDB();
            db.Clients.Add(client);
            db.SaveChanges();
            Client.Add(client);
        }

        public ObservableCollection<Data.Client> GetClient()
        {
            return Client;
        }
    }
}
