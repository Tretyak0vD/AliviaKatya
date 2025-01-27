using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Фотосалон.Classes;

namespace Фотосалон.Elements
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAdminItem.xaml
    /// </summary>
    public partial class EmployeeAdminItem : UserControl
    {
        Employee employee;
        public EmployeeAdminItem(Employee _employee)
        {
            InitializeComponent();
            this.employee = _employee;

            fio_employee.Content = $"ФИО: {_employee.FIO}";
            email_employee.Content = $"Адрес эл. почты: {_employee.Email}";
            post_employee.Content = $"Должность: {_employee.Post}";
        }

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.AddEmployee(employee));
        }
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить сотрудника? При удалении сотрудника будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Employee] WHERE Id = {employee.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление сотрудника", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление сотрудника не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
