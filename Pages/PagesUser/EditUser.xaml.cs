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

namespace Фотосалон.Pages.PagesUser
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        public Clients clients;
        public EditUser(Clients clients = null)
        {
            InitializeComponent();
            this.clients = clients;
            if(clients != null)
            {
                fio_client.Text = clients.FIO;
                email_client.Text = clients.Email;
                password_client.Text = clients.Password;
            }
        }
        private void Click_Cancel_User_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesUser.Main_User(clients));
        }
        private void Click_User_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fio_client.Text) && !Regex.IsMatch(fio_client.Text, @"^[а-яА-ЯёЁ]+\s[а-яА-ЯёЁ]+\s[а-яА-ЯёЁ]+$"))
            {
                MessageBox.Show("Некорректный ввод ФИО пользователя", "ФИО", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(fio_client.Text))
            {
                MessageBox.Show("Заполните поле с ФИО", "ФИО", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(password_client.Text))
            {
                MessageBox.Show("Заполните поле с Паролем", "Пароль", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (fio_client.Text.Equals("admin", StringComparison.OrdinalIgnoreCase) && email_client.Text.Equals("admin") && password_client.Text.Equals("admin"))
            {
                MessageBox.Show("Запрещено использовать ФИО, Пароль и Адрес почты 'admin'. Пожалуйста, выберите другие учетные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(email_client.Text) || !Regex.IsMatch(email_client.Text, @"^[^@\s]+(\.[^@\s]+)*@[^@\s]+\.[^@\s]+(\.[^@\s]+)*$"))
            {
                MessageBox.Show("Некорректный адрес электронной почты", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (SqlConnection connection = Classes.Connection.Open())
            {
                string query = "UPDATE [dbo].[Clients] SET FIO = @FIO, EmailAddress = @EmailAddress, Password = @Password WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FIO", fio_client.Text);
                    command.Parameters.AddWithValue("@EmailAddress", email_client.Text);
                    command.Parameters.AddWithValue("@Password", password_client.Text);
                    command.Parameters.AddWithValue("@Id", clients.Id);

                    var result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Успешное изменение данных клиента", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow.init.OpenPage(new Pages.PagesUser.Main_User(clients));
                    }
                    else
                    {
                        MessageBox.Show("Запрос на изменение данных клиента не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
