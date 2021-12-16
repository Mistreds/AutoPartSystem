using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace Data
{
    public class Mark:MainClass, IMainInterface
    {
        public Mark() { }
        public Mark(string name)
        {
            this.Name = name;
        }
    }
    public class Model : MainClass, IMainInterface
    {
        private int _mark_id;
        public int MarkId
        {
            get=> _mark_id;
            set=>this.RaiseAndSetIfChanged(ref _mark_id, value);
        }
        private Mark _mark;
        public Mark Mark
        {
            get=> _mark;
            set=>this.RaiseAndSetIfChanged(ref _mark, value);
        }
        public  Model(){}

        public Model(string name, Mark Mark)
        {

            Name = name;
            this.Mark = Mark;
        }
        public Model(string name, int MarkId)
        {

            Name = name;
            this.MarkId = MarkId;
        }
        public Model(string name, int MarkId, Mark Mark)
        {

            Name = name;
            this.MarkId = MarkId;
            this.Mark = Mark;
        }
    }
    public class Brand : MainClass
    {
        
        public Brand() { }
        public Brand(string Name)
        {
            this.Name = Name;
        }
        public Brand(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
