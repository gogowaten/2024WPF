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
using System.Windows.Controls.Primitives;
using WpfLibrary1;

namespace WpfAppDllTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlock textBlock = new() { Text = "textblock" };
            MyThumb1.MyTemplateCanvas.Children.Add(textBlock);
            MyThumb1.DragDelta += MyThumb1_DragDelta;
        }

        private void MyThumb1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FThumb ft = (FThumb)sender;

            Canvas.SetLeft(ft, Canvas.GetLeft(ft) + e.HorizontalChange);
            Canvas.SetTop(ft, Canvas.GetTop(ft) + e.VerticalChange);
        }
    }
}