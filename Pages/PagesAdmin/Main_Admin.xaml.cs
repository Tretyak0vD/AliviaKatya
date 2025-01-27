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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Фотосалон.Classes;

namespace Фотосалон.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для Main_Admin.xaml
    /// </summary>
    public partial class Main_Admin : Page
    {
        List<Clients> clients = Clients.AllClients();
        List<Employee> employees = Employee.AllEmployee();
        List<Equipment> equipment = Equipment.AllEquipment();
        List<Materials> materials = Materials.AllMaterials();
        List<PrintShops> printShops = PrintShops.AllPrintShops();
        List<Orders> orders = Orders.AllOrders();
        List<Payments> payments = Payments.AllPayments();
        public Main_Admin()
        {
            InitializeComponent();
        }

        private void Click_Clients(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Clients item in clients)
            {
                parrent.Children.Add(new Elements.UserAdminItem(item));
            }
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_Employees(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Employee item in employees)
            {
                parrent.Children.Add(new Elements.EmployeeAdminItem(item));
            }
            parrent.Children.Add(new Elements.Add_itm(new Pages.PagesAdmin.AddEmployee()));
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_Equipment(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Equipment item in equipment)
            {
                parrent.Children.Add(new Elements.EquipmentAdminItem(item));
            }
            parrent.Children.Add(new Elements.Add_itm(new Pages.PagesAdmin.AddEquipment()));
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_Materials(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Materials item in materials)
            {
                parrent.Children.Add(new Elements.MaterialAdminItem(item));
            }
            parrent.Children.Add(new Elements.Add_itm(new Pages.PagesAdmin.AddMaterials()));
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_PrintShops(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (PrintShops item in printShops)
            {
                parrent.Children.Add(new Elements.PrintShopsAdminItem(item));
            }
            parrent.Children.Add(new Elements.Add_itm(new Pages.PagesAdmin.AddPrintShops()));
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_Orders(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Orders item in orders)
            {
                parrent.Children.Add(new Elements.OrderAdminItem(item));
            }
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }

        private void Click_Payments(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.From = 0;
            fadeInAnimation.To = 1;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);

            parrent.Children.Clear();

            foreach (Payments item in payments)
            {
                parrent.Children.Add(new Elements.PaymentsAdminItem(item));
            }
            parrent.BeginAnimation(StackPanel.OpacityProperty, fadeInAnimation);
        }
    }
}
