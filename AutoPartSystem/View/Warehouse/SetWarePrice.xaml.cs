using System.Windows;

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
            model.UpdateWarehouseMainPrice(table.Goods.InputPrice, table.Goods.RecomPrice, table.Goods.InputAstana, table.Goods.InputAktau, table.Id);
        }
    }
}
