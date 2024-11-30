using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
    public class MyDataSelector : StyleSelector
    {
        public Style TextStyle { get; set; } = new Style();
        public Style MaruStyle { get; set; } = new Style();
        public Style RectStyle { get; set; } = new Style();
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is DataMoto data)
            {
                DataType da = data.Type;

                switch (da)
                {
                    case DataType.Text:
                        return TextStyle;
                    case DataType.Maru:
                        return MaruStyle;
                    case DataType.Rect: return RectStyle;
                    default: return base.SelectStyle(item, container);
                }

            }
            return base.SelectStyle(item, container);
        }
    }

    //【WPF】DataTemplateSelectorクラスを使用して、動的にコントロールの種類を変更する方法【C#】 #Xaml - Qiita
    //    https://qiita.com/MetroOsamu/items/572bdb0f86b284fea588

    //C#のWPFで複数のテンプレートを切り替えて使う - Ararami Studio
    //    https://araramistudio.jimdofree.com/2016/10/04/wpf%E3%81%A7%E8%A4%87%E6%95%B0%E3%81%AE%E3%83%86%E3%83%B3%E3%83%97%E3%83%AC%E3%83%BC%E3%83%88%E3%82%92%E5%88%87%E3%82%8A%E6%9B%BF%E3%81%88%E3%81%A6%E4%BD%BF%E3%81%86/

    public class ItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? TextGata { get; set; }
        public DataTemplate? MaruGata { get; set; }
        public DataTemplate? RectGata { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {

            if (item is DataMoto data)
            {
                DataType da = data.Type;

                switch (da)
                {
                    case DataType.Text:
                        return TextGata;
                    case DataType.Maru:
                        return MaruGata;
                    case DataType.Rect:
                        return RectGata;
                    default:
                        return base.SelectTemplate(item, container);
                }

            }
            return base.SelectTemplate(item, container);
            
        }
    }

  


}
