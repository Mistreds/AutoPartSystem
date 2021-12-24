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

namespace AutoPartSystem.View.Cash
{
    /// <summary>
    /// Логика взаимодействия для InsertOutCash.xaml
    /// </summary>
    public partial class InsertOutCash : Window
    {
        private Data.Employee emp;
        private string type;
        private int cash;
        Model.Admin.AdminModel admin;
        public InsertOutCash(Data.Employee emp, string type,  Model.Admin.AdminModel admin)
        {
            this.emp = emp;
            this.type = type;
            this.admin = admin;
            InitializeComponent();
            if(type=="Добавить")
            {
                this.Title = "Добавить в кассу";
            }
            else
            {
                this.Title = "Вывод средств из кассы";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int _cash = 0;
            try
            {
                _cash = Convert.ToInt32(count.Text);
                admin.UpdateCash(_cash, name.Text, emp, type);
            }
            catch
            {
                MessageBox.Show("Неверно введена сумма", "Ошибка");
            }
        }
    }
}
