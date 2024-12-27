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

namespace _20241225
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MyDD> myDDs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = MyData.Items;

            myDDs = [new MyDD(ThumbType.Text) { MyLeft = 20, MyTop = 10 }, new MyDD(ThumbType.Ellipse) { MyLeft = 30, MyTop = 80 }];
            myDDs[0].MyText = "iii";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myDDs[0].MyText = "aaa";
            myDDs[1].MyLeft = 100;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                var hori = Canvas.GetLeft(t);
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
            }
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

    public class MySelector2 : StyleSelector
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

    public class MyDTSelector : DataTemplateSelector
    {
        public DataTemplate? DT1 { get; set; }
        public DataTemplate? DT2 { get; set; }
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {

            if (item is not MyDD)
            {
                return base.SelectTemplate(item, container);
            }
            else if (item is MyDD dd)
            {
                if (dd.Type == ThumbType.Text) { return DT1; }
                else if (dd.Type == ThumbType.Ellipse) { return DT2; }
                else { return base.SelectTemplate(item, container); }
            }
            return base.SelectTemplate(item, container);
        }
    }

    public enum ThumbType { None = 0, Text, Ellipse, }
    public class MyDD : DependencyObject
    {
        public ThumbType Type { get; }

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(MyDD), new PropertyMetadata(string.Empty));


        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(MyDD), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(MyDD), new PropertyMetadata(0.0));


        public MyDD(ThumbType type)
        {
            if (type == ThumbType.None) { Type = ThumbType.None; }
            else if (type == ThumbType.Text) { Type = ThumbType.Text; }
            else if (type == ThumbType.Ellipse) { Type = ThumbType.Ellipse; }

        }
    }

    public class MyClass : System.Windows.Controls.Primitives.Selector
    {

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