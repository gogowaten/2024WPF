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


//StyleSelector使ってみた
//DataをBindingして表示をDataの種類によって変更するのはできるけど、
//ListBoxをコンテナにした場合はドラッグ移動ができない、もしくはできたとしても難しそう
//ListBoxに追加できるのはListBoxItem型だけなのが良くない
//試しにListBoxItemのTemplateをThumbにしても、それはListBoxItemには変わりないので
//ThumbのDragDeltaイベントは使えない
//また、逆にThumbのTemplateをListBoxItemにしたら、それはThumbなので
//ListBoxには追加できないはず(試してない)
//ってことで無理、たぶんTreeViewでも同じだと思う、TreeViewに追加できるのはTreeViewItemだけ

//なので、StyleSelectorの使い道としては、複雑な動きの制御はしないで
//表示関連に絞れば良さそう、動きではクリックしたときに少し表示を変化させるとかはできそう

namespace _20241227_StyleSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MyData> MyDatas { get; set; }
        public MainWindow()
        {
            InitializeComponent();


            MyDatas = [];
            MyDatas.Add(new(DataType.Text, "text111111111") { MyLeft = 30, MyTop = 30 });
            MyDatas.Add(new(DataType.Ellipse, "text22222222") { MyLeft = 20, MyTop = 60 });
            DataContext = MyDatas;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var test = this.Resources["testkey"];
            //var neko = this.Resources;

            //var inu = this.FindResource("testkey");
            //var uma = MyGrid.Resources["MyStyle1"];//ok
            //var uma = MyDockPanel.Resources["MyStyle1"];//null
            //var uma = this.Resources["MyStyle1"];//null
            //var uma = this.FindResource("MyStyle1");//error
            //ItemsControl tako = ItemsControl.ItemsControlFromItemContainer(this);
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                var pare = VisualTreeHelper.GetParent(t);
                var sou = e.Source.GetType();
                var ori = e.OriginalSource.GetType;
                var hori = Canvas.GetLeft(t);
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyDatas[0].MyLeft += 10;
            MyDatas[1].MyTop += 10;
        }
    }


}