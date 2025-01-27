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
using Фотосалон.Pages.PagesUser;

namespace Фотосалон.Elements
{
    /// <summary>
    /// Логика взаимодействия для PaymentsUserItem.xaml
    /// </summary>
    public partial class PaymentsUserItem : UserControl
    {
        public Payments payments;
        public Clients clients;

        public PaymentsUserItem(Payments _payments, Clients clients)
        {
            InitializeComponent();
            this.payments = _payments;
            this.clients = clients;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            idorder_payment.Content = $"Номер заказа: {payments.IdOrder}";
            fullcost_payment.Content = $"Общая стоимость: {payments.FullCost}";
            paymentdate_payment.Content = $"Дата платежа: {payments.PaymentDate.ToShortDateString()}";
            paymentstatus_payment.Content = $"Статус оплаты: {payments.PaymentStatus}";
            paymentmethod_payment.Content = $"Способ оплаты: {payments.PaymentMethod}";

            if (payments.PaymentStatus == "Оплачено")
            {
                HidePaymentControls();
            }
        }

        private void Click_pay(object sender, RoutedEventArgs e)
        {
            if (payments.PaymentStatus != "Оплачено")
            {
                payments.PaymentStatus = "Оплачено";

                var selectedItem = addresssalon_order.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    payments.PaymentMethod = selectedItem.Content.ToString();
                }

                UpdateDisplay();

                UpdatePaymentInDatabase(payments);
            }
        }

        private void HidePaymentControls()
        {
            addresssalon_order.Visibility = Visibility.Hidden;
            buttonAdd.Visibility = Visibility.Hidden;
        }

        private void UpdatePaymentInDatabase(Payments payment)
        {
            using (SqlConnection connection = Connection.Open())
            {
                string query = "UPDATE [dbo].[Payments] SET PaymentStatus = @PaymentStatus, PaymentMethod = @PaymentMethod WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);
                command.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                command.Parameters.AddWithValue("@Id", payment.Id);

                command.ExecuteNonQuery();
            }
        }
    }
}
