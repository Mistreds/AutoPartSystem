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
            Client=new ObservableCollection<Data.Client>(db.Clients.ToList());
        }

        public void AddClient(Data.Client client)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Data.Client> GetClient()
        {
            throw new NotImplementedException();
        }
    }
}
