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

namespace AutoPartSystem.View.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для CardGood.xaml
    /// </summary>
    public partial class CardGood : UserControl
    {
        private Model.MarkModel.MarkModel MarkModel;
        private bool is_model_start;
        private bool is_model_start1;
        private bool is_brand_start;
        private bool is_mark_start;
        private bool is_mark_start1;
        public CardGood(ViewModel.Warehouse.GoodCardViewModel goodCardViewModel, Model.MarkModel.MarkModel MarkModel)
        {
            InitializeComponent();
            DataContext = goodCardViewModel;
            this.MarkModel = MarkModel;  
        }
        private void ComboBoxMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!is_mark_start) { is_mark_start = !is_mark_start;return; }
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            tb.CaretIndex = tb.Text.Length;
            Console.WriteLine("dasdas " + text.Length);
            if (tb.SelectionStart != 0)
            {
                ComboBoxMark.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && ComboBoxMark.SelectedItem == null)
            {
                ComboBoxMark.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            // tb.Select(0, 0);
            ComboBoxMark.IsDropDownOpen = true;
            if (ComboBoxMark.SelectedItem == null)
            {

                ComboBoxMark.ItemsSource = new ObservableCollection<Data.Model>();
                if (ViewModel.MainViewModel.WarehouseViewModel != null)
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                    {
                        ComboBoxMark.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkFromName(text);
                        int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
                        ComboBoxModel.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(mark_id);
                    }
                       
            }
        }
        private void ComboBoxModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!is_model_start) { is_model_start = !is_model_start; return; }
           
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            Console.WriteLine("adasd" + text.Length);
            if (tb.SelectionStart != 0)
            {
                ComboBoxModel.SelectedItem = null;
                tb.Text = text;
                
                if(!ComboBoxModel.IsDropDownOpen)
                ComboBoxModel.IsDropDownOpen = true; // Если набирается текст сбросить выбраный элемент
                tb.CaretIndex = tb.Text.Length;
                tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            }
            if (tb.SelectionStart == 0 && ComboBoxModel.SelectedItem == null)
            {
                ComboBoxModel.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }  
            if (ComboBoxModel.SelectedItem == null)
            {

                if (ComboBoxMark.SelectedItem is Data.Mark mark)
                {
                    if (ViewModel.MainViewModel.WarehouseViewModel == null) return;
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                        ComboBoxModel.ItemsSource =
                            ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(text, mark.Id);
                }
                else
                {
                    int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
                    if (mark_id != 0)
                    {
                        ComboBoxModel.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(text, mark_id);
                    }
                    else
                    {
                        ComboBoxModel.ItemsSource = new ObservableCollection<Data.Model>();
                    }

                }
            }
            else
            {
                int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
                ComboBoxModel.ItemsSource =
                   ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(mark_id);
            }
        }
        private void ComboBoxMark_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;

            tb.CaretIndex = tb.Text.Length;
        }
        private void ComboBoxMark_KeyDown(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            tb.CaretIndex = tb.Text.Length;
        }
        private void ComboBoxMark_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!is_brand_start) { is_brand_start = !is_brand_start;return; }
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;

            tb.CaretIndex = tb.Text.Length;
            Console.WriteLine(",fk,kf,kf"+text.Length);
            ComboBoxBrand.SelectedItem = null;
            if (text.Length == 0)
            {
                ComboBoxBrand.ItemsSource = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetBrand();
            }
            ComboBoxBrand.IsDropDownOpen = true;
            if (ComboBoxBrand.SelectedItem == null)
            {
                ComboBoxBrand.ItemsSource = new ObservableCollection<Data.Brand>();
                if (ViewModel.MainViewModel.WarehouseViewModel != null)
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                        ComboBoxBrand.ItemsSource =
                            ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetBrandFromName(text);
            }
        }

        private void ComboBoxModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if(ComboBoxModel.SelectedItem != null)
            {
                Console.WriteLine("dsadasdsasdasddasds");
            int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
            ComboBoxModel.ItemsSource =
               ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(mark_id);
                if(is_model_start)
                is_model_start = !is_model_start;
            }
        }
        private void ComboBoxMark1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!is_mark_start1) { is_mark_start1 = !is_mark_start1; return; }
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            tb.CaretIndex = tb.Text.Length;
           
            if (tb.SelectionStart != 0)
            {
                ComboBoxMark1.SelectedItem = null;
                tb.Text = text;
                Console.WriteLine(text);
                if (!ComboBoxMark1.IsDropDownOpen)
                    ComboBoxMark1.IsDropDownOpen = true; // Если набирается текст сбросить выбраный элемент
                tb.CaretIndex = tb.Text.Length;
                tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            }
            if (tb.SelectionStart == 0 && ComboBoxMark1.SelectedItem == null)
            {
                ComboBoxMark1.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            if (ComboBoxMark1.SelectedItem == null)
            {

                ComboBoxMark1.ItemsSource = new ObservableCollection<Data.Model>();
                if (ViewModel.MainViewModel.WarehouseViewModel != null)
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                    {
                        ComboBoxMark1.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkFromName(text);
                        int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark1.Text);
                        ComboBoxModel1.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(mark_id);
                    }

            }
        }

        private void ComboBoxModel1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!is_model_start1) { is_model_start1 = !is_model_start1; return; }

            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            Console.WriteLine("adasd" + text.Length);
            if (tb.SelectionStart != 0)
            {
                ComboBoxModel1.SelectedItem = null;
                tb.Text = text;

                if (!ComboBoxModel1.IsDropDownOpen)
                    ComboBoxModel1.IsDropDownOpen = true; // Если набирается текст сбросить выбраный элемент
                tb.CaretIndex = tb.Text.Length;
                tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            }
            if (tb.SelectionStart == 0 && ComboBoxModel1.SelectedItem == null)
            {
                ComboBoxModel1.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }


            if (ComboBoxModel1.SelectedItem == null)
            {

                if (ComboBoxMark1.SelectedItem is Data.Mark mark)
                {
                    if (ViewModel.MainViewModel.WarehouseViewModel == null) return;
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                        ComboBoxModel1.ItemsSource =
                            ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(text, mark.Id);
                }
                else
                {
                    int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
                    if (mark_id != 0)
                    {
                        ComboBoxModel1.ItemsSource =
                           ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(text, mark_id);
                    }
                    else
                    {
                        ComboBoxModel1.ItemsSource = new ObservableCollection<Data.Model>();
                    }

                }
            }
            else
            {
                int mark_id = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkIdFromName(ComboBoxMark.Text);
                ComboBoxModel1.ItemsSource =
                   ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(mark_id);
            }
            if(text.Length==0)
            {
                Console.WriteLine("bla "+ Convert.ToInt32(ComboBoxMark1.SelectedValue));
                ComboBoxModel1.ItemsSource =
                   ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetModelFromMarkId(Convert.ToInt32(ComboBoxMark1.SelectedValue));
            }
        }

        private void ComboBoxMark1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("Пздиа");
            
        }
    }
    
}
