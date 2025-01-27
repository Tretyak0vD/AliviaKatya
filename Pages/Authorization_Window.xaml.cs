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
using Фотосалон.Classes;

namespace Фотосалон.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization_Window.xaml
    /// </summary>
    public partial class Authorization_Window : Page
    {
        public Authorization_Window()
        {
            InitializeComponent();
        }
		private void Log_in(object sender, RoutedEventArgs e)
		{
			try
			{
				
				string email = email_client.Text.Trim();
                string password = password_client.Password.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
				{
					MessageBox.Show("Пожалуйста, введите Адрес эл. почты и пароль от учетной записи", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				if (email == "admin" && password == "admin")
				{
					MessageBox.Show("Вы успешно вошли как Администратор", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
					MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
					return;
				}

				Clients clients = Clients.GetClientByFIOAndEmail(email, password);
				if (clients != null)
				{
					MessageBox.Show("Вы успешно вошли как пользователь " + clients.FIO, "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
					MainWindow.init.OpenPage(new Pages.PagesUser.Main_User(clients));
				}
				else
				{
					MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Произошла ошибка: {ex.Message}");
			}
		}
		private void Back(object sender, RoutedEventArgs e)
		{
			MainWindow.init.OpenPage(new Pages.Registration_Window());
		}
	}
}
