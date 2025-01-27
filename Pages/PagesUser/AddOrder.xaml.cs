using Microsoft.Win32;
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

namespace Фотосалон.Pages.PagesUser
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        Orders orders;
        public Clients clients;
        public AddOrder(Orders _orders, Clients _clients)
        {
            this.orders = _orders;
            clients = _clients;
            InitializeComponent();
            LoadPharmacies();
            if (_orders != null)
            {
                numbercopies_order.Text = _orders.NumberCopies.ToString();
                orderdate_order.SelectedDate = _orders.OrderDate;
                fioemployee_order.Text = _orders.FIOEmployee;
                materialname_order.Text = _orders.MaterialName;
                equipmentname_order.Text = _orders.EquipmentName;
                addresssalon_order.Text = _orders.AddressSalon;
                fullcost_order.Text = _orders.FullCost.ToString();
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

                        image_products.Source = image;
                    }
                }
            }
        }
        private void LoadPharmacies()
        {
            List<Employee> employeeList = Employee.AllEmployee();
            List<Materials> materialsList = Materials.AllMaterials();
            List<Equipment> equipmentList = Equipment.AllEquipment();
            List<PrintShops> printShopsList = PrintShops.AllPrintShops();

            fioemployee_order.Items.Clear();
            materialname_order.Items.Clear();
            equipmentname_order.Items.Clear();
            addresssalon_order.Items.Clear();

            foreach (var employee in employeeList)
            {
                fioemployee_order.Items.Add(employee.FIO);
            }
            foreach (var material in materialsList)
            {
                materialname_order.Items.Add(material.MaterialName);
            }
            foreach (var equipment in equipmentList)
            {
                equipmentname_order.Items.Add(equipment.EquipmentName);
            }
            foreach (var printShops in printShopsList)
            {
                addresssalon_order.Items.Add(printShops.AddressSalon);
            }
        }
        private void MaterialName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateTotalCost();
        }

        private void EquipmentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateTotalCost();
        }

        private void NumberCopies_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            var selectedMaterial = materialname_order.SelectedItem as string;
            var material = Materials.AllMaterials().FirstOrDefault(m => m.MaterialName == selectedMaterial);

            var selectedEquipment = equipmentname_order.SelectedItem as string;
            var equipment = Equipment.AllEquipment().FirstOrDefault(e => e.EquipmentName == selectedEquipment);

            int numberOfCopies;
            if (!int.TryParse(numbercopies_order.Text, out numberOfCopies))
            {
                numberOfCopies = 0;
            }

            decimal totalCost = 0;

            if (material != null)
            {
                totalCost += material.CostPerUnit * numberOfCopies;
            }

            if (equipment != null)
            {
                totalCost += equipment.CostPerUnit;
            }

            fullcost_order.Text = totalCost.ToString("0.00");
        }

        private void Click_load(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                image_products.Source = bitmap;
            }
        }
        private byte[] GetImageBytes(Image image)
        {
            if (image.Source is BitmapSource bitmapSource)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }

        private void Click_add(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(numbercopies_order.Text, out int quantity))
            {
                MessageBox.Show("Количество копий должно быть числом.", "Некорректный ввод количества копий", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(fullcost_order.Text, out decimal fullCost))
            {
                MessageBox.Show("Полная стоимость должна быть числом.", "Некорректный ввод стоимости", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получаем выбранный материал
            var selectedMaterialName = materialname_order.Text;
            var selectedMaterial = Materials.AllMaterials().FirstOrDefault(m => m.MaterialName == selectedMaterialName);

            // Проверяем, достаточно ли материала для выполнения заказа
            if (selectedMaterial != null && selectedMaterial.MaterialRest < quantity)
            {
                MessageBox.Show("Недостаточно материала для выполнения заказа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            byte[] imageProducts = GetImageBytes(image_products);
            string query;

            using (SqlConnection connection = Classes.Connection.Open())
            {
                int orderId = 0;

                if (orders == null)
                {
                    query = "INSERT INTO [dbo].[Orders] ([Image], [NumberCopies], [OrderDate], [IdClient], [FIOEmployee],[MaterialName],[EquipmentName],[AddressSalon],[FullCost]) " +
                            "VALUES (@Image, @NumberCopies, @OrderDate, @IdClient, @FIOEmployee, @MaterialName, @EquipmentName, @AddressSalon, @FullCost);" +
                            "SELECT SCOPE_IDENTITY();";
                }
                else
                {
                    query = "UPDATE [dbo].[Orders] SET [Image] = @Image, [NumberCopies] = @NumberCopies, [OrderDate] = @OrderDate, " +
                            "[IdClient] = @IdClient, [FIOEmployee] = @FIOEmployee, [MaterialName] = @MaterialName, [EquipmentName] = @EquipmentName, " +
                            "[AddressSalon] = @AddressSalon, [FullCost] = @FullCost " +
                            "WHERE Id = @Id;";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Image", imageProducts);
                    command.Parameters.AddWithValue("@NumberCopies", quantity);
                    command.Parameters.AddWithValue("@OrderDate", orderdate_order.SelectedDate.HasValue ? orderdate_order.SelectedDate.Value : DateTime.Now);
                    command.Parameters.AddWithValue("@IdClient", clients.Id);
                    command.Parameters.AddWithValue("@FIOEmployee", fioemployee_order.Text);
                    command.Parameters.AddWithValue("@MaterialName", materialname_order.Text);
                    command.Parameters.AddWithValue("@EquipmentName", equipmentname_order.Text);
                    command.Parameters.AddWithValue("@AddressSalon", addresssalon_order.Text);
                    command.Parameters.AddWithValue("@FullCost", fullCost);

                    if (orders != null)
                    {
                        command.Parameters.AddWithValue("@Id", orders.Id);
                    }

                    try
                    {
                        if (orders == null)
                        {
                            orderId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }

                        // Обновляем остаток материала
                        if (selectedMaterial != null)
                        {
                            selectedMaterial.MaterialRest -= quantity; // Уменьшаем остаток
                            string updateMaterialQuery = "UPDATE [dbo].[Materials] SET [MaterialRest] = @MaterialRest WHERE [Id] = @Id;";

                            using (SqlCommand updateCommand = new SqlCommand(updateMaterialQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@MaterialRest", selectedMaterial.MaterialRest);
                                updateCommand.Parameters.AddWithValue("@Id", selectedMaterial.Id);
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        string paymentQuery;
                        if (orders == null)
                        {
                            paymentQuery = "INSERT INTO [dbo].[Payments] ([IdOrder], [FullCost], [PaymentDate], [PaymentStatus], [PaymentMethod], [IdClient]) " +
                                            "VALUES (@IdOrder, @FullCost, @PaymentDate, @PaymentStatus, @PaymentMethod, @IdClient);";

                            using (SqlCommand paymentCommand = new SqlCommand(paymentQuery, connection))
                            {
                                paymentCommand.Parameters.AddWithValue("@IdOrder", orderId);
                                paymentCommand.Parameters.AddWithValue("@FullCost", fullCost);
                                paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                                paymentCommand.Parameters.AddWithValue("@PaymentStatus", "Не оплачено");
                                paymentCommand.Parameters.AddWithValue("@PaymentMethod", "Не выбран");
                                paymentCommand.Parameters.AddWithValue("@IdClient", clients.Id);

                                paymentCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            paymentQuery = "UPDATE [dbo].[Payments] SET [FullCost] = @FullCost, [PaymentDate] = @PaymentDate, " +
                                "[PaymentStatus] = @PaymentStatus, [PaymentMethod] = @PaymentMethod WHERE [IdOrder] = @IdOrder;";

                            using (SqlCommand paymentCommand = new SqlCommand(paymentQuery, connection))
                            {
                                paymentCommand.Parameters.AddWithValue("@IdOrder", orders.Id);
                                paymentCommand.Parameters.AddWithValue("@FullCost", fullCost);
                                paymentCommand.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                                paymentCommand.Parameters.AddWithValue("@PaymentStatus", "Не оплачено");
                                paymentCommand.Parameters.AddWithValue("@PaymentMethod", "Не выбран");

                                paymentCommand.ExecuteNonQuery();
                            }
                        }

                        string message = orders == null ? "Успешное формирование заказа" : "Успешное изменение заказа";
                        MessageBox.Show(message, "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow.init.OpenPage(new Pages.PagesUser.Main_User(clients));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private void Click_back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesUser.Main_User(clients));
        }
    }
}
