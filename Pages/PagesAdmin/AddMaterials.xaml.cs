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
    /// Логика взаимодействия для AddMaterials.xaml
    /// </summary>
    public partial class AddMaterials : Page
    {
        Materials materials;
        public AddMaterials(Materials materials = null)
        {
            InitializeComponent();
            this.materials = materials;

            if (materials != null ) 
            {
                name_material.Text = materials.MaterialName;
                rest_material.Text = materials.MaterialRest.ToString();
                costperunit_material.Text = materials.CostPerUnit.ToString();
            }
        }

        private void Click_Material_Redact(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_material.Text))
            {
                MessageBox.Show("Заполните поле с названием материала", "Название материала", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!decimal.TryParse(costperunit_material.Text, out decimal costperunit))
            {
                MessageBox.Show("Стоимость за единицу использования материала должно быть числом.", "Некорректный ввод cтоимости за единицу использования материала", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(rest_material.Text, out int rest))
            {
                MessageBox.Show("Остаток материала должен быть числом.", "Некорректный ввод остатка материала", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string query;
            using (SqlConnection connection = Classes.Connection.Open())
            {
                if (materials == null)
                {
                    query = "INSERT INTO [dbo].[Materials] ([MaterialName], [MaterialRest], [CostPerUnit]) " +
                            "VALUES (@MaterialName, @MaterialRest, @CostPerUnit);";
                }
                else
                {
                    query = "UPDATE [dbo].[Materials] SET [MaterialName] = @MaterialName, [MaterialRest] = @MaterialRest, " +
                            "[CostPerUnit] = @CostPerUnit " +
                            "WHERE Id = @Id;";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MaterialName", name_material.Text);
                    command.Parameters.AddWithValue("@MaterialRest", rest);
                    command.Parameters.AddWithValue("@CostPerUnit", costperunit);

                    if (materials != null)
                    {
                        command.Parameters.AddWithValue("@Id", materials.Id);
                    }

                    try
                    {
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            string message = materials == null ? "Успешное добавление материала" : "Успешное изменение материала";
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

        private void Click_Cancel_Material_Redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.Main_Admin());
        }

        private void Click_Remove_Material_Redact(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить материал? При удалении материала будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Materials] WHERE Id = {materials.Id}";
                var ps = Classes.Connection.Query(vs, connection);
                if (ps != null)
                {
                    MessageBox.Show("Успешное удаление материала", "Успешное удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    (this.Parent as Panel).Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Запрос на удаление материала не был обработан", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Classes.Connection.CloseConnection(connection);
            }
        }
    }
}
