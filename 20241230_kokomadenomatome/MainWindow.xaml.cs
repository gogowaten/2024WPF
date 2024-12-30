using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20241230_kokomadenomatome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            GotKeyboardFocus += MainWindow_GotKeyboardFocus;
            GotFocus += MainWindow_GotFocus;

        }



        private void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            MyString1.Text = e.Source.GetType().ToString();
            MyString2.Text = sender.GetType().ToString();
        }

        private void MainWindow_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is KisoThumb t)
            {
                MyString.Text = $"{t.MyType}, {t.MyText}";
            }
            else
            {
                MyString.Text = e.NewFocus.GetType().ToString();
            }
        }


    }


}