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
namespace AutoPartSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Data.Employee employee)
        {

            InitializeComponent();
            DataContext=new ViewModel.MainViewModel(employee);
            this.Title = $"Сотружник {employee.Position.Name.ToLower() } {employee.Name } город {employee.City.Name}";
            AddNewGood.Visibility =  Setting.BoolToVisibility(employee.SetGood);
            AddNewVirtualGood.Visibility = Setting.BoolToVisibility(employee.SetGood);
         
            AdminMenu.Visibility = Setting.BoolToVisibility(employee.IsAdmin);
            Report.Visibility = Setting.BoolToVisibility(employee.SetReport);
            MoveAgent.Visibility = Setting.BoolToVisibility(employee.SetMoveAgent);
            AgentMenu.Visibility = Setting.BoolToVisibility(employee.SetMoveAgent);
            MoveCity.Visibility= Setting.BoolToVisibility(employee.SetMoveCity);
            Invoce.Visibility = Setting.BoolToVisibility(employee.SetCell);
            ComInvoice.Visibility = Setting.BoolToVisibility(employee.SetCell);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
