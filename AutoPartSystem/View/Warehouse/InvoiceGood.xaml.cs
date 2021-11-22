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

namespace AutoPartSystem.View.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для InvoiceGood.xaml
    /// </summary>
    public partial class InvoiceGood : Window
    {
        public InvoiceGood(ViewModel.InvoiceWinViewModel invoiceWinViewModel)
        {
            InitializeComponent();
            DataContext = invoiceWinViewModel;
        }
    }
}
