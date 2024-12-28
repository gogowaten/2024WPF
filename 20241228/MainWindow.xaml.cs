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

namespace _20241228
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
                    MyVolume = 100,
                    MyBrush = Brushes.Gold
                },
                new MyData(ThumbType.Rect)
                {
                    MyLeft = 130,
                    MyTop= 140,
                    MyVolume = 100,
                    MyBrush = Brushes.DodgerBlue
                },
                new MyData(ThumbType.Group)
                {
                    MyLeft= 30,
                    MyTop= 30,

                }];

            DataContext = MyDatas;
            MyData data = new(ThumbType.Text) { MyLeft = 30, MyTop = 10, MyText = "group item 1" };
            MyDatas[3].MyDatas.Add(data);
            MyDatas[3].MyDatas.Add(new(ThumbType.Ellipse) { MyLeft = 30, MyTop = 80, MyVolume = 30, MyBrush = Brushes.Salmon });

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