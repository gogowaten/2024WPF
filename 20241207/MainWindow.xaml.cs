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

namespace _20241207
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
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }

        private void Thumb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if(sender is Thumb t)
            //{
            //    if (e.Key == Key.Right)
            //    {
            //        Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
            //        e.Handled= true;
            //    }
            //}
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