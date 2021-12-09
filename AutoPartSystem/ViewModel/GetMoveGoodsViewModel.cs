using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace AutoPartSystem.ViewModel
{
    public class GetMoveGoodsViewModel:ReactiveObject
    {
        private ObservableCollection<Data.MainMove> _main_move;
        public ObservableCollection<Data.MainMove> MainMove
        {
            get => _main_move;
            set => this.RaiseAndSetIfChanged(ref _main_move, value);
        }
        public GetMoveGoodsViewModel()
        {
            SelectMove();
            ViewModel.MainViewModel.MoveGoodsModel.WhenAnyValue(vm => vm.MainMoves.Count).Subscribe(_ => SelectMove());
        }
        private void SelectMove()
        {
            MainMove = ViewModel.MainViewModel.MoveGoodsModel.GetMainMove();
        }
        public ReactiveCommand<Data.MainMove, Unit> ArriveGoods=>ReactiveCommand.Create<Data.MainMove>(MainViewModel.WarehouseModel.ArriveGoods);
       
        
    }
}
