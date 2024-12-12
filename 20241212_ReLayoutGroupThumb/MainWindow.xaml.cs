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

namespace _20241212_ReLayoutGroupThumb
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

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;
            }
        }

        private void MyButtonText_Click(object sender, RoutedEventArgs e)
        {
            MyItem1_1.MyLeft -= 100;
            MyItem1_1.ParentThumb?.ReLayout();
        }

        //ドラッグ移動終了時
        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            //親要素の再配置
            if (sender is KisoThumb t && t.ParentThumb is not null)
            {
                t.ParentThumb.ReLayout();
            }
            //イベント通知をここで停止
            e.Handled = true;
        }
    }
}