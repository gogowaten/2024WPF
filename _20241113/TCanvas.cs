using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace _20241113
{
    internal class TCanvas : Canvas
    {
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (double.IsNaN(Width) && double.IsNaN(Height))
            {
                base.ArrangeOverride(arrangeSize);
                Size size = new();
                foreach (var item in Children.OfType<FrameworkElement>())
                {
                    double x = GetLeft(item) + item.ActualWidth;
                    double y = GetTop(item) + item.ActualHeight;
                    if (size.Width < x) size.Width = x;
                    if (size.Height < y) size.Height = y;
                }
                return size;
            }
            else
            {
                return base.ArrangeOverride(arrangeSize);
            }
        }

        public Rect GetCildrenBounds()
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(this);
            return rect;
        }

        protected override void ParentLayoutInvalidated(UIElement child)
        {
            base.ParentLayoutInvalidated(child);
        }
    }
}
