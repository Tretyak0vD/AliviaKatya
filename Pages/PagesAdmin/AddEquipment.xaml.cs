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

namespace Фотосалон.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddEquipment.xaml
    /// </summary>
    public partial class AddEquipment : Page
    {
        Equipment equipment;
        public AddEquipment(Equipment equipment = null)
        {
            InitializeComponent();
            this.equipment = equipment;

            if (equipment != null)
            {
                name_equipment.Text = equipment.EquipmentName;
                costperunit_equipment.Text = equipment.CostPerUnit.ToString();
            }
        }

        private void Click_Equipment_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_equipment.Text))
            {
                MessageBox.Show("Заполните поле с названием оборудования", "Название оборудования", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!decimal.TryParse(costperunit_equipment.Text, out decimal costperunit))
            {
                MessageBox.Show("Стоимость за единицу использования оборудования должно быть числом.", "Некорректный ввод cтоимости за единицу использования оборудования", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string query;
            using (SqlConnection connection = Classes.Connection.Open())
            {
                if (equipment == null)
                {
                    query = "INSERT INTO [dbo].[Equipment] ([EquipmentName], [CostPerUnit]) " +
                            "VALUES (@EquipmentName, @CostPerUnit);";
                }
                else
                {
                    query = "UPDATE [dbo].[Equipment] SET [EquipmentName] = @EquipmentName, [CostPerUnit] = @CostPerUnit " +
                            "WHERE Id = @Id;";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@EquipmentName", name_equipment.Text);
                    command.Parameters.AddWithValue("@CostPerUnit", costperunit);

                    if (equipment != null)
                    {
                        command.Parameters.AddWithValue("@Id", equipment.Id);
                    }

                    try
                    {
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            string message = equipment == null ? "Успешное добавление оборудования" : "Успешное изменение оборудования";
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

        private void Click_Cancel_Equipment_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
        }

        private void Click_Remove_Equipment_Redact(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить оборудование? При удалении оборудования будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Equipment] WHERE Id = {equipment.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление оборудования", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление оборудоавния не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
