using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Фотосалон.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        Employee employee;
        public AddEmployee(Employee employee = null)
        {
            InitializeComponent();
            this.employee = employee;

            if (employee != null)
            {
                fio_employee.Text = employee.FIO;
                email_employee.Text = employee.Email;
                post_employee.Text = employee.Post;
            }
        }

        private void Click_Employee_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fio_employee.Text) && !Regex.IsMatch(fio_employee.Text, @"^[а-яА-ЯёЁ]+\s[а-яА-ЯёЁ]+\s[а-яА-ЯёЁ]+$"))
            {
                MessageBox.Show("Некорректный ввод ФИО сотрудника", "ФИО", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(fio_employee.Text))
            {
                MessageBox.Show("Заполните поле с ФИО сотрудника", "ФИО", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(email_employee.Text) || !Regex.IsMatch(email_employee.Text, @"^[^@\s]+(\.[^@\s]+)*@[^@\s]+\.[^@\s]+(\.[^@\s]+)*$"))
            {
                MessageBox.Show("Некорректный адрес электронной почты", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(email_employee.Text))
            {
                MessageBox.Show("Заполните поле с адресом электронной почты сотрудника", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(post_employee.Text))
            {
                MessageBox.Show("Заполните поле с должностью сотрудника", "Должность", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string query;
            using (SqlConnection connection = Classes.Connection.Open())
            {
                if (employee == null)
                {
                    query = "INSERT INTO [dbo].[Employee] ([FIO], [EmailAddress], [Post]) " +
                            "VALUES (@FIO, @EmailAddress, @Post);";
                }
                else
                {
                    query = "UPDATE [dbo].[Employee] SET [FIO] = @FIO, [EmailAddress] = @EmailAddress, " +
                            "[Post] = @Post " +
                            "WHERE Id = @Id;";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@FIO", fio_employee.Text);
                    command.Parameters.AddWithValue("@EmailAddress", email_employee.Text);
                    command.Parameters.AddWithValue("@Post", post_employee.Text);

                    if (employee != null)
                    {
                        command.Parameters.AddWithValue("@Id", employee.Id);
                    }

                    try
                    {
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            string message = employee == null ? "Успешное добавление сотрудника" : "Успешное изменение сотрудника";
                            MessageBox.Show(message, "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
                        }
                        else
                        {
                            MessageBox.Show("Запрос не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Click_Cancel_Employee_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
        }

        private void Click_Remove_Employee_Redact(object sender, RoutedEventArgs e)
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
