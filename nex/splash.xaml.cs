using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
using System.Xml;
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

            if (Environment.CommandLine.IndexOf("/up", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                try
                {
                    string[] args = Environment.GetCommandLineArgs();
                    int pid = Convert.ToInt32(args[2]);
                    Process.GetProcessById(pid).WaitForExit(); // 終了待ち
                }
                catch
                {
                    //
                }

                File.Delete(@"nex.old");
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown(); // 終了する
		}

        public MainWindow main = new MainWindow();

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";

            // Assemblyを取得
            System.Reflection.Assembly asm =
                System.Reflection.Assembly.GetExecutingAssembly();
            // バージョンの取得
            Version ver = asm.GetName().Version;
            string currentVer = ver.ToString();

            // ソフトタイトルの取得
            System.Reflection.AssemblyTitleAttribute asmttl =
                (System.Reflection.AssemblyTitleAttribute)
                Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(),
                    typeof(System.Reflection.AssemblyTitleAttribute));

            softname.Text = asmttl.Title;

            buildNumber.Text = "Build" + " " + ver;

            status.Text = "wakeing up...";

            // Internetに接続されているか調べる
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("インターネットに接続されていません。\r\nインターネットに接続後、再度起動してください。");
            }

            status.Text = "Checking updates from stableChannel...";

            // Update 確認処理 >>>
                WebClient wc = new WebClient();

                byte[] pagedata = wc.DownloadData("https://raw.githubusercontent.com/frainworks/nex/StableChannel/update.xml");

                Encoding ec = Encoding.UTF8;

                XmlDocument xdoc = new XmlDocument();

                xdoc.LoadXml(ec.GetString(pagedata));

                XmlElement root = xdoc.DocumentElement;

                var channel = root.SelectSingleNode("channel").InnerText;
                var latestVer = root.SelectSingleNode("ver").InnerText;
                var releaseDate = root.SelectSingleNode("releaseDate").InnerText;
                var description = root.SelectSingleNode("description").InnerText;

                wc.Dispose();

                Console.WriteLine("channel: " + channel);
                Console.WriteLine("latestVer: " + latestVer);
                Console.WriteLine("releaseDate: " + releaseDate);
                Console.WriteLine("description: " + description);

                if (latestVer == currentVer)
                {
                    status.Text = "no updates found.";
                }

                else
                {
                    status.Text = "update found. starting update.";

                    File.Delete(appPath + @"nex.old"); // delete old file
                    File.Move(appPath + @"nex.exe", appPath + @"nex.old"); // change own name to .old

                    wc.DownloadFile("https://raw.githubusercontent.com/frainworks/nex/StableChannel/nex.zip", appPath + @"nex.zip");
                    wc.Dispose();

                    ZipArchive archive = ZipFile.OpenRead(appPath + @"nex.zip");

                    ZipArchiveEntry entry = archive.GetEntry("nex.exe");
                    entry.ExtractToFile(entry.FullName, true);

                    // ZipFile.ExtractToDirectory(appPath + @"nex.zip", appPath);

                    Process.Start(appPath + @"nex.exe", "/up " + Process.GetCurrentProcess().Id);
                    Close();
                }


            // <<< Update 確認処理

            status.Text = "Getting cookie from Installed Browsers...";

            try {
                var importableBrowsers = await CookieGetters.Default.GetInstancesAsync(true);

                var cookieGetter = importableBrowsers.First();
                var targetUrl = new Uri("http://nicovideo.jp/");
                var result = await cookieGetter.GetCookiesAsync(targetUrl);

                // CookieContainerへ取得結果を追加
                main.cookies.Add(result.Cookies);

                // 次回起動時用の構成を保存
                Properties.Settings.Default.SelectedGetterInfo = cookieGetter.SourceInfo;
            }

            catch
            {
                status.Text = "Get cookie failed.";

                // login window show
            }

            status.Text = "All done. waking up nex";

            main.Show();
        }
	}
}
