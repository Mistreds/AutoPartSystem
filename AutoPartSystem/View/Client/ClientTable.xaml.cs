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
        public ClientTable(ClientWindow clientWindow)
        {
            InitializeComponent();
            DataContext = clientWindow;
            this.clientWindow = clientWindow;
            clientTable.ItemsSource = clientWindow.clients;
            clientTable.IsReadOnly = true;
            clientTable.MouseDoubleClick += ClientTable_MouseDoubleClick;
            
        }

        private void ClientTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Data.Client? client = clientTable.SelectedItem as Data.Client;
            if(client!=null)
            {
                clientWindow.Invoice.Client = new Data.Client(client);
                clientWindow.Close();
            }
        }
    }
}
