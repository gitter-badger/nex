using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SunokoLibrary.Application;

namespace nex
{
    /// <summary>
    /// splash.xaml の相互作用ロジック
    /// </summary>
    public partial class splash : Window
    {
        public splash()
        {
            InitializeComponent();

            MouseLeftButtonDown += (sender, e) => DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //終了する
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Assemblyを取得
            System.Reflection.Assembly asm =
                System.Reflection.Assembly.GetExecutingAssembly();
            //バージョンの取得
            Version ver = asm.GetName().Version;

            //ソフトタイトルの取得
            System.Reflection.AssemblyTitleAttribute asmttl =
                (System.Reflection.AssemblyTitleAttribute)
                Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(),
                    typeof(System.Reflection.AssemblyTitleAttribute));

            softname.Text = asmttl.Title;

            buildNumber.Text = "Build" + " " + ver;

            status.Text = "Starting up...";

            status.Text = "Checking updates from repository...";
            //Update codes here.

            status.Text = "Getting cookie from Installed Browsers...";
            getCookie();

        }

        public async void getCookie()
        {
            var importableBrowsers = await CookieGetters.Default.GetInstancesAsync(true);

            var cookieGetter = importableBrowsers.First();
            var targetUrl = new Uri("http://nicovideo.jp/");
            var result = await cookieGetter.GetCookiesAsync(targetUrl);

            //通信に使う際にはCookieContainerへ取得結果を追加して使用します。
            var cookies = new CookieContainer();
            cookies.Add(result.Cookies);

            //次回起動時用の構成を保存します。
            Properties.Settings.Default.SelectedGetterInfo = cookieGetter.SourceInfo;
        }
    }
}
