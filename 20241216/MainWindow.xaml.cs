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

namespace _20241216
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            GotKeyboardFocus += MainWindow_GotKeyboardFocus;
        }



        private void MainWindow_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MyTextGotKeyFocus.Text = "GotKeyFocus = " + e.NewFocus.GetType().ToString();
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

        //失敗
        //スクロールオフセットでスクロール位置を固定はできない
        private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                var scrollWidth = MyScrollV.ActualWidth;
                var scrollHOffset = MyScrollV.HorizontalOffset;
                var scrollRight = MyScrollV.ActualWidth + MyScrollV.HorizontalOffset;
                double parentLeft = t.MyParentThumb?.MyLeft ?? 0;
                var thumbRight = parentLeft + t.MyLeft + t.ActualWidth;

                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;


                var thumbRight2 = parentLeft + t.MyLeft + t.ActualWidth;
                //固定
                if (scrollRight > thumbRight2)
                {
                    MyScrollV.ScrollToHorizontalOffset(scrollHOffset);
                }

                e.Handled = true;
            }
        }

        private void MyButtonText_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// ドラッグ移動終了時
        /// アンカーThumbをCollapsed化と再配置後に親要素の再配置
        /// </summary>
        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is KisoThumb t && t.MyParentThumb is not null)
            {
                if (t.MyParentThumb is GroupThumb gt)
                {
                    AnchorThumb anchor = gt.MyAnchorThumb;
                    anchor.Visibility = Visibility.Collapsed;
                    anchor.MyLeft = t.MyLeft;
                    anchor.MyTop = t.MyTop;
                    //イベントをここで停止
                    e.Handled = true;
                }
                t.MyParentThumb.ReLayout3();
            }

            //イベントをここで停止
            //e.Handled = true;
        }

        /// <summary>
        /// ドラッグ移動開始時
        /// アンカーThumbをHidden化、サイズと位置を移動要素に合わせる
        /// </summary>
        private void KisoThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (e.Source is KisoThumb t)
            {
                //アンカーThumbをHidden、在るけど見えないだけ
                if (t.MyParentThumb is GroupThumb gt)
                {
                    AnchorThumb anchor = gt.MyAnchorThumb;
                    anchor.Visibility = Visibility.Hidden;
                    anchor.Width = t.ActualWidth;
                    anchor.Height = t.ActualHeight;
                    anchor.MyLeft = t.MyLeft;
                    anchor.MyTop = t.MyTop;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// KeyDown時
        /// 方向キーでそれぞの方向に10ピクセル移動、各移動処理後にイベント終了
        /// </summary>
        private void KisoThumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                if (e.Key == Key.Left)
                {
                    t.MyLeft -= 10;
                    e.Handled = true;
                }
                else if (e.Key == Key.Right) { t.MyLeft += 10; e.Handled = true; }
                else if (e.Key == Key.Up) { t.MyTop -= 10; e.Handled = true; }
                else if (e.Key == Key.Down) { t.MyTop += 10; e.Handled = true; }

                //e.Handled = true;//これは各所に必要、ここで実行するとタブキーでのフォーカス移動がキャンセルされる
            }
        }


        //wpf コントロール - WPF ScrollViewer が認識されたコンテンツに自動的にスクロールするのを停止する - Stack Overflow
        //https://stackoverflow.com/questions/8384237/stop-wpf-scrollviewer-automatically-scrolling-to-perceived-content
        /// <summary>
        /// KeyUp時
        /// 再配置処理してからBringIntoViewすることで、
        /// 対象Thumbが表示される位置にスクロールする
        /// </summary>
        private void KisoThumb_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                //t.Focus();
                t.MyParentThumb?.ReLayout3();
                t.BringIntoView();

                e.Handled = true;
            }
        }
    }
}