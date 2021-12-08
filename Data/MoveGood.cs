using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MainMove : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private ObservableCollection<MoveGoods> _move_goods;
        public ObservableCollection<MoveGoods> MoveGoods
        {
            get=> _move_goods;
            set=>this.RaiseAndSetIfChanged(ref _move_goods, value);
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set=>this.RaiseAndSetIfChanged(ref _date, value);
        }
        private int _employee_id;
        public int EmployeeId
        {
            get => _employee_id;
            set => this.RaiseAndSetIfChanged(ref _employee_id, value);
        }
        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);
        }
        private int _city_from_id;
        public int CityFromId
        {
            get => _city_from_id;
            set => this.RaiseAndSetIfChanged(ref _city_from_id, value);
        }
        private City _city_from;
        public City CityFrom
        {
            get => _city_from;
            set => this.RaiseAndSetIfChanged(ref _city_from, value);
        }
        private int _city_to_id;
        public int CityToId
        {
            get => _city_to_id;
            set => this.RaiseAndSetIfChanged(ref _city_to_id, value);
        }
        private City _city_to;
        public City CityTo
        {
            get => _city_to;
            set => this.RaiseAndSetIfChanged(ref _city_to, value);
        }
    }
    public class MoveGoods : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
        private int _warehouse_id;
        public int WarehouseId
        {
            get => _warehouse_id;
            set => this.RaiseAndSetIfChanged(ref _warehouse_id, value);
        }
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private int _count;
        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }
        private int _main_move;
        public int MainMove
        {
            get => _main_move;
            set=>this.RaiseAndSetIfChanged(ref _main_move,value);
        }
        
       
    }
}
