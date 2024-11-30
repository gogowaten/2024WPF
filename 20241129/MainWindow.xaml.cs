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

namespace _20241129
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
            DataMoto inu = MyItemsThumb1.MyData;
            var tako = (Datas)inu;
            tako.MyDatas[0].MyTop = 100;
        }

    }



}