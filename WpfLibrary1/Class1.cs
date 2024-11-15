
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfLibrary1
{
    public class Class1
    {
    }
    public class FThumb : Thumb
    {
        public Canvas MyTemplateCanvas { get; private set; }

        public FThumb()
        {
            MyTemplateCanvas = MakeTemplate<Canvas>();
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            MyTemplateCanvas.Background = Brushes.AliceBlue;
            //this.Background = Brushes.Magenta;
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
