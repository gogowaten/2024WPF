using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace _20241211
{
    public class MyAdorner : Adorner
    {
        public MyAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawEllipse(Brushes.Aqua, new Pen(), new Point(80, 80), 100, 100);
            var size = this.AdornedElement.DesiredSize;
            var ele = this.AdornedElement;
        }
    }
}
