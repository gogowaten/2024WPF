using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
                //↑ trueにしないと、重なっているThumb全てにイベントが伝播して動いてしまう
            }
        }
    }
}