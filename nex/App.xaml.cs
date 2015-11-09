using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace nex
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		private splash _splash;
		public splash Splash { get; private set; }

		public App() : base()
		{
			Startup += Application_Startup;
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Splash = new splash();
			Splash.Show();
		}
	}
}
