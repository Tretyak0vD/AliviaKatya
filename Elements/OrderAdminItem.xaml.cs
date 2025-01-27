using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для OrderAdminItem.xaml
    /// </summary>
    public partial class OrderAdminItem : UserControl
    {
        Orders orders;
        public OrderAdminItem(Orders _orders)
        {
            InitializeComponent();
            this.orders = _orders;

            id_order.Content = $"Номер заказа: {_orders.Id}";
            idclient_order.Content = $"Номер клиента: {_orders.IdClient}";
            numbercopies_order.Content = $"Количество копий: {_orders.NumberCopies}";
            orderdate_order.Content = $"Дата заказа: {_orders.OrderDate.ToShortDateString()}";
            fioemployee_order.Content = $"ФИО сотрудника: {_orders.FIOEmployee}";
            materialname_order.Content = $"Название материала: {_orders.MaterialName}";
            equipmentname_order.Content = $"Название оборудования: {_orders.EquipmentName}";
            addresssalon_order.Content = $"Адрес салона: {_orders.AddressSalon}";
            fullcost_order.Content = $"Общая стоимость: {_orders.FullCost}";
            if (orders.Image != null)
            {
                using (var stream = new MemoryStream(orders.Image))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();

                    products_image.Source = image;
                }
            }
        }

        private void Click_del(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить историю заказа этого пользователя? При удалении истории заказа будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Orders] WHERE Id = {orders.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление истории заказа", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление истории заказа не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
