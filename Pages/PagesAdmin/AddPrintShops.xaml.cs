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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Фотосалон.Classes;

namespace Фотосалон.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddPrintShops.xaml
    /// </summary>
    public partial class AddPrintShops : Page
    {
        PrintShops printShops;
        public AddPrintShops(PrintShops printShops = null)
        {
            InitializeComponent();
            this.printShops = printShops;

            if (printShops != null)
            {
                address_salon.Text = printShops.AddressSalon;
                openinghours_salon.Text = printShops.OpeningHours;
            }
        }

        private void Click_PrintShops_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(address_salon.Text))
            {
                MessageBox.Show("Заполните поле с адресом салона", "Адрес салона", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(openinghours_salon.Text))
            {
                MessageBox.Show("Заполните поле часы работы салона", "Часы работы", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string query;
            using (SqlConnection connection = Classes.Connection.Open())
            {
                if (printShops == null)
                {
                    query = "INSERT INTO [dbo].[PrintShops] ([AddressSalon], [OpeningHours]) " +
                            "VALUES (@AddressSalon, @OpeningHours);";
                }
                else
                {
                    query = "UPDATE [dbo].[PrintShops] SET [AddressSalon] = @AddressSalon, [OpeningHours] = @OpeningHours " +
                            "WHERE Id = @Id;";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@AddressSalon", address_salon.Text);
                    command.Parameters.AddWithValue("@OpeningHours", openinghours_salon.Text);

                    if (printShops != null)
                    {
                        command.Parameters.AddWithValue("@Id",printShops.Id);
                    }

                    try
                    {
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            string message = printShops == null ? "Успешное добавление салона" : "Успешное изменение салона";
                            MessageBox.Show(message, "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
                        }
                        else
                        {
                            MessageBox.Show("Запрос не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Click_Cancel_PrintShops_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
        }

        private void Click_Remove_PrintShops_Redact(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить салон печати? При удалении салона печати будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[PrintShops] WHERE Id = {printShops.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление салона печати", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
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
