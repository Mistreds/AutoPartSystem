using AutoPartSystem.Model;
using Microsoft.EntityFrameworkCore;
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

namespace AutoPartSystem
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Data.ConDB();
           // var employee = db.Employees.Where(x => x.Login == this.Login.Text && x.Password == Data.Hash.SHA512(this.Password.Password)).FirstOrDefault();
            var employee = db.Employees.Include(p=>p.City).Include(p=>p.Position).Where(x => x.Login == this.Login.Text && x.IsDelete==false).FirstOrDefault();
            if (employee != null)
            {
                MainWindow main=new MainWindow(employee);
                main.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль", "Ошибка");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CheckUpdates.CheckUpdate();
        }
    }
}
