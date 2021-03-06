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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartSystem.View.Admin
{
    /// <summary>
    /// Логика взаимодействия для MarkEdit.xaml
    /// </summary>
    public partial class MarkEdit : UserControl
    {
        Model.MarkModel.MarkModel markModel;
        public MarkEdit(Model.MarkModel.MarkModel markModel)
        {
            this.markModel = markModel;
            InitializeComponent();
        }

        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            markModel.AddBrand(Brand.Text);
            Brand.Text =String.Empty;

        }
    }
}
