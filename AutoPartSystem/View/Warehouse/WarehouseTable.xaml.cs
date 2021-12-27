using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            if (ViewModel.MainViewModel.Employee.SetCell && !ViewModel.MainViewModel.Employee.IsAdmin)
            {
                Prihod.Visibility = Visibility.Collapsed;
                NQZArrive.Visibility = Visibility.Collapsed;
                SCOArrive.Visibility = Visibility.Collapsed;
            }
            if (!ViewModel.MainViewModel.Employee.SetCell)
            {
                CellCount.Visibility = Visibility.Collapsed;
                CellPrice.Visibility = Visibility.Collapsed;
                Prodash.Visibility = Visibility.Collapsed;
            }
            if (ViewModel.MainViewModel.Employee.SetGoodCount && ViewModel.MainViewModel.Employee.SetCell)
            {
                Prihod.Visibility = Visibility.Visible;

            }
            if (!ViewModel.MainViewModel.Employee.SetGoodCount)
            {
                Prihod.Visibility = Visibility.Collapsed;
            }
            if (ViewModel.MainViewModel.Employee.SetPrihod && ViewModel.MainViewModel.Employee.SetCell)
            {
                NQZArrive.Visibility = Visibility.Visible;
                SCOArrive.Visibility = Visibility.Visible;

            }
            if (!ViewModel.MainViewModel.Employee.SetPrihod)
            {
                NQZArrive.Visibility = Visibility.Collapsed;
                SCOArrive.Visibility = Visibility.Collapsed;
            }

            this.PreviewKeyDown += WarehouseTable_PreviewKeyDown;

        }

        private void WarehouseTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {

                PopupFind.IsOpen = true;
                TextFindAll.Focus();
            }
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {

                var warehouseTables = warehouse.SelectedItems;
                Console.WriteLine(warehouseTables.Count);
                string text = "";
                bool first = true;
                foreach(var table in warehouseTables)
                {
                    ViewModel.WarehouseTable warehouseTable = (ViewModel.WarehouseTable)table;
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        text += "\n";
                    }
                    text += $"{warehouseTable.Goods.GoodsModel.FirstOrDefault().Model.Mark.Name} {warehouseTable.Goods.GoodsModel.FirstOrDefault().Model.Name}   |  {warehouseTable.Goods.Description}   |  {warehouseTable.InAlmata}  |   {warehouseTable.Goods.Article}  |   {warehouseTable.WarehousePlace}";
                }
                Clipboard.SetText(text);
            }
        }

        public WarehouseTable(Data.Invoice invoice)
        {
            InitializeComponent();
            Prihod.Visibility = Visibility.Collapsed;
            Prodash.Visibility = Visibility.Collapsed;
            Invoice = invoice;
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

        private void warehouse_LoadingRow(object sender, DataGridRowEventArgs e)
        {
              e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void onDragStarted(object sender, DragStartedEventArgs e)

        {

            Thumb t = (Thumb)sender;

            t.Cursor = Cursors.Hand;

        }



        private void onDragDelta(object sender, DragDeltaEventArgs e)

        {
            double yadjust = DescrPopup.Height + e.VerticalChange;
            double xadjust = DescrPopup.Width + e.HorizontalChange;

            if ((xadjust >= 0) && (yadjust >= 0))

            {

                DescrPopup.Width = xadjust;

                DescrPopup.Height = yadjust;

            }

        }
        private void onDragCompleted(object sender, DragCompletedEventArgs e)

        {

            Thumb t = (Thumb)sender;

            t.Cursor = null;

        }

        private void DescrPopup_Loaded(object sender, RoutedEventArgs e)
        {
            // DescrPopup = (Popup)sender;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double yadjust = PupupModel.Height + e.VerticalChange;
            double xadjust = PupupModel.Width + e.HorizontalChange;
            if ((xadjust >= 0) && (yadjust >= 0))
            {
                PupupModel.Width = xadjust;
                PupupModel.Height = yadjust;
            }
        }

        private void Thumb_DragDelta_1(object sender, DragDeltaEventArgs e)
        {
            double yadjust = PopupArticle.Height + e.VerticalChange;
            double xadjust = PopupArticle.Width + e.HorizontalChange;
            if ((xadjust >= 0) && (yadjust >= 0))
            {
                PopupArticle.Width = xadjust;
                PopupArticle.Height = yadjust;
            }
        }

        private void warehouse_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("dsadasd");
            if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {

                PopupFind.IsOpen = true;
                TextFindAll.Focus();
            }
        }

        private void warehousetable_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

            this.Focus();
        }

        private void warehouse_MouseMove(object sender, MouseEventArgs e)
        {
            // Console.WriteLine("ydfadfaf");
            //if(!PopupFind.IsOpen)

        }

        private void warehouse_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {


        }

        private void warehouse_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            Console.WriteLine("sdad");

        }

        private void PopupArticle_Opened(object sender, EventArgs e)
        {
            TextFindAll.Focus();
        }

        private void warehouse_CopyingRowClipboardContent(object sender, DataGridRowClipboardEventArgs e)
        {

        }

        private void warehouse_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Thumb_DragDelta_2(object sender, DragDeltaEventArgs e)
        {

        }
    }
}
