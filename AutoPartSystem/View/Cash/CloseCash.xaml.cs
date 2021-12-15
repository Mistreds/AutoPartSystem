using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для CloseCash.xaml
    /// </summary>
    public partial class CloseCash : Window
    {
        public CloseCash(Data.OpenCloseCash closeCash)
        {
            InitializeComponent();
        }
    }
}
