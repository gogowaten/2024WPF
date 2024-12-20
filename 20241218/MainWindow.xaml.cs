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

namespace _20241218
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

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var send = (StackPanel)sender;
            var layer = AdornerLayer.GetAdornerLayer(send);
            foreach (var item in layer.GetAdorners(send))
            {
                layer.Remove(item);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var send = (StackPanel)sender;
            AdornerLayer.GetAdornerLayer(send).Add(new FocusAdorner(send));
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var send = (StackPanel)sender;
            Keyboard.Focus(send);
        }








        //private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        //{
        //    if (sender is Thumb t)
        //    {
        //        Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
        //        Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
        //        e.Handled = true;
        //    }
        //}

        //private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
        //{
        //    if (sender is Thumb t)
        //    {
        //        double hc = e.HorizontalChange;

        //        if (hc > 0)
        //        {
        //            Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc + 0.5));
        //        }
        //        else if (hc < 0)
        //        {
        //            Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc - 0.5));
        //        }

        //        double vc = e.VerticalChange;
        //        if (vc > 0)
        //        {
        //            Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc + 0.5));
        //        }
        //        else if (vc < 0)
        //        {
        //            Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc - 0.5));
        //        }
        //        e.Handled = true;
        //    }
        //    //if (sender is Thumb t)
        //    //{
        //    //    double hc = e.HorizontalChange;

        //    //    if (hc > 0)
        //    //    {
        //    //        //Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc + 0.5));
        //    //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale + 0.5));
        //    //    }
        //    //    else if (hc < 0)
        //    //    {
        //    //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale - 0.5));
        //    //    }

        //    //    double vc = e.VerticalChange;
        //    //    if (vc > 0)
        //    //    {
        //    //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale + 0.5));
        //    //    }
        //    //    else if (vc < 0)
        //    //    {
        //    //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale - 0.5));
        //    //    }
        //    //    e.Handled = true;
        //    //}

        //    //if (sender is Thumb t)
        //    //{
        //    //    double hc = e.HorizontalChange;

        //    //    if (hc > 0)
        //    //    {
        //    //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale + 0.5) * MyScale);
        //    //    }
        //    //    else if (hc < 0)
        //    //    {
        //    //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale - 0.5) * MyScale);
        //    //    }

        //    //    double vc = e.VerticalChange;
        //    //    if (vc > 0)
        //    //    {
        //    //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale + 0.5) * MyScale);
        //    //    }
        //    //    else if (vc < 0)
        //    //    {
        //    //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale - 0.5) * MyScale);
        //    //    }
        //    //    e.Handled = true;
        //    //}
        //}



    }
}