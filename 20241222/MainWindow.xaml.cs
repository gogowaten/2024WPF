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

namespace _20241222
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


        private void CustomTextThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is CustomBase cb)
            {
                cb.MyX += e.HorizontalChange;
                cb.MyY += e.VerticalChange;
            }
        }

        private void KisoThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
            }
        }

        private void Kiso3_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Kiso2Thumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;
            }
        }
    }
}