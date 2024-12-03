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
using System.Windows.Controls.Primitives;
using WpfLibrary1;

namespace WpfAppDllTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlock textBlock = new() { Text = "textblock" };
            MyThumb1.MyTemplateCanvas.Children.Add(textBlock);
            MyThumb1.DragDelta += MyThumb1_DragDelta;
            MyCustomControlTextThumb.PreviewMouseDown += MyCustomControlTextThumb_PreviewMouseDown;
            
            AddRoundButton();
            AddRoundButton2();
            AddRoundButton3();
        }

        private void MyCustomControlTextThumb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var sen = sender;
            var sour = e.Source;
            var ori = e.OriginalSource;
        }

        private void MyThumb1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FThumb ft = (FThumb)sender;

            Canvas.SetLeft(ft, Canvas.GetLeft(ft) + e.HorizontalChange);
            Canvas.SetTop(ft, Canvas.GetTop(ft) + e.VerticalChange);
        }

        /// <summary>
        /// テンプレートを改変した丸型ボタンを追加
        /// </summary>
        private void AddRoundButton()
        {
            //ControlTemplate
            FrameworkElementFactory ellipseF = new(typeof(Ellipse));
            ellipseF.SetValue(Shape.FillProperty, Brushes.Red);
            ellipseF.SetValue(Shape.StrokeProperty, Brushes.Blue);
            FrameworkElementFactory contentPresenterF = new(typeof(ContentPresenter));
            contentPresenterF.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterF.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            FrameworkElementFactory factory = new(typeof(Grid), "nemo");
            factory.AppendChild(ellipseF);
            factory.AppendChild(contentPresenterF);
            ControlTemplate ct = new() { TargetType = typeof(Button), VisualTree = factory };
            Button button = new() { Template = ct, Content = "button1" };
            MyStackPanel.Children.Add(button);

        }

        //c# - コードで ControlTemplate TemplateBinding を設定する - Stack Overflow
        //        https://stackoverflow.com/questions/50934186/set-controltemplate-templatebinding-in-code
        /// <summary>
        /// TemplateBindingをコードで
        /// </summary>
        private void AddRoundButton2()
        {
            //ControlTemplate
            FrameworkElementFactory ellipseF = new(typeof(Ellipse));
            //↓ここ
            ellipseF.SetValue(Shape.FillProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            ellipseF.SetValue(Shape.StrokeProperty, new TemplateBindingExtension(Button.ForegroundProperty));
            //↑
            //ellipseF.SetValue(Shape.FillProperty, Brushes.Red);
            //ellipseF.SetValue(Shape.StrokeProperty, Brushes.Blue);
            FrameworkElementFactory contentPresenterF = new(typeof(ContentPresenter));
            contentPresenterF.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterF.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            FrameworkElementFactory factory = new(typeof(Grid), "nemo");
            factory.AppendChild(ellipseF);
            factory.AppendChild(contentPresenterF);
            ControlTemplate ct = new() { TargetType = typeof(Button), VisualTree = factory };
            Button button = new() { Template = ct, Content = "button2" };
            MyStackPanel.Children.Add(button);
            //↓ここ
            button.Background = Brushes.Orange;
            button.Foreground = Brushes.MediumAquamarine;
        }


        //テンプレートを作成する方法 - WPF .NET | Microsoft Learn
        //        https://learn.microsoft.com/ja-jp/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-9.0

        /// <summary>
        /// できない、Triggerを使ったStyle変更
        /// </summary>
        private void AddRoundButton3()
        {
            //ControlTemplate
            FrameworkElementFactory ellipseF = new(typeof(Ellipse), "ell");
            //↓ここ
            ellipseF.SetValue(Shape.FillProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            ellipseF.SetValue(Shape.StrokeProperty, new TemplateBindingExtension(Button.ForegroundProperty));
            //↑
            FrameworkElementFactory contentPresenterF = new(typeof(ContentPresenter));
            contentPresenterF.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterF.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            FrameworkElementFactory factory = new(typeof(Grid), "nemo");
            factory.AppendChild(ellipseF);
            factory.AppendChild(contentPresenterF);
            ControlTemplate ct = new() { TargetType = typeof(Button), VisualTree = factory };
            Button button = new() { Template = ct, Content = "button3" };
            //↓ここ
            //button.Background = Brushes.Orange;
            //button.Foreground = Brushes.MediumAquamarine;
            //Trigger

            Setter setter = new() { Property = Shape.FillProperty, Value = Brushes.Magenta, TargetName = "ell" };
            Trigger trigger = new() { Property = Button.IsMouseOverProperty, Value = true };
            trigger.Setters.Add(setter);
            //ct.Triggers.Add(trigger);//error
            //Style styleCopy = new(typeof(Ellipse), button.Style);//どちらも変化なし
            Style styleCopy = new(typeof(Button), button.Style);//どちらも変化なし

            styleCopy.Triggers.Add(trigger);
            MyStackPanel.Children.Add(button);
        }


    }
}