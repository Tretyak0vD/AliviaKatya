using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Фотосалон
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static MainWindow init;
        public MainWindow()
        {
			InitializeComponent();
			init = this;
			OpenPage(new Pages.Authorization_Window());
		}
		public void OpenPage(Page page)
		{
			DoubleAnimation opgridAnmation = new DoubleAnimation();
			opgridAnmation.From = 1;
			opgridAnmation.To = 0;
			opgridAnmation.Duration = TimeSpan.FromSeconds(0.6);
			opgridAnmation.Completed += delegate
			{
				frame.Navigate(page);
				DoubleAnimation opgrisdAnmation = new DoubleAnimation();
				opgrisdAnmation.From = 0;
				opgrisdAnmation.To = 1;
				opgrisdAnmation.Duration = TimeSpan.FromSeconds(1.2);
				frame.BeginAnimation(Frame.OpacityProperty, opgrisdAnmation);
			};
			frame.BeginAnimation(Frame.OpacityProperty, opgridAnmation);
		}

		private void frame_Navigated(object sender, NavigationEventArgs e)
		{

		}
	}
}
