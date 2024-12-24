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

namespace _20241224
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            //Initialized += MainWindow_Initialized;
        }

        //private void MainWindow_Initialized(object? sender, EventArgs e)
        //{
        //    //Initializedではタイミングが早すぎて取得できない
        //    if (AdornerLayer.GetAdornerLayer(MyEllipse) is AdornerLayer layer)
        //    {
        //        layer.Add(new EzAdorner(MyEllipse));
        //    }
        //}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var これでは取得できない = AdornerLayer.GetAdornerLayer(this);
            var canv = AdornerLayer.GetAdornerLayer(MyCanvas);
            var elli = AdornerLayer.GetAdornerLayer(MyEllipse);
            var hantei = canv == elli;//true
            if (AdornerLayer.GetAdornerLayer(MyEllipse) is AdornerLayer layer)
            {
                var ador = new EzAdorner(MyEllipse);
                Rectangle waku = ador.Waku;
                //Bindingできてない
                waku.SetBinding(WidthProperty, new Binding() { Source = MyCanvas, Path = new PropertyPath(WidthProperty) });
                MyCanvas.SetBinding(HeightProperty,new Binding() { Source=waku,Path=new PropertyPath(HeightProperty) });
                layer.Add(ador);

                //layer.Add(new EzAdorner(MyEllipse));
                layer.Add(new EzAdorner(MyTextBlock));
            }

        }
    }
}