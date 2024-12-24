using System.Windows;
using System.Windows.Controls.Primitives;

namespace _20241224_ContentControlThumb
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