using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace _20241227_StyleSelector
{
    public enum DataType { None = 0, Text, Ellipse, }
    public class MyData : DependencyObject
    {

        public MyData(DataType type, string text)
        {
            MyText = text;
            switch (type)
            {
                case DataType.None:
                    Type = DataType.None; break;
                case DataType.Text:
                    Type = DataType.Text; break;
                case DataType.Ellipse:
                    Type = DataType.Ellipse; break;
                default:
                    break;
            }
        }

        public DataType Type { get; }

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(MyData), new PropertyMetadata(string.Empty));


        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(MyData), new PropertyMetadata(10.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(MyData), new PropertyMetadata(0.0));

    }

    public class MySelector : StyleSelector
    {
        public Style? Style1 { get; set; }
        public Style? Style2 { get; set; }


        public override Style SelectStyle(object item, DependencyObject container)
        {

            if (item is not MyData data)
            {
                return base.SelectStyle(item, container);
            }
            if (Style1 != null && Style2 != null)
            {

                return data.Type switch
                {
                    DataType.Text => Style1,
                    DataType.Ellipse => Style2,
                    _ => base.SelectStyle(item, container)
                };
            }
            return base.SelectStyle(item, container);
        }
    }

}
