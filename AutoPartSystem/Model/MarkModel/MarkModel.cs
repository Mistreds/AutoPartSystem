
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using AutoPartSystem.ViewModel;

namespace AutoPartSystem.Model.MarkModel
{
    public interface IMarkModel
    {
        public ObservableCollection<Data.Model> GetModels();
        public ObservableCollection<Data.Mark> GetMark();
        public ObservableCollection<Data.Model> GetModelFromMarkId(int markId);
        public void AddMark(Data.Mark mark);
        public void AddModel(Data.Model model);
        public ObservableCollection<Data.Mark> GetMarkFromName(string name);
        public Mark GetMarkFromNameFind(string name);
        public ObservableCollection<Data.Model> GetModelFromMarkId(string name, int markId);
        public Data.Model GetModelFromNameFind(string name, int id);
        public ObservableCollection<ViewModel.MarkModelFind> MarkModelFind(string name);
        public int GetMarkIdFromName(string name);
    }
    public class MarkModel : IMarkModel
    {
        private ObservableCollection<Data.Model>Model;
        private ObservableCollection<Data.Mark>? Mark;
        public MarkModel()
        {
            Model = GetModels();
            Mark= GetMark();
        }
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
        public ObservableCollection<Mark> GetMarkFromName(string name)
        {
            return new ObservableCollection<Mark>(Mark.Where(p=>p.Name.ToLower().Contains(name.ToLower())).ToList());
        }
        public Mark GetMarkFromNameFind(string name)
        {
            return Mark.FirstOrDefault(p =>string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public int GetMarkIdFromName(string name)
        {
            return Mark.Where(p => p.Name.ToLower().Contains(name.ToLower())).Select(p=>p.Id).FirstOrDefault();
        }

        public ObservableCollection<Data.Model> GetModelFromMarkId(int markId)
        {
            return new ObservableCollection<Data.Model>(Model.Where(p=>p.MarkId==markId).ToList());
        }
        public ObservableCollection<Data.Model> GetModelFromMarkId(string name, int markId)
        {
            return new ObservableCollection<Data.Model>(Model.Where(p => p.Name.ToLower().Contains(name.ToLower()) && p.MarkId==markId).ToList());
        }

        public Data.Model GetModelFromNameFind(string name,int id)
        {
            return Model.FirstOrDefault(p =>string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase) && p.MarkId==id);
        }

        public ObservableCollection<Data.Model> GetModels()
        {
            using var db = new ConDB();
            return new ObservableCollection<Data.Model>(db.Models.Include(p => p.Mark).ToList());
        }

        public ObservableCollection<MarkModelFind> MarkModelFind(string name)
        {
            var a= new ObservableCollection<MarkModelFind>(Model.Where(p => $"{p.Mark.Name} {p.Name}".ToLower().Contains(name.ToLower())).Select(p=>new ViewModel.MarkModelFind(p.Id, $"{p.Mark.Name} {p.Name}")).OrderBy(p=>p.model_name));
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
    }
}
