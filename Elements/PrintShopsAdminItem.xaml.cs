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

namespace Фотосалон.Elements
{
    /// <summary>
    /// Логика взаимодействия для PrintShopsAdminItem.xaml
    /// </summary>
    public partial class PrintShopsAdminItem : UserControl
    {
        PrintShops printShops;
        public PrintShopsAdminItem(PrintShops _printShops)
        {
            InitializeComponent();
            this.printShops = _printShops;

            address_salon.Content = $"Адрес салона: {_printShops.AddressSalon}";
            openinghours_salon.Content = $"Часы работы: {_printShops.OpeningHours}";
        }
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.AddPrintShops(printShops));
        }
        private void Click_remove(object sender, RoutedEventArgs e)
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