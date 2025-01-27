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
    /// Логика взаимодействия для PaymentsAdminItem.xaml
    /// </summary>
    public partial class PaymentsAdminItem : UserControl
    {
        public Payments payments;
        public PaymentsAdminItem(Payments _payments)
        {
            InitializeComponent();
            this.payments = _payments;

            idorder_payment.Content = $"Номер заказа: {payments.IdOrder}";
            fullcost_payment.Content = $"Общая стоимость: {payments.FullCost}";
            paymentdate_payment.Content = $"Дата платежа: {payments.PaymentDate.ToShortDateString()}";
            paymentstatus_payment.Content = $"Статус оплаты: {payments.PaymentStatus}";
            paymentmethod_payment.Content = $"Способ оплаты: {payments.PaymentMethod}";
            idclient_payment.Content = $"Номер клиента: {payments.IdClient}";
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы информацию об оплате этого пользователя? При удалении информацию об оплате этого пользователя будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Payments] WHERE Id = {payments.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление информацию об оплате этого пользователя", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление салона печати не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
