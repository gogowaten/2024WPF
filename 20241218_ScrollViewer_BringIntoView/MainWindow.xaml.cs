using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace _20241218_ScrollViewer_BringIntoView
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

        /// <summary>
        /// マウスドラッグ移動
        /// </summary>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 方向キーの方向へ10ピクセル移動
        /// </summary>
        private void Thumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) - 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) - 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + 10);
                    e.Handled = true;
                }

            }
        }

        /// <summary>
        /// キーが離されたとき
        /// BringIntoViewを実行することで、Thumbが見える位置までスクロールする
        /// </summary>
        private void Thumb_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                t.BringIntoView();
            }
        }

        /// <summary>
        /// マウスボタン押したとき、
        /// フォーカスを無効化することで、
        /// Thumbの表示位置(スクロール位置)と
        /// マウスカーソルの位置に違和感がなくなり、ドラッグ移動動作も自然になる
        /// </summary>
        private void Thumb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Thumb t)
            {
                t.Focusable = false;
            }
        }

        /// <summary>
        /// マウスボタンを離したとき
        /// フォーカスを有効化してからフォーカスする
        /// </summary>
        private void Thumb_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Thumb t)
            {
                t.Focusable = true;
                t.Focus();
            }
        }
    }


}