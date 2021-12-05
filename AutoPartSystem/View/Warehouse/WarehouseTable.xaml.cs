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

namespace AutoPartSystem.View.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для WarehouseTable.xaml
    /// </summary>
    public partial class WarehouseTable : UserControl
    {
        private Data.Invoice Invoice;
        public WarehouseTable()
        {
            InitializeComponent();
            
        }
        public WarehouseTable(Data.Invoice invoice)
        {
            InitializeComponent();
            Prihod.Visibility = Visibility.Collapsed;
            Prodash.Visibility = Visibility.Collapsed;
            Invoice=invoice;
    }
        private void ModelText_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }
        private void model_sort_a_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddToInvoice(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
