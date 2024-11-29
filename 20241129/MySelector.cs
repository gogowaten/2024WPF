using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _20241129
{

    //Q102. ListBox で一行おきに背景色を変えるには？ - 周回遅れのブルース
    //https://hilapon.hatenadiary.org/entry/20130918/1379486632

    public class MySelector : StyleSelector
    {
        public Style EvenStyle { get; set; } = new Style();
        public Style OddStyle { get; set; } = new Style();

        public override Style SelectStyle(object item, DependencyObject container)
        {
            ItemsControl control = ItemsControl.ItemsControlFromItemContainer(container);
            if ((control.Items.IndexOf(item) % 2) == 0)
            {
                return OddStyle;
            }
            else
            {
                return EvenStyle;
            }
            //return base.SelectStyle(item, container);
        }
    }
}
