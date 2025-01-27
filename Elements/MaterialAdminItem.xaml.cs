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
    /// Логика взаимодействия для MaterialAdminItem.xaml
    /// </summary>
    public partial class MaterialAdminItem : UserControl
    {
        Materials material;
        public MaterialAdminItem(Materials _material)
        {
            InitializeComponent();
            this.material = _material;

            name_material.Content = $"Название Материала: { _material.MaterialName}";
            rest_material.Content = $"Остаток Материала: {_material.MaterialRest}";
            costperunit_material.Content = $"Стоимость за единицу: {_material.CostPerUnit}";
        }
        private void Click_redact(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.PagesAdmin.AddMaterials(material));
        }
        private void Click_remove(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли вы удалить материал? При удалении материала будут удалены все связанные данные", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SqlConnection connection = Classes.Connection.Open();
                string vs = $"DELETE FROM [dbo].[Materials] WHERE Id = {material.Id}";
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
