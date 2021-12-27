using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartSystem.Model
{
   public class MonthYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MonthYear(int Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }

}
