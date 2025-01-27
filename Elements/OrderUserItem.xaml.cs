using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для OrderUserItem.xaml
    /// </summary>
    public partial class OrderUserItem : UserControl
    {
        public Orders orders;
        public Clients clients;
        public OrderUserItem(Orders _orders, Clients clients)
        {
            InitializeComponent();
            orders = _orders;
            this.clients = clients;

            id_order.Content = $"Номер заказа: {_orders.Id}";
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

        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesUser.AddOrder(orders,clients));
        }
    }
}
