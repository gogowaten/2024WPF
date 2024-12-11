using System.Windows;
using System.Windows.Controls.Primitives;

namespace _20241211_Style_Inheritance
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
            MyItem1_1.ParentThumb?.ReLayout();
        }

        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is KisoThumb t) {
                var sen = sender;
                var sou = e.Source;
                var ori = e.OriginalSource;
                if(t.ParentThumb is GroupThumb gt)
                {
                    gt.ReLayout();
                }
            }
            e.Handled = true;
        }


    }
}