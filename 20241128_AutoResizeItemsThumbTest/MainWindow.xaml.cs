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


//AutoResizeCanvasを使ったThumbのテスト
//スクロールバー表示しているときに外側のThumbを移動すると一気に縮小する動作がいまいち
//できればドラッグ移動終了時にリサイズできるようにしたい
namespace _20241128_AutoResizeItemsThumbTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddThumbs(200);
        }

        private void AddThumbs(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CustomTextThumb thumb = new() { MyText = "text", MyX = i * 10, MyY = i * 10 };
                thumb.DragDelta += CustomTextThumb_DragDelta;
                MyItemsThumb.MyItems.Add(thumb);
            }
        }

        private void CustomTextThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is CustomBase cb)
            {
                cb.MyX += e.HorizontalChange;
                cb.MyY += e.VerticalChange;
            }
        }
    }
}