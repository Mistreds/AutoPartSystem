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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : UserControl
    {
        public ClientPage()
        {
            InitializeComponent();
            DataContext = ViewModel.MainViewModel.ClientViewModel;
            if(!ViewModel.MainViewModel.Employee.SetCell)
            {
                AddNewClient.Visibility = Visibility.Collapsed;
            }
            if(!ViewModel.MainViewModel.Employee.SetMoveAgent)
            {
                AddNewAgent.Visibility = Visibility.Collapsed;  
            }
        }
    }
}
