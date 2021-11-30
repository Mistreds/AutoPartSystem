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

namespace AutoPartSystem.View.Invoice
{
    /// <summary>
    /// Логика взаимодействия для InvoiceMainPage.xaml
    /// </summary>
    public partial class InvoiceMainPage : UserControl
    {
        public InvoiceMainPage()
        {
            InitializeComponent();
            DataContext = ViewModel.MainViewModel.InvoiceViewModel;
        }
    }
}
