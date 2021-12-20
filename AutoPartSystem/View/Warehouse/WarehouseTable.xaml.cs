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
            if(ViewModel.MainViewModel.PositId==3)
            {
                Prihod.Visibility = Visibility.Collapsed;
                NQZArrive.Visibility = Visibility.Collapsed;
                SCOArrive.Visibility = Visibility.Collapsed;
            }
            
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
    }
}
