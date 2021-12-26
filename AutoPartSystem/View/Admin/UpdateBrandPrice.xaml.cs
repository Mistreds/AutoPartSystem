using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для UpdateBrandPrice.xaml
    /// </summary>
    public partial class UpdateBrandPrice : Window
    {
        private Data.Brand brand;
        public UpdateBrandPrice(Data.Brand brand)
        {
            InitializeComponent();
            this.brand = brand;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainViewModel.WarehouseModel.UpdateBrandPrice(brand, Convert.ToInt32(Proc.Text));
            Close();
        }
    }
}
