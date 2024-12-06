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

namespace _20241203
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public FrameworkElement? MyItem { get; set; }



        public string MyItemType
        {
            get { return (string)GetValue(MyItemTypeProperty); }
            set { SetValue(MyItemTypeProperty, value); }
        }
        public static readonly DependencyProperty MyItemTypeProperty =
            DependencyProperty.Register(nameof(MyItemType), typeof(string), typeof(MainWindow),
                new FrameworkPropertyMetadata(string.Empty));

        public bool MyScope
        {
            get { return (bool)GetValue(MyScopeProperty); }
            set { SetValue(MyScopeProperty, value); }
        }
        public static readonly DependencyProperty MyScopeProperty =
            DependencyProperty.Register(nameof(MyScope), typeof(bool), typeof(MainWindow),
                new FrameworkPropertyMetadata(false));


        public string MyTabNaviMode
        {
            get { return (string)GetValue(MyTabNaviModeProperty); }
            set { SetValue(MyTabNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyTabNaviModeProperty =
            DependencyProperty.Register(nameof(MyTabNaviMode), typeof(string), typeof(MainWindow),
                new FrameworkPropertyMetadata(string.Empty));

        public string MyDirectionNaviMode
        {
            get { return (string)GetValue(MyDirectionNaviModeProperty); }
            set { SetValue(MyDirectionNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyDirectionNaviModeProperty =
            DependencyProperty.Register(nameof(MyDirectionNaviMode), typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        public string MyControlTabNaviMode
        {
            get { return (string)GetValue(MyControlTabNaviModeProperty); }
            set { SetValue(MyControlTabNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyControlTabNaviModeProperty =
            DependencyProperty.Register(nameof(MyControlTabNaviMode), typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public bool MyFocusable
        {
            get { return (bool)GetValue(MyFocusableProperty); }
            set { SetValue(MyFocusableProperty, value); }
        }
        public static readonly DependencyProperty MyFocusableProperty =
            DependencyProperty.Register(nameof(MyFocusable), typeof(bool), typeof(MainWindow), new PropertyMetadata(false));


        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(MainWindow), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(MainWindow), new PropertyMetadata(0.0));


        public Thumb FocusedThumb
        {
            get { return (Thumb)GetValue(FocusedThumbProperty); }
            set { SetValue(FocusedThumbProperty, value); }
        }
        public static readonly DependencyProperty FocusedThumbProperty =
            DependencyProperty.Register(nameof(FocusedThumb), typeof(Thumb), typeof(MainWindow),
                new FrameworkPropertyMetadata(null));


        public KeyboardNavigationMode MyThumbDirectionNaviMode
        {
            get { return (KeyboardNavigationMode)GetValue(MyThumbDirectionNaviModeProperty); }
            set { SetValue(MyThumbDirectionNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyThumbDirectionNaviModeProperty =
            DependencyProperty.Register(nameof(MyThumbDirectionNaviMode), typeof(KeyboardNavigationMode), typeof(MainWindow), new PropertyMetadata(KeyboardNavigationMode.Continue));


        public KeyboardNavigationMode MyThumbTbaNaviMode
        {
            get { return (KeyboardNavigationMode)GetValue(MyThumbTbaNaviModeProperty); }
            set { SetValue(MyThumbTbaNaviModeProperty, value); }
        }
        public static readonly DependencyProperty MyThumbTbaNaviModeProperty =
            DependencyProperty.Register(nameof(MyThumbTbaNaviMode), typeof(KeyboardNavigationMode), typeof(MainWindow), new PropertyMetadata(KeyboardNavigationMode.Continue));


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            PreviewMouseDown += MainWindow_PreviewMouseDown;
            MyComboThumbDirection.ItemsSource = Enum.GetValues(typeof(KeyboardNavigationMode));
            SetBinding(MyThumbDirectionNaviModeProperty, new Binding()
            {
                Source = MyComboThumbDirection,
                Path = new PropertyPath(ComboBox.SelectedValueProperty),
            });
            MyComboCanvasDirection.ItemsSource = Enum.GetValues(typeof(KeyboardNavigationMode));
            MyComboCanvasTabNavi.ItemsSource = Enum.GetValues(typeof(KeyboardNavigationMode));
        }

        private void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Thumb item)
            {
                FocusedThumb = item;
            }
            if (e.Source is FrameworkElement fe)
            {
                MyItemType = fe.GetType().Name;
                MyScope = FocusManager.GetIsFocusScope(fe);
                MyTabNaviMode = KeyboardNavigation.GetTabNavigation(fe).ToString();
                MyDirectionNaviMode = KeyboardNavigation.GetDirectionalNavigation(fe).ToString();
                MyControlTabNaviMode = KeyboardNavigation.GetControlTabNavigation(fe).ToString();
                MyFocusable = fe.Focusable;
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb item)
            {
                Canvas.SetLeft(item, Canvas.GetLeft(item) + e.HorizontalChange);
                Canvas.SetTop(item, Canvas.GetTop(item) + e.VerticalChange);
            }
        }

        //private void MyButtonCheck_Click(object sender, RoutedEventArgs e)
        //{
        //    var sco = FocusManager.GetIsFocusScope(this);
        //    var csco = FocusManager.GetIsFocusScope(MyCanvas);
        //    var tsco = FocusManager.GetIsFocusScope(MyThumb1);
        //   var neko = FocusManager.IsFocusScopeProperty;

        //}

        private void Thumb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is Thumb t)
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + 10);
                    e.Handled = true;
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

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
            }
        }
    }
}