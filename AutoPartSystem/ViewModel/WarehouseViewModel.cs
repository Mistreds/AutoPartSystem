using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class WarehouseViewModel:ReactiveObject
    {
        private UserControl? _main_control;
        public UserControl? MainControl
        {
            get => _main_control;
            set=>this.RaiseAndSetIfChanged(ref _main_control, value);
        }
        private ObservableCollection<UserControl>? _controls;
        public WarehouseViewModel()
        {
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(), new View.Warehouse.AddToWarehouse() };
            MainControl = _controls[0];
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            MainControl = _controls[Convert.ToInt32(page_id)];
        }
    }
}
