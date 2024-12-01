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


//Bindingテスト
//ExCanvasのLeft
//CustomThumbのMyLeft
//CustomThumbのMyDataのMyLeft

namespace _20241201_Binding
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
                e.Handled = true;//これをつけておくと親子両方にイベントをつけてもクリックした方だけしか移動しなくなる、伝播が自身で止まる
            }
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            //MyItemsThumb1.MyData.MyDatas[1].MyLeft = 120;//Ok
            //MyTextThumb2.MyLeft = 120;//ok
            MyTextThumb2.MyData.MyLeft = 120;//Ok
            //Canvas.SetLeft(MyTextThumb2, 120);//これはBindingが外れてしまう
            //ExCanvas.SetLeft(MyTextThumb2, 120);//これはBindingが外れてしまう

            if (MyItemsThumb1.MyData.MyDatas[1] is DataText data) { data.MyText += "_"; }
            MyTextThumb2.MyData.MyText += "+";
            MyTextThumb2.MyText+= "-";
        }

        private void MyButton2_Click(object sender, RoutedEventArgs e)
        {
            MyItemsThumb1.MyChildren.Remove(MyTextThumb3);
        }
    }

}