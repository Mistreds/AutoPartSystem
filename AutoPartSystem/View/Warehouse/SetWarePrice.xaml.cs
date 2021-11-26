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
    /// Логика взаимодействия для SetWarePrice.xaml
    /// </summary>
    public partial class SetWarePrice : Window
    {
        private ViewModel.WarehouseTable _table;
        public ViewModel.WarehouseTable table
        {
            get => _table;
            set => _table = value;
        }
        private Model.Warehouse.WarehouseModel model;
        public SetWarePrice(ViewModel.WarehouseTable table, Model.Warehouse.WarehouseModel model)
        {
            InitializeComponent();
            this._table = ViewModel.WarehouseTable.NewTable(table);
            this.model = model;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.UpdateWarehouseMainPrice(table.Goods.InputPrice, table.Goods.RecomPrice, table.Id);
        }
    }
}
