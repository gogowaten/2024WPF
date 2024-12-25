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

namespace _20241225
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = MyData.Items;

            List<MyDD> myDDs = new() { new MyDD(ThumbType.Text), new MyDD(ThumbType.Ellipse) };
            DataContext = myDDs;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var test = this.Resources["testkey"];
            var neko = this.Resources;

            var inu = this.FindResource("testkey");
            //var uma = MyGrid.Resources["MyStyle1"];//ok
            //var uma = MyDockPanel.Resources["MyStyle1"];//null
            //var uma = this.Resources["MyStyle1"];//null
            //var uma = this.FindResource("MyStyle1");//error
            ItemsControl tako = ItemsControl.ItemsControlFromItemContainer(this);
        }
    }

    public class MySelector : StyleSelector
    {
        public Style Style1 { get; set; }
        public Style Style2 { get; set; }

       
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is not MyDD dd)
            {
                return base.SelectStyle(item, container);
            }

            return dd.Type switch
            {
                ThumbType.Text => Style1,
                ThumbType.Ellipse => Style2,
                _ => base.SelectStyle(item, container)
            };
        }
    }

    public enum ThumbType { None = 0, Text, Ellipse, }
    public class MyDD
    {
        public ThumbType Type { get; }
        public MyDD(ThumbType type)
        {
            if (type == ThumbType.None) { Type = ThumbType.None; }
            else if (type == ThumbType.Text) { Type = ThumbType.Text; }
            else if (type == ThumbType.Ellipse) { Type = ThumbType.Ellipse; }

        }
    }

    class MyItemContaierStyleSelector : StyleSelector
    {
        public Style Style1 { get; set; }
        public Style Style2 { get; set; }
        public Style Style3 { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is not MyData myData)
            {
                return base.SelectStyle(item, container);
            }

            return myData.Type switch
            {
                MyData.ItemType.Bool => Style1,
                MyData.ItemType.Int => Style2,
                MyData.ItemType.String => Style3,
                _ => base.SelectStyle(item, container),
            };
        }
    }

    class MyData
    {
        public enum ItemType { Bool, Int, String };
        public ItemType Type { get; }

        public bool BoolValue { get; set; }
        public int IntValue { get; set; }
        public string StringValue { get; set; } = string.Empty;

        private MyData(bool b)
        {
            Type = ItemType.Bool;
            BoolValue = b;
        }
        private MyData(int i)
        {
            Type = ItemType.Int;
            IntValue = i;
        }
        private MyData(string s)
        {
            Type = ItemType.String;
            StringValue = s ?? "";
        }

        // ◆下記、雑多な並びのリストがグルーピングされて表示される
        public static List<MyData> Items => [
                new MyData(false),
                new MyData(true),
                new MyData(123),
                new MyData("Hello"),
                new MyData(null),
                new MyData(4321),
                new MyData("こんにちわ"),
                new MyData(false),
                new MyData(0),
            ];

    }
}