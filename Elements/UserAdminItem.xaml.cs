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
    /// Логика взаимодействия для UserAdminItem.xaml
    /// </summary>
    public partial class UserAdminItem : UserControl
    {
        Clients clients;
        public UserAdminItem(Clients _clients)
        {
            InitializeComponent();
            this.clients = _clients;

            id_clients.Content = $"Номер клиента: {_clients.Id}";
            fio_clients.Content = $"ФИО: {_clients.FIO}";
            email_clients.Content = $"Адрес эл. почты: {_clients.Email}";
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить клиента? При удалении клиента будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Clients] WHERE Id = {clients.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление клиента", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление клиента не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
