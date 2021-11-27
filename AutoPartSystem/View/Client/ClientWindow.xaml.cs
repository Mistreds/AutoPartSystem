using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace AutoPartSystem.View.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ObservableCollection<Data.Client> clients { get; set; }  
        public View.Client.ClientTable ClientTable { get; set; }
        public Data.Invoice Invoice { get; set; }
        public ClientWindow(ObservableCollection<Data.Client> clients, Data.Invoice invoice)
        {
            InitializeComponent();
            this.clients = clients;
            Invoice = invoice;
            DataContext = this;
            ClientTable = new ClientTable(this);
        }
    }
}
