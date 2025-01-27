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

namespace Фотосалон.Pages
{
	/// <summary>
	/// Логика взаимодействия для Registration_Window.xaml
	/// </summary>
	public partial class Registration_Window : Page
	{
		public Clients clients;
		public Registration_Window()
		{
			InitializeComponent();
		}
		private bool IsEmailAddressExists(string email)
		{
			using (SqlConnection connection = Classes.Connection.Open())
			{
				string query = "SELECT COUNT(*) FROM [dbo].[Clients] WHERE EmailAddress = @EmailAddress";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@EmailAddress", email);
					int count = (int)command.ExecuteScalar();
					return count > 0;
				}
			}
		}
		private void Log_in(object sender, RoutedEventArgs e)
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
            if (string.IsNullOrWhiteSpace(password_client.Password))
            {
                MessageBox.Show("Заполните поле с Паролем", "Пароль", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (fio_client.Text.Equals("admin", StringComparison.OrdinalIgnoreCase) && email_client.Text.Equals("admin") && password_client.Password.Equals("admin"))
			{
				MessageBox.Show("Запрещено использовать ФИО, Пароль и Адрес почты 'admin'. Пожалуйста, выберите другие учетные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (string.IsNullOrWhiteSpace(email_client.Text) || !Regex.IsMatch(email_client.Text, @"^[^@\s]+(\.[^@\s]+)*@[^@\s]+\.[^@\s]+(\.[^@\s]+)*$"))
			{
				MessageBox.Show("Некорректный адрес электронной почты", "Email", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}
			if (IsEmailAddressExists(email_client.Text))
			{
				MessageBox.Show("Учетная запись под такой почтой уже существует. Укажите другую почту!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}
			SqlConnection connection = Classes.Connection.Open();
			if (clients == null)
			{
				string vs = "INSERT INTO [dbo].[Clients] ([FIO],[EmailAddress],[Password]) VALUES (@FIO, @EmailAddress, @Password);";

				using (SqlCommand command = new SqlCommand(vs, connection))
				{
					command.Parameters.AddWithValue("@FIO", fio_client.Text);
					command.Parameters.AddWithValue("@EmailAddress", email_client.Text);
                    command.Parameters.AddWithValue("@Password", password_client.Password);
                    try
					{
						int result = command.ExecuteNonQuery();
						if (result > 0)
						{
							MessageBox.Show("Успешное добавление клиента", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
							MainWindow.init.OpenPage(new Pages.Authorization_Window());
						}
						else
						{
							MessageBox.Show("Запрос на добавление пользователя не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
						}
					}
					catch (SqlException sqlEx)
					{
						MessageBox.Show($"Произошла ошибка при выполнении запроса: {sqlEx.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}
		private void Back(object sender, RoutedEventArgs e)
		{
			MainWindow.init.OpenPage(new Pages.Authorization_Window());
		}
	}
}
