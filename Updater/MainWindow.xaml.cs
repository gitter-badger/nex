using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Updater
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
        public string channel;
        public string latestVer;
        public string releaseDate;
        public string description;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Internetに接続されているか調べる
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                listBox.Items.Add("インターネットに接続されていません。\r\nインターネットに接続後、再度起動してください。");
            }

            status.Text = "Checking updates";

            try
            {
                // Update 確認処理 >>>
                WebClient wc = new WebClient();

                byte[] pagedata = wc.DownloadData("https://raw.githubusercontent.com/frainworks/nex/StableChannel/update.xml");

                Encoding ec = Encoding.UTF8;

                XmlDocument xdoc = new XmlDocument();

                xdoc.LoadXml(ec.GetString(pagedata));

                XmlElement root = xdoc.DocumentElement;

                channel = root.SelectSingleNode("channel").InnerText;
                latestVer = root.SelectSingleNode("ver").InnerText;
                releaseDate = root.SelectSingleNode("releaseDate").InnerText;
                description = root.SelectSingleNode("description").InnerText;

                wc.Dispose();
            }

            catch
            {
                status.Text = "Failed updates check";
            }

            // <<< Update 確認処理


            // Update 処理 >>>
            try
            {
                WebClient wc = new WebClient();
                status.Text = "update found. starting update.";

                listBox.Items.Add("channel: " + channel);
                listBox.Items.Add("latestVer: " + latestVer);
                listBox.Items.Add("releaseDate: " + releaseDate);
                listBox.Items.Add("description: " + description);

                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFile(new Uri("https://raw.githubusercontent.com/frainworks/nex/StableChannel/nex.zip"), appPath + @"nex.zip");

                wc.Dispose();

                status.Text = "unzipping...";
                ZipArchive archive = ZipFile.OpenRead(appPath + "nex.zip");

                ZipFile.ExtractToDirectory(appPath + @"nex.zip", appPath);

                progressBar.Value = 100;
                status.Text = "update completed";
            }

            catch
            {
                status.Text = "Extract failed. plz contact to frainworks";
            }

            // <<< Update 処理


            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = appPath + @"nex.exe";
            bool result = p.Start();

            if (result)
            {
                Application.Current.Shutdown();
            }

            else
            {
                MessageBox.Show("nex.exe を起動してください");

                Application.Current.Shutdown();
            }

        }

        // DL進歩
        private void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            status.Text = "Downloading nex: " + e.ProgressPercentage.ToString() + "%";
        }
        
        // 後始末
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.Delete(appPath + @"nex.zip");
        }
    }
}
