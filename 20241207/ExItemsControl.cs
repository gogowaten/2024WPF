using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _20241207
{
    public class ExItemsControl : ItemsControl
    {
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (double.IsNaN(Width) && double.IsNaN(Height))
            {
                base.ArrangeOverride(arrangeBounds);
                Size resultSize = new();
                foreach (var item in Items.OfType<FrameworkElement>())
                {
                    double x = Canvas.GetLeft(item) + item.ActualWidth;
                    double y = Canvas.GetTop(item) + item.ActualHeight;
                    if (resultSize.Width < x) resultSize.Width = x;
                    if (resultSize.Height < y) resultSize.Height = y;
                }
                return resultSize;
            }
            else
            {
                return base.ArrangeOverride(arrangeBounds);
            }

        }
    }
}
