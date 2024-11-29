using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace _20241129
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DataMoto> MyData { get; set; }
        public MainWindow()
        {
            InitializeComponent();


            MyData = [];
            DataContext = this;
            MyData.Add(new DataText() { MyText = "text" });
            MyData.Add(new DataMaru() { MyFill = Brushes.YellowGreen });
            MyData.Add(new DataRect() { MyFill = Brushes.MediumAquamarine, MyWidth = 100, MyHeight = 50 });
        }
    }

}