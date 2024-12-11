using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//スタイルの引き継ぎ
//[WPF/xaml]BasedOnを使って元のstyleを受け継ぐ #C# - Qiita
//https://qiita.com/tera1707/items/3c4f598c5d022e4987a2

namespace _20241211
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}