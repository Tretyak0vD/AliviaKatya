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

namespace Фотосалон.Elements
{
    /// <summary>
    /// Логика взаимодействия для UserUserItem.xaml
    /// </summary>
    public partial class UserUserItem : UserControl
    {
        Clients clients;
        public UserUserItem(Clients _clients)
        {
            InitializeComponent();
            this.clients = _clients;
            fio_client.Content = "ФИО: " + _clients.FIO;
            email_client.Content = "Адрес эл. почты: " + _clients.Email;
            password_client.Content = "Пароль: " + _clients.Password;
        }
        public void Click_exitAccount(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Authorization_Window());
        }
        public void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesUser.EditUser(clients));
        }
    }
}
