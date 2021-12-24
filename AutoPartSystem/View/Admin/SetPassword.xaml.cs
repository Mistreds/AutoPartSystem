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
    /// Логика взаимодействия для SetPassword.xaml
    /// </summary>
    public partial class SetPassword : Window
    {
        private Data.Employee Employee;
        public SetPassword(Data.Employee employee)
        {
            InitializeComponent();
            Employee = employee;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee.SetPassword(Password.Password);
            ViewModel.MainViewModel.AdminModel.UpdatePassEmployee(Employee);
        }
    }
}
