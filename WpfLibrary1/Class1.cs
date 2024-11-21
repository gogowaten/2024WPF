
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfLibrary1
{
    public class Class1
    {
    }
    public class FThumb : Thumb
    {
        public Canvas MyTemplateCanvas { get; private set; }
        public UIElementCollection MyElements { get; private set; }

        public FThumb()
        {
            MyTemplateCanvas = MakeTemplate<Canvas>();
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            MyTemplateCanvas.Background = Brushes.AliceBlue;

            MyElements = MyTemplateCanvas.Children;
        }

        private T MakeTemplate<T>()
        {
            FrameworkElementFactory factory = new(typeof(T), "nemo");
            this.Template = new ControlTemplate() { VisualTree = factory };
            ApplyTemplate();
            return (T)this.Template.FindName("nemo", this);
        }
    }

    /// <summary>
    /// ContentControlをベースにしたThumb
    /// XAMLでも直接TextBlockなどを追加できる
    /// </summary>
    [ContentProperty(nameof(MyObj))]
    public class ContentThumb : Thumb
    {

        public object MyObj
        {
            get { return (object)GetValue(MyObjProperty); }
            set { SetValue(MyObjProperty, value); }
        }
        public static readonly DependencyProperty MyObjProperty =
            DependencyProperty.Register(nameof(MyObj), typeof(object), typeof(ContentThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ContentControl MyContentControl { get; set; }
        public ContentThumb()
        {
            this.MyContentControl = MakeTemplate<ContentControl>();

            MyContentControl.SetBinding(ContentControl.ContentProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyObjProperty)
            });
        }

        private T MakeTemplate<T>()
        {
            FrameworkElementFactory factory = new(typeof(T), "nemo");
            this.Template = new ControlTemplate() { VisualTree = factory };
            ApplyTemplate();
            return (T)this.Template.FindName("nemo", this);
        }
    }

    public class TTextBlock : ContentThumb
    {

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(TTextBlock),
                new FrameworkPropertyMetadata("",
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TTextBlock()
        {
            TextBlock tb = new();
            tb.SetBinding(TextBlock.TextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyTextProperty)
            });
            MyObj = tb;
        }

    }

    public class TTextBlock2 : Thumb
    {
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(TTextBlock2),
                new FrameworkPropertyMetadata("",
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TTextBlock2()
        {

            TextBlock t = MakeTemplate<TextBlock>();
            t.SetBinding(TextBlock.TextProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyTextProperty)
            });
        }

        private T MakeTemplate<T>()
        {
            FrameworkElementFactory factory = new(typeof(T), "nemo");
            this.Template = new ControlTemplate() { VisualTree = factory };
            ApplyTemplate();
            return (T)this.Template.FindName("nemo", this);
        }
    }


    public class TRectAngle : ContentThumb
    {

        public double MyWidth
        {
            get { return (double)GetValue(MyWidthProperty); }
            set { SetValue(MyWidthProperty, value); }
        }
        public static readonly DependencyProperty MyWidthProperty =
            DependencyProperty.Register(nameof(MyWidth), typeof(double), typeof(TRectAngle),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyHeight
        {
            get { return (double)GetValue(MyHeightProperty); }
            set { SetValue(MyHeightProperty, value); }
        }
        public static readonly DependencyProperty MyHeightProperty =
            DependencyProperty.Register(nameof(MyHeight), typeof(double), typeof(TRectAngle),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }


    //.net - ContentControl と ContentPresenter の違いは何ですか? - Stack Overflow
    //    https://stackoverflow.com/questions/1287995/whats-the-difference-between-contentcontrol-and-contentpresenter

    /// <summary>
    /// ContentPresenterを使ったThumb、ContentControlよりこちらのほうが軽量
    /// </summary>
    [ContentProperty(nameof(MyObj))]
    public class ContentPThumb : Thumb
    {

        public object MyObj
        {
            get { return (object)GetValue(MyObjProperty); }
            set { SetValue(MyObjProperty, value); }
        }
        public static readonly DependencyProperty MyObjProperty =
            DependencyProperty.Register(nameof(MyObj), typeof(object), typeof(ContentPThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ContentPresenter MyContentPresenter { get; set; }
        public ContentPThumb()
        {
            this.MyContentPresenter = MakeTemplate<ContentPresenter>();

            MyContentPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyObjProperty)
            });
        }

        private T MakeTemplate<T>()
        {
            FrameworkElementFactory factory = new(typeof(T), "nemo");
            this.Template = new ControlTemplate() { VisualTree = factory };
            ApplyTemplate();
            return (T)this.Template.FindName("nemo", this);
        }
    }

    [ContentProperty(nameof(MyObj))]
    public class ContentPThumb2 : ContentPThumb
    {
        public ContentPThumb2()
        {
            Trigger trigger = new();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;
            Setter setter = new();
            setter.Property = Shape.FillProperty;
            setter.Value = Brushes.Magenta;
            setter.TargetName = "nemo";
            this.Template.Triggers.Add(trigger);
        }


    }

    [ContentProperty(nameof(MyObj))]
    public class ContentPThumb3 : Thumb
    {

        public object MyObj
        {
            get { return (object)GetValue(MyObjProperty); }
            set { SetValue(MyObjProperty, value); }
        }
        public static readonly DependencyProperty MyObjProperty =
            DependencyProperty.Register(nameof(MyObj), typeof(object), typeof(ContentPThumb3),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ContentPresenter MyContentPresenter { get; set; }
        public ContentPThumb3()
        {
            this.MyContentPresenter = MakeTemplate<ContentPresenter>();

            MyContentPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyObjProperty)
            });

        }



        private T MakeTemplate<T>()
        {
            Trigger trigger = new() { Property = Thumb.IsMouseOverProperty, Value = true };
            Setter setter = new() { Property = Thumb.BackgroundProperty, Value = Brushes.Magenta, TargetName = "nemo" };
            trigger.Setters.Add(setter);
            Style styleCopy = new(typeof(ContentPThumb3), this.Style);
            styleCopy.Triggers.Add(trigger);

            FrameworkElementFactory factory = new(typeof(T), "nemo");
            ControlTemplate ct = new();
            ct.VisualTree = factory;
            ct.Triggers.Add(trigger);
            this.Template = ct;
            ApplyTemplate();
            
            return (T)this.Template.FindName("nemo", this);
        }
    }


}
