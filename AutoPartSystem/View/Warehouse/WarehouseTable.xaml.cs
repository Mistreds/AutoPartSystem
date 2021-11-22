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
        private int id;
        private int modelId;
        private Data.Model model;
        private string description;
        private string article;
        private int inAlmata;
        private int inAstana;
        private int inAktau;
        private double inputPrice;
        private int warehousePlace;
        private double recomPrice;
        private string typePay;
        private string note;

        public WarehouseTable()
        {
            InitializeComponent();
        }


        private void DataGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString(); 
        }
    }
}
