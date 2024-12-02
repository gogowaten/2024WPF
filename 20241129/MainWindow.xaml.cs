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
using System.Windows.Controls.Primitives;
//Dataの型は派生させずに単一にしたほうがいい
//  派生させるとキャストで面倒なことになる、派生元のなのか先なのかの判別がめんどくさい
//  派生先のつもりが元だったりでBindingができない元だったりする
namespace _20241129
{

    public partial class MainWindow : Window
    {
        public BaseThumb? ClickedThumb { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.PreviewMouseDown += MyCanvas_PreviewMouseDown;
        }

        private void MyCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = e.Source.ToString();
            var tt=e.OriginalSource.ToString();
           var ev = e.RoutedEvent;
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TextThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is BaseThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;//これをつけておくと親子両方にイベントをつけてもクリックした方だけしか移動しなくなる、伝播が自身で止まる、つまり付け外しをしなくても良くなる
            }
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            var neko = MyTextThumb2.MyData;
            Data inu = MyGroupThumb1.MyData;
            inu.MyDatas[1].MyLeft = 120;
            MyTextThumb2.MyText += "_";
            MyTextThumb2.MyData.MyText += "+";
            if (MyGroupThumb1.MyData.MyDatas[1] is Data data) { data.MyText += "*"; }
        }

        private void MyButton2_Click(object sender, RoutedEventArgs e)
        {
            MyGroupThumb1.MyChildren.Remove(MyTextThumb3);
        }

        private void MyButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //MyItemsThumb1.MyChildren.Add(new TextThumb() { MyText = "AddText" });
            //DataText data = new() { MyText = "AAA" };
            //TextThumb thumb = new(data);
            //var text = thumb.MyData.MyText;
            //var text2 = thumb.MyText;
            //MyItemsThumb1.MyChildren.Add(thumb);
            MyGroupThumb1.MyChildren.Add(new TextThumb(new Data() { MyText = "FromDataText", MyLeft = 50, MyTop = 50 }));
        }

        private void MyButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            var datas = MyGroupThumb1.MyData.MyDatas;
            if (MyGroupThumb1.MyChildren[2] is TextThumb thumb)
            {
                var left = thumb.MyLeft; 
                thumb.MyData.MyLeft = 50;
               var canleft=  Canvas.GetLeft(thumb);
            }
        }
    }



}