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

        //起動時とかに要素一つ一つごとにここが実行される
        //引数のitemは要素自体、containerも要素自体
        //containerから親要素のListBoxを取得はItemsControlFromitemContainerメソッド
        //親要素のItemsプロパティからitemのインデックスを取得、2で割って奇数偶数判別
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
