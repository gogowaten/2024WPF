using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace _20241219_拡大表示時のマウスドラッグ移動
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 拡大率指定用
        /// </summary>
        public int MyScale
        {
            get { return (int)GetValue(MyScaleProperty); }
            set { SetValue(MyScaleProperty, value); }
        }
        public static readonly DependencyProperty MyScaleProperty =
            DependencyProperty.Register(nameof(MyScale), typeof(int), typeof(MainWindow), new PropertyMetadata(1));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyComboScaleMode.ItemsSource = Enum.GetValues(typeof(BitmapScalingMode));
        }


        // Canvasを拡大表示した状態での子要素のドラッグ移動
        // 移動距離に小数点以下の数値が入るので
        // 中途半端な位置に移動した結果、表示がぼやける。
        // ぼやけ解消のためにCanvasのUseLayoutRoundingをtrueにして座標を丸めていると、
        // ブレながら移動するおかしな動きになる

        /// <summary>
        /// 通常ドラッグ移動
        /// マウスの移動距離に合わせて、そのまま移動
        /// </summary>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                MyDelta.Text = $"HorizontalChange = {e.HorizontalChange:0.00}";
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }


        // ぼやけ表示と、おかしな動きを解消 
        // UseLayoutRoundingがtrueでもブレずに移動する
        // 移動距離の小数点以下を四捨五入することで
        // 1ピクセル(ドット)単位での移動でぼやけない、ただし
        // 元の座標に小数点以下がある場合は、ドラッグ移動開始時に
        // 四捨五入しておかないとぼやけたままになる

        /// <summary>
        /// 移動距離を丸めてドラッグ移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                MyDelta.Text = $"HorizontalChange = {e.HorizontalChange:0.00}";
                double hc = e.HorizontalChange;
                if (hc > 0)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc + 0.5));
                }
                else if (hc < 0)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc - 0.5));
                }

                double vc = e.VerticalChange;
                if (vc > 0)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc + 0.5));
                }
                else if (vc < 0)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc - 0.5));
                }
                e.Handled = true;
            }
        }

        private void Button_Click_100(object sender, RoutedEventArgs e)
        {
            MyScale = 1;
        }

        private void Button_Click_1000(object sender, RoutedEventArgs e)
        {
            MyScale = 10;
        }

        private void Button_Click_300(object sender, RoutedEventArgs e)
        {
            MyScale = 3;
        }

        private void Button_Click_Rounding(object sender, RoutedEventArgs e)
        {
            MyCanvas.UseLayoutRounding = !MyCanvas.UseLayoutRounding;
        }


        private void Button_Click_EdgeMode(object sender, RoutedEventArgs e)
        {
            if (RenderOptions.GetEdgeMode(MyCanvas) == EdgeMode.Unspecified)
            {
                RenderOptions.SetEdgeMode(MyCanvas, EdgeMode.Aliased);
            }
            else
            {
                RenderOptions.SetEdgeMode(MyCanvas, EdgeMode.Unspecified);
            }
        }

        /// <summary>
        /// ドラッグ移動開始時
        /// 今の座標を四捨五入してぼやけ表示を直す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, (int)(Canvas.GetLeft(t) + 0.5));
                Canvas.SetTop(t, (int)(Canvas.GetTop(t) + 0.5));
            }
        }
    }
}