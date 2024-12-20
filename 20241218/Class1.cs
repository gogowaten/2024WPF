using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace _20241218
{




    public class FocusAdorner : Adorner
    {
        // Be sure to call the base class constructor.
        public FocusAdorner(UIElement adornedElement) : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout system as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            var drawRect = LayoutInformation.GetLayoutSlot((FrameworkElement)this.AdornedElement);
            drawRect = new Rect(1, 1, drawRect.Width - 2, drawRect.Height - 2);


            // Some arbitrary drawing implements.
            //SolidColorBrush renderBrush = new(Colors.Transparent);
            Pen renderPen = new(new SolidColorBrush(Colors.Black), 2)
            {
                DashStyle = new DashStyle([2, 3], 0)
            };

            drawingContext.DrawRoundedRectangle(Brushes.Transparent, renderPen, drawRect, 3, 3);
        }
    }



}
