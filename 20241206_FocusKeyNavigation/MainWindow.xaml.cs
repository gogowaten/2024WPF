using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

//WPF、フォーカスのあるThumbを方向キーで移動(方向キーでフォーカスを移動させない) - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2024/12/07/142426


//目的はカーソルキーによるThumbの移動とTabキーによるフォーカス移動

//状態
//  Thumbは複数あり、すべてCanvas内にある
//  Canvasの周りにはその他のコントロールがある、Menu、DockPanel、StatusBar

//Thumbにフォーカスがあるときに
//  Tabキーを押したとき
//      Canvas内にあるThumb群でフォーカスがループ
//      Thumb以外にはフォーカスが移らない
//  カーソルキーを押したとき
//      フォーカスのあるThumbだけが、それぞれの方向へ移動

//必要な設定
//CanvasのKeyboardNavigation.TabNavigationをKeyboardNavigationMode.Cycleにする
//  これにより、TabキーでCanvas内のThumb群でのフォーカスループになる
//ThumbのDragDeltaイベントで移動処理をした後にe.Handled = true;
//  これにより、カーソルキーでのフォーカス移動を防げる

//関係ありそうでなかったこと、できなかったこと
//各コントロールのKeyboardNavigation.DirectionalNavigationの値を変更
//CanvasやThumbの設定を変更したけど、カーソルキーでの動作は期待通りにはならなかった


namespace _20241206_FocusKeyNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public UIElement MyFocusedElement
        {
            get { return (UIElement)GetValue(MyFocusedElementProperty); }
            set { SetValue(MyFocusedElementProperty, value); }
        }
        public static readonly DependencyProperty MyFocusedElementProperty =
            DependencyProperty.Register(nameof(MyFocusedElement), typeof(UIElement), typeof(MainWindow), new PropertyMetadata(null));




        public KeyboardNavigationMode MyThumbDirectionNaviMode
        {
            get { return (KeyboardNavigationMode)GetValue(MyThumbDirectionNaviModeProperty); }
            set { SetValue(MyThumbDirectionNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyThumbDirectionNaviModeProperty =
            DependencyProperty.Register(nameof(MyThumbDirectionNaviMode), typeof(KeyboardNavigationMode), typeof(MainWindow), new PropertyMetadata(KeyboardNavigationMode.Continue));


        public MainWindow()
        {
            InitializeComponent();


            DataContext = this;
            SetBinding(MyThumbDirectionNaviModeProperty, new Binding()
            {
                Source = MyComboThumbDirection,
                Path = new PropertyPath(ComboBox.SelectedValueProperty),
            });

            var items = Enum.GetValues(typeof(KeyboardNavigationMode));
            MyComboThumbDirection.ItemsSource = items;
            MyComboCanvasDirection.ItemsSource = items;
            MyComboCanvasTabNavi.ItemsSource = items;

            GotFocus += MainWindow_GotFocus;
            GotKeyboardFocus += MainWindow_GotKeyboardFocus;
            PreviewGotKeyboardFocus += MainWindow_PreviewGotKeyboardFocus;

        }

        private void MainWindow_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MyPreKeyFocus.Text = "PreviewGotKeyboardFocus = " + e.NewFocus.GetType().Name;
            if (e.NewFocus is UIElement el) MyFocusedElement = el;
        }

        private void MainWindow_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MyKeyFocus.Text = "GotKeyboardFocus = " + e.NewFocus.GetType().Name;
        }

        private void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            MyFocus.Text = "GotFocus = " + sender.GetType().Name;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb item)
            {
                Canvas.SetLeft(item, Canvas.GetLeft(item) + e.HorizontalChange);
                Canvas.SetTop(item, Canvas.GetTop(item) + e.VerticalChange);
            }
        }


        //private void Thumb_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (sender is Thumb t)
        //    {
        //        if (e.Key == Key.Right)
        //        {
        //            Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
        //        }
        //        else if (e.Key == Key.Left)
        //        {
        //            Canvas.SetLeft(t, Canvas.GetLeft(t) - 10);
        //        }
        //        else if (e.Key == Key.Up)
        //        {
        //            Canvas.SetTop(t, Canvas.GetTop(t) - 10);
        //        }
        //        else if (e.Key == Key.Down)
        //        {
        //            Canvas.SetTop(t, Canvas.GetTop(t) + 10);
        //        }
        //    }
        //}

        private void Thumb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
                    e.Handled = true;//重要
                }
                else if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) - 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) - 10);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + 10);
                    e.Handled = true;
                }
            }
        }



    }
}