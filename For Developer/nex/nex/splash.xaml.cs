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
using System.Windows.Shapes;

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

            System.Reflection.AssemblyTitleAttribute asmttl =
                (System.Reflection.AssemblyTitleAttribute)
                Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(),
                    typeof(System.Reflection.AssemblyTitleAttribute));

            buildNumber.Content = asmttl.Title + "\n" + "Build" + " " + ver;
        }
    }
}
