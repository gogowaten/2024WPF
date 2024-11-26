using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace _20241126_CustomControlMultiple
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

        private void CustomControl1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is UIElement element)
            {
                Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
            }
        }
    }
}