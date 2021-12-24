using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartSystem
{
    public static class ObservableCollectionExtension
    {
        //public static int UpdateFromId(this ObservableCollection<T> str, char c)
        //{
        //    int counter = 0;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if (str[i] == c)
        //            counter++;
        //    }
        //    return counter;
        //}
    }
    public static class Setting
    {
        public static Visibility BoolToVisibility(bool vis)
        {
            if(vis)
            {
                return Visibility.Visible;
            }
            else
            {
               return Visibility.Collapsed;
            }
        }
    }
}
