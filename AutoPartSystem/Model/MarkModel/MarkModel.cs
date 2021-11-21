using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace AutoPartSystem.Model.MarkModel
{
    public interface IMarkModel
    {
        public ObservableCollection<Data.Model> GetModels();
        public ObservableCollection<Data.Mark> GetMark();
        public void AddMark(Data.Mark mark);
        public void AddModel(Data.Model model);
    }
    public class MarkModel : IMarkModel
    {
        public void AddMark(Mark mark)
        {
            using var db = new ConDB();
            db.Mark.Add(mark);
            db.SaveChanges();
        }
        public void AddModel(Data.Model model)
        {
            using var db = new ConDB();
            db.Models.Add(model);
            db.SaveChanges();
        }

        public ObservableCollection<Mark> GetMark()
        {
            using var db = new ConDB();
            return new ObservableCollection<Mark>(db.Mark.ToList());
        }

        public ObservableCollection<Data.Model> GetModels()
        {
            using var db = new ConDB();
            return new ObservableCollection<Data.Model>(db.Models.Include(p => p.Mark).ToList());
        }
    }
}
