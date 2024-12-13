using System.Windows;
using System.Windows.Controls.Primitives;

//WPF、カスタムコントロール子要素の位置変更後に、親要素の位置とサイズを変更 - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2024/12/13/222822


namespace _20241212_ReLayoutGroupThumb
{
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
            MyItem1_1.MyParentThumb?.ReLayout();
        }

        //ドラッグ移動終了時
        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            //親要素の再配置
            if (sender is KisoThumb t && t.MyParentThumb is not null)
            {
                t.MyParentThumb.ReLayout();
            }
            //イベントをここで停止
            e.Handled = true;
        }
    }
}