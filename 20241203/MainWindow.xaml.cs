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

namespace _20241203
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

        private void MyButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            var sco = FocusManager.GetIsFocusScope(this);
            var csco = FocusManager.GetIsFocusScope(MyCanvas);
            var tsco = FocusManager.GetIsFocusScope(MyThumb1);
           var neko = FocusManager.IsFocusScopeProperty;
            
        }

        private void MyThumb1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) - 10);
                    e.Handled = true;
                }
            }
            
        }
    }
}