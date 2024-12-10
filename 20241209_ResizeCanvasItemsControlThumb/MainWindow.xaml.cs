using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

//WPF、自動サイズCanvasをGroupThumbに使ってみた - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2024/12/10/154802


namespace _20241209_ResizeCanvasItemsControlThumb
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
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }

        private void Thumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) - 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) - 10);
                    e.Handled = true;
                }
            }
        }

    }
}