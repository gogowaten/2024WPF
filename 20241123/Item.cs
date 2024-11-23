using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _20241123
{
    public class Item
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class PathItem : Item
    {
        public Brush Stroke { get; set; } = Brushes.Black;
        public string Data { get; set; } = string.Empty;
    }

    public class EllipseItem : Item
    {
        public Brush Fill { get; set; } = Brushes.Red;
    }

    public class RectangleItem : Item
    {
        public Brush Fill { get; set; } = Brushes.Blue;
    }

    public class RichTextBoxItem : Item
    {
        public string Text { get; set; }
    }
    public class TextBlockItem : Item
    {
        public string Text { get; set; }
    }

}
