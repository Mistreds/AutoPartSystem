using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientTable.xaml
    /// </summary>
    public partial class ClientTable : UserControl
    {
        public ClientTable()
        {
            InitializeComponent();
        }
        private ClientWindow clientWindow;
        private Data.Invoice Invoice;
        public ClientTable(Data.Invoice Invoice,ClientWindow clientWindow)
        {
            InitializeComponent();
            this.clientWindow = clientWindow;
            this.Invoice = Invoice;
            clientTable.IsReadOnly = true;
            clientTable.MouseDoubleClick += ClientTable_MouseintClick;
            
        }

        private void ClientTable_MouseintClick(object sender, MouseButtonEventArgs e)
        {
            Data.Client? client = clientTable.SelectedItem as Data.Client;
            if(client!=null)
            {
                Invoice.Client = new Data.Client { Id = client.Id, Name = client.Name, Mark = client.Model.Mark, City = client.City, Model = client.Model, MarkId=client.Model.MarkId, CityId=client.CityId, ModelId=client.ModelId, PhoneName=client.PhoneName, CityName=client.City.Name  };
                Console.WriteLine(Invoice.Client.MarkId);
                clientWindow.Close();
            }
        }
    }
}
