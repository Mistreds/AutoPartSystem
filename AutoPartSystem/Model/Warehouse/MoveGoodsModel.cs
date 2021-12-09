using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
namespace AutoPartSystem.Model.Warehouse
{
    public interface IMoveGoodsModel
    {
        public void AddMainMove(Data.MainMove mainMove);
        public ObservableCollection<Data.MainMove> GetMainMove();
        public void RemoveMove(Data.MainMove mainMove);
    }
    public class MoveGoodsModel:ReactiveObject, IMoveGoodsModel
    {
        private ObservableCollection<Data.MainMove> _mainMoves;
        public ObservableCollection<Data.MainMove> MainMoves
        {
            get => _mainMoves;
            set=>this.RaiseAndSetIfChanged(ref _mainMoves, value);
        }
        public MoveGoodsModel()
        {
            GetMoveFromDb();
            
        }
        private void GetMoveFromDb()
        {
            using var db = new Data.ConDB();
            switch (ViewModel.MainViewModel.Employee.PositionId)
            {
                case 1:
                    MainMoves = new ObservableCollection<Data.MainMove>(db.MainMove.Include(p => p.Employee).Include(p => p.CityTo).Include(p => p.CityFrom).Include(p => p.MoveGoods).ThenInclude(p => p.Warehouse).ThenInclude(p => p.Goods).ThenInclude(p => p.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Select(p => new Data.MainMove(p.Id, p.Employee, p.CityFrom, p.CityTo, p.MoveGoods)).ToList());
                    break;
                case 2:
                    MainMoves = new ObservableCollection<Data.MainMove>(db.MainMove.Include(p => p.Employee).Include(p => p.CityTo).Include(p => p.CityFrom).Include(p => p.MoveGoods).ThenInclude(p => p.Warehouse).ThenInclude(p => p.Goods).ThenInclude(p => p.GoodsModel).ThenInclude(p => p.Model).ThenInclude(p => p.Mark).Where(p => p.CityToId == ViewModel.MainViewModel.Employee.CityId).Select(p => new Data.MainMove(p.Id, p.Employee, p.CityFrom, p.CityTo, p.MoveGoods)).ToList());
                    break;
            }
        }
        public void AddMainMove(MainMove mainMove)
        {
            using var db=new Data.ConDB();
            db.MainMove.Add(mainMove); ;
            db.SaveChanges();
        }

        public ObservableCollection<MainMove> GetMainMove()
        {
            return MainMoves;
        }

        public void RemoveMove(MainMove mainMove)
        {
            using var db = new Data.ConDB();
            db.Remove(mainMove);
            db.SaveChanges();
            GetMoveFromDb();
        }
    }
}
