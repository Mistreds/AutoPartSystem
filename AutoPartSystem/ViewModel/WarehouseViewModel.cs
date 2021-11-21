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
        private ObservableCollection<Data.Model> _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set=>this.RaiseAndSetIfChanged(ref _models,value);
        }
        private ObservableCollection<Data.Mark> _mark;
        public ObservableCollection<Data.Mark> Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        private ObservableCollection<UserControl>? _controls;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Warehouse _warehouse;
        public Data.Warehouse Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        public WarehouseViewModel()
        {
            _controls = new ObservableCollection<UserControl> { new View.Warehouse.WarehouseTable(), new View.Warehouse.AddToWarehouse() };
            MainControl = _controls[0];
            MarkModel=new Model.MarkModel.MarkModel();
            
            Mark=MarkModel.GetMark();
            Warehouse=new Data.Warehouse();
        }
        public ReactiveCommand<string, Unit> OpenPage => ReactiveCommand.Create<string>(OpenPageCommand);
        private void OpenPageCommand(string page_id)
        {
            MainControl = _controls[Convert.ToInt32(page_id)];
        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand);
        private void SelectModelFromMarkCommand(int mark_id)
        {
            Models = MarkModel.GetModelFromMarkId(mark_id);
        }
    }
}
