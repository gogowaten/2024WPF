
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

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
    /// ContentControl���x�[�X�ɂ���Thumb
    /// XAML�ł�����TextBlock�Ȃǂ�ǉ��ł���
    /// </summary>
    [ContentProperty(nameof(MyObj))]
    public class ContentThumb : Thumb
    {
        public ContentControl MyContent {  get; set; }

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

        public ContentThumb()
        {
            this.MyContent = MakeTemplate<ContentControl>();
            
            MyContent.SetBinding(ContentControl.ContentProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyObjProperty)
            });

            //MyObj = new TextBlock() { Text = "object" };
        }

        private T MakeTemplate<T>()
        {
            FrameworkElementFactory factory = new(typeof(T), "nemo");
            this.Template = new ControlTemplate() { VisualTree = factory };
            ApplyTemplate();
            return (T)this.Template.FindName("nemo", this);
        }
    }


}
