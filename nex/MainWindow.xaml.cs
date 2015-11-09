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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nex
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Loaded += MainWindow_Loaded;

			MouseLeftButtonDown += (sender, e) => DragMove();
		}

		public void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var app = Application.Current as App;
			if (app == null || app.Splash == null) return;
			app.Splash.Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown(); //終了する
		}

		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			SystemCommands.MinimizeWindow(this); //最小化する
		}
	}
}
