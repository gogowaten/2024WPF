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

//GroupThumb
//  ItemsControl
//      Border
//          ItemsPresenter
//              ExCanvas
//                  Thumb
//                  Thumb

//ExCanvasやItemsControlのサイズが変化してもGroupThumbのサイズは変化しないので
//Bindingする必要がある
//またThumbの座標が変化したときにArrangeOverrideが動くのはExCanvasだけなので
//GroupThumbはExCanvasとBindingすることになる

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

            Array list = Enum.GetValues(typeof(KeyboardNavigationMode));
            MyComboGroup1TabNavi.ItemsSource = list;
            MyComboGroup2TabNavi.ItemsSource = list;

            GotKeyboardFocus += MainWindow_GotKeyboardFocus;
        }

        private void MainWindow_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is FrameworkElement el)
            {
                MyGotKeyFocusElementType.Text = el.GetType().Name;
            }
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

        private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
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

        private void GroupThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is Thumb t)
            {
                double xDiff = Canvas.GetLeft(t) + e.HorizontalChange;
                double yDiff = Canvas.GetTop(t) + e.VerticalChange;
                var pare = t.Parent;
                //t.UpdateLayout();//無反応
                //                t.UpdateDefaultStyle();
                //t.Arrange(new Rect(xDiff, 0, 30, 30));
                //t.Measure(new Size(30, 30));
                //t.InvalidateArrange();
                var pa = VisualTreeHelper.GetParent(t);//excanvas
                var paa = VisualTreeHelper.GetParent(pa);//itemspresenter
                var paaa = VisualTreeHelper.GetParent(paa);//border
                var paaaa = VisualTreeHelper.GetParent(paaa);//itemsconterol
                var paaaaa = VisualTreeHelper.GetParent(paaaa);//groupthumb

                if(t.Tag is GroupThumb gt)
                {
                    var neko = 0;
                }
            }
        }
    }
}