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

namespace Фотосалон.Pages.PagesUser
{
    /// <summary>
    /// Логика взаимодействия для Main_User.xaml
    /// </summary>
    public partial class Main_User : Page
    {
        public Clients _clients;
        private Orders _order;
        public Main_User(Clients clients)
        {
            InitializeComponent();
            _clients = clients;
        }

        private void Click_Order(object sender, MouseButtonEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesUser.AddOrder(_order, _clients));
        }

        private void Click_HistoryOrders(object sender, MouseButtonEventArgs e)
        {
            parrent.Children.Clear();

            var clientsOrders = Orders.GetOrdersByClient(_clients.Id);

            foreach (Orders orders in clientsOrders)
            {
                var orderItem = new Elements.OrderUserItem(orders, _clients);
                parrent.Children.Add(orderItem);
            }
        }
        private void Click_PayOrders(object sender, MouseButtonEventArgs e)
        {
            parrent.Children.Clear();

            var clientsPayments = Payments.GetPaymentsByClient(_clients.Id);

            foreach (Payments payments in clientsPayments)
            {
                var orderItem = new Elements.PaymentsUserItem(payments, _clients);
                parrent.Children.Add(orderItem);
            }
        }

        private void Click_clientAccount(object sender, MouseButtonEventArgs e)
        {
            parrent.Children.Clear();
            parrent.Children.Add(new Elements.UserUserItem(_clients));
        }
    }
}
