using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

//WPF、ItemsControlを使って要素を入れ子にできるカスタムコントロールThumb、グループ化みたいなもの - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2024/12/08/211056

namespace _20241207_ItemsControlCanvasPanelThumb
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                ////10ピクセル移動
                //double left = 0;
                //if (e.HorizontalChange > 10)
                //{
                //    left = 10;
                //}
                //else if (e.HorizontalChange < -10)
                //{
                //    left = -10;
                //}
                //Canvas.SetLeft(t, Canvas.GetLeft(t) + left);

                //double top = 0;
                //if (e.VerticalChange > 10)
                //{
                //    top = 10;
                //}
                //else if (e.VerticalChange < -10)
                //{
                //    top = -10;
                //}
                //Canvas.SetTop(t, Canvas.GetTop(t) + top);
                //e.Handled = true;


                //通常移動
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
                //↑ trueにしないと、重なっているThumb全てにイベントが伝播して動いてしまう
            }
        }
    }
}