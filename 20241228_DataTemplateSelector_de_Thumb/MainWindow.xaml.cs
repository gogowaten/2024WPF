using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace _20241228_DataTemplateSelector_de_Thumb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MyData> MyDatas { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            MyDatas = [
                new MyData(ThumbType.Text)
                {
                    MyLeft = 30,
                    MyTop = 10,
                    MyText = "textblock thumb"
                },
                new MyData(ThumbType.Ellipse)
                {
                    MyLeft = 30,
                    MyTop = 80,
                    MyVolume = 100
                },
                new MyData(ThumbType.Rect)
                {
                    MyLeft = 130,
                    MyTop= 140,
                    MyVolume = 100
                }];

            DataContext = MyDatas;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyDatas[0].MyText += "👍️";
            MyDatas[1].MyLeft += 10;
            MyDatas[2].MyTop += 10;
        }
    }
}