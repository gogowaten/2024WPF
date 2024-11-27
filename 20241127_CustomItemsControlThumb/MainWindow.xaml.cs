using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace _20241127_CustomItemsControlThumb
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

        private void CustomItemsThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is UIElement ue)
            {
                Canvas.SetLeft(ue, Canvas.GetLeft(ue) + e.HorizontalChange);
                Canvas.SetTop(ue, Canvas.GetTop(ue) + e.VerticalChange);
            }
        }
    }
}