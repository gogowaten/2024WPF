using System.Collections.ObjectModel;
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


//Canvasコントロールの子要素を動的に増減させたい
//https://teratail.com/questions/359699



namespace _20241123
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Item> Items { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Items =
            [
                new PathItem {
                    X = 50, Y = 50, Width = 50, Height = 50, Stroke = Brushes.Orange, Data = "M0,25L25,50L50,25L25,0Z" },
                new EllipseItem { X = 200, Y = 50, Width = 100, Height = 100, Fill = Brushes.MediumAquamarine },
                new RectangleItem() { X = 200, Y = 200, Width = 100, Height = 50, Fill = Brushes.Blue },
                new RichTextBoxItem { X = 400, Y = 50, Width = 300, Height = 300, Text = "richText" },
            ];
            
        }
        
    }

}
