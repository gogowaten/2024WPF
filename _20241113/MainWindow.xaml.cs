using System.Diagnostics;
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
using WpfLibrary1;
using System.Windows.Controls.Primitives;

namespace _20241113
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //イベントの登録（subscribe/購読）
            //MyButtonTest1.Click += MyButtonTest1_Click;
            //左側がイベントで、右側がハンドラーのオブジェクト

            //匿名
            //MyButtonTest1.Click += (sender, e) => { MessageBox.Show("ok"); };

        //以下の3行は同じ効果
        //MyButtonTest1.Click += new RoutedEventHandler(MyButtonTest1_Click);
        //MyButtonTest1.Click += MyButtonTest1_Click;//普通はこれを使う
        //MyButtonTest1.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(MyButtonTest1_Click));

        //MyStackPanel1.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(MyButtonTest1_Click));
        MyStackPanel1.AddHandler(MouseDownEvent, new RoutedEventHandler(MyButtonTest1_Click));
            //MyStackPanel1.MouseDown += MyStackPanel1_MouseDown;

            MyFThumb.MyTemplateCanvas.Children.Add(new TextBlock() { Text = "flatThumb" });
            MyFThumb.DragDelta += MyFThumb_DragDelta;
        }

        private void MyFThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FThumb thumb = (FThumb)sender;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void MyCustomThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb t = (Thumb)sender;
            Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
            Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
        }

        private void MyStackPanel1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("stackpanel");
        }

        //コードを使用してイベント ハンドラーを追加する方法 - WPF.NET | Microsoft Learn
        //https://learn.microsoft.com/ja-jp/dotnet/desktop/wpf/events/how-to-add-an-event-handler-using-code?view=netdesktop-9.0

        private void MyButtonTest1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(sender.ToString());
            string sourceName = ((FrameworkElement)e.Source).Name;
            string senderName = ((FrameworkElement)sender).Name;
            Debug.WriteLine($"RoutedEvent {sourceName}");
            Debug.WriteLine(senderName);
            //_ = MessageBox.Show($"RoutedEvent {sourceName}");
            //var tt = e.Source.GetType().Name;
            _ = MessageBox.Show($"RoutedEventのSourceのTypeは、{e.Source.GetType().Name} \n" +
                $"イベントの起点は{sender.GetType().Name}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var top = Canvas.GetTop(MyText22);
            Canvas.SetTop(MyText22, top + 1);

            SleepClass sleepClass = new();
            sleepClass.Sleep += new EventHandler(SleepTime);
            sleepClass.Sleep += this.SleepTime;
            sleepClass.Start();
        }
        private void SleepTime(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Handler_ConditionalClick(object sender, RoutedEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            MessageBox.Show($"sender = {senderName}, source = {sourceName}");
        }
    }

    public class ExCanvas : Canvas
    {
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var size = Test2(arrangeSize);
            return base.ArrangeOverride(arrangeSize);


        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
            //return GetChildrenSize();
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);
        }

        public void Test()
        {
            var mySize = GetChildrenSize();
            //Width = mySize.Width;
            //Height = mySize.Height;
            //var size = MeasureCore(mySize);
            //var size = MeasureOverride(mySize);
            //var size = ArrangeOverride(mySize);

        }

        public Size Test2(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }

                double x = 0;
                double y = 0;


                //Compute offset of the child:
                //If Left is specified, then Right is ignored
                //If Left is not specified, then Right is used
                //If both are not there, then 0
                double left = GetLeft(child);
                if (!Double.IsNaN(left))
                {
                    x = left;
                }
                else
                {
                    double right = GetRight(child);

                    if (!Double.IsNaN(right))
                    {
                        x = arrangeSize.Width - child.DesiredSize.Width - right;
                    }
                }

                double top = GetTop(child);
                if (!Double.IsNaN(top))
                {
                    y = top;
                }
                else
                {
                    double bottom = GetBottom(child);

                    if (!Double.IsNaN(bottom))
                    {
                        y = arrangeSize.Height - child.DesiredSize.Height - bottom;
                    }
                }

                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
            }
            return arrangeSize;
        }

        public Size GetChildrenSize()
        {
            double right = 0; double bottom = 0;

            foreach (UIElement child in Children)
            {
                var cleft = Canvas.GetLeft(child);
                var ctop = Canvas.GetTop(child);
                var rs = child.RenderSize;
                //var ds = child.DesiredSize;
                if (rs.Width + cleft > right) { right = rs.Width + cleft; }
                if (rs.Height + ctop > bottom) { bottom = rs.Height + ctop; }
            }
            return new Size(right, bottom);
        }
        protected override void ParentLayoutInvalidated(UIElement child)
        {
            base.ParentLayoutInvalidated(child);
        }
    }
}