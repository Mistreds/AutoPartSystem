﻿using System;
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
using System.Windows.Shapes;

namespace AutoPartSystem.View.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для AddToWarehousePage.xaml
    /// </summary>
    public partial class AddToWarehousePage : Window
    {
        public AddToWarehousePage()
        {
            InitializeComponent();
        }
        private void ComboBoxMark_TextInput(object sender, TextCompositionEventArgs e)
        {

            //ComboBoxMark.ItemsSource = ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkFromName(e.Text);
            Console.WriteLine(e.Text);
            if (e.Text == null)
            {
                Console.WriteLine("dsad");
            }
        }

        private void ComboBoxMark_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            if (tb.SelectionStart != 0)
            {
                ComboBoxMark.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && ComboBoxMark.SelectedItem == null)
            {
                ComboBoxMark.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }
            ComboBoxMark.IsDropDownOpen = true;
            if (ComboBoxMark.SelectedItem == null)
            {
                ComboBoxMark.ItemsSource = new ObservableCollection<Data.Model>();
                if (ViewModel.MainViewModel.WarehouseViewModel != null)
                    if (ViewModel.MainViewModel.WarehouseViewModel.MarkModel != null)
                        ComboBoxMark.ItemsSource =
                            ViewModel.MainViewModel.WarehouseViewModel.MarkModel.GetMarkFromName(text);
            }
        }
        private void ComboBoxModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            var text = tb.Text;
            if (tb.SelectionStart != 0)
            {
                ComboBoxModel.SelectedItem = null; // Если набирается текст сбросить выбраный элемент
            }
            if (tb.SelectionStart == 0 && ComboBoxModel.SelectedItem == null)
            {
                ComboBoxMark.IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }

            ComboBoxModel.IsDropDownOpen = true;
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
                    ComboBoxMark.ItemsSource = new ObservableCollection<Data.Model>();
                }
            }
        }
    }
}