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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace AutoPartSystem.View.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для ArrivelGoods.xaml
    /// </summary>
    public partial class ArrivelGoods : Window
    {
        public ViewModel.WarehouseTable table { get; set; }
        private Model.Warehouse.WarehouseModel model;
        public ArrivelGoods(ViewModel.WarehouseTable table, Model.Warehouse.WarehouseModel model)
        {
            InitializeComponent();
            this.table = ViewModel.WarehouseTable.NewTable(table);
            this.model = model;
            DataContext = this;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Приход возможно отменить только через администратора, продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                
                model.UpdateWarehouseCount(Convert.ToInt32(Almata.Text), Convert.ToInt32(Astana.Text), Convert.ToInt32(Actau.Text), table.Id);
                Close();
            }            
        }
    }
}
