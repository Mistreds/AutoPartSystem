
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
using ReactiveUI;
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
        public ObservableCollection<Brand> GetBrand();
        public void AddBrand(string brand);
        public void UpdateBrand(Brand brand);
        public ObservableCollection<Brand> GetBrandFromName(string name);
    }
    public class MarkModel :ReactiveObject, IMarkModel
    {
        private ObservableCollection<Data.Model> _model;
        public ObservableCollection<Data.Model> Model
        {
            get => _model;
            private set {
                Console.WriteLine("блаблабла");
                this.RaiseAndSetIfChanged(ref _model, value);
            }
        }
        private ObservableCollection<Data.Brand> _brand;
        public ObservableCollection<Data.Brand> Brand
        {
            get=> _brand;
            set=>this.RaiseAndSetIfChanged(ref _brand, value);
        }
        private ObservableCollection<Data.Mark>? Mark;
        private ObservableCollection<ViewModel.MarkModelFind> _markModel;
        public ObservableCollection<MarkModelFind> MarkModelFinds
        {
            get=> _markModel;
            set=>this.RaiseAndSetIfChanged(ref _markModel, value);
        }
        private void GetBrandFromDb()
        {
            using var db = new Data.ConDB();

            Brand = new ObservableCollection<Brand>(db.Brands.Where(p=>p.Id!=1).ToList());
        }
        public MarkModel()
        {
            Console.WriteLine("Тип создалась модель");
            Model = GetModels();
            
            Mark = GetMark();
            GetBrandFromDb();
            MarkModelFinds = new ObservableCollection<MarkModelFind>(Model.Select(p => new ViewModel.MarkModelFind(p.Id, $"{p.Mark.Name} {p.Name}")).OrderBy(p => p.model_name));
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
            Model.Add(model);
            
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
            var a= new ObservableCollection<MarkModelFind>(MarkModelFinds.Where(p => p.model_name.ToLower().Contains(name.ToLower())).OrderBy(p=>p.model_name));
            a.Insert(0, new ViewModel.MarkModelFind(0, "Выделить все"));
            return a;
        }
        public ObservableCollection<Brand> GetBrand()
        {
            return Brand;
        }
        public void AddBrand(string brand_name)
        {
            var brand=new Brand(brand_name);
            using var db = new Data.ConDB();
            db.Add(brand);
            db.SaveChanges();
            Brand.Add(brand);
        }
        public void UpdateBrand(Brand brand)
        {
            using var db = new Data.ConDB();
            db.Update(brand);
            db.SaveChanges();
        }

        public ObservableCollection<Brand> GetBrandFromName(string name)
        {
            return new ObservableCollection<Brand>(Brand.Where(p => p.Name.ToLower().Contains(name.ToLower())));
        }
    }
}
