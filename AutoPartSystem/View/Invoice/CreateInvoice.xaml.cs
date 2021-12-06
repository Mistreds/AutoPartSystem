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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Invoice
{
    /// <summary>
    /// Логика взаимодействия для CreateInvoice.xaml
    /// </summary>
    public partial class CreateInvoice : UserControl
    {
       private ViewModel.InvoiceWinViewModel invoice_view;
        public CreateInvoice(ViewModel.InvoiceWinViewModel invoice)
        {
            InitializeComponent();

            invoice_view = invoice;
        }
        private void ComboBoxMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("dasdsad");
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            tb.CaretIndex = tb.Text.Length;

            if (tb.SelectionStart != 0)
            {

                //ComboBoxMark.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && ComboBoxMark.SelectedItem == null)
            {
                //ComboBoxMark.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            ComboBoxMark.IsDropDownOpen = true;
            if (ComboBoxMark.SelectedItem == null)
            {
                invoice_view.Cities = new ObservableCollection<Data.City>();
                if (ViewModel.MainViewModel.AdminModel != null)
                       invoice_view.Cities =
                           ViewModel.MainViewModel.AdminModel.GetCitiesFromText(text);
            }
        }
        private void ComboBoxMark_GotFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("dadsadasdsdsad");
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;

            tb.CaretIndex = tb.Text.Length;
        }
        private void ComboBoxMark_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("dasddsadsadsad");
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            tb.CaretIndex = tb.Text.Length;
        }
    }
}
