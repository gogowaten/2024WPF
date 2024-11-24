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

namespace _20241124_CustomControlTextBlockThumb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddCustomControl();
        }

        private void AddCustomControl()
        {
            CustomControl1 custom = new()
            {
                MyText = "Thumbを継承して作成したのでDragDeltaイベントを\nそのまま使えて移動処理が簡単" ,
                FontSize = 20,
                Background = Brushes.Khaki
            };
            custom.DragDelta += CustomControl1_DragDelta;
            MyCanvas.Children.Add(custom);
        }

        private void CustomControl1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
            }
        }
    }
}