using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace _20241229_DataTemplateSelector_de_GroupThumb
{
    /// <summary>
    /// データタイプの識別用列挙体
    /// </summary>
    public enum ThumbType { None = 0, Text, Ellipse, Rect, Group, Text2, Group2 }

    /// <summary>
    /// データ用
    /// 殆どを依存関係プロパティにしているのは、値を更新したときにBinding先に通知するため
    /// </summary>
    public class MyData : DependencyObject
    {
        public ThumbType Type { get; }

        public MyData(ThumbType type)
        {
            this.Type = type;
            MyDatas = [];
        }

        #region 依存関係プロパティ

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
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(MyData), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(MyData), new PropertyMetadata(0.0));

        public double MyVolume
        {
            get { return (double)GetValue(MyVolumeProperty); }
            set { SetValue(MyVolumeProperty, value); }
        }
        public static readonly DependencyProperty MyVolumeProperty =
            DependencyProperty.Register(nameof(MyVolume), typeof(double), typeof(MyData), new PropertyMetadata(30.0));


        public Brush MyBrush
        {
            get { return (Brush)GetValue(MyBrushProperty); }
            set { SetValue(MyBrushProperty, value); }
        }
        public static readonly DependencyProperty MyBrushProperty =
            DependencyProperty.Register(nameof(MyBrush), typeof(Brush), typeof(MyData), new PropertyMetadata(Brushes.Transparent));


        public ObservableCollection<MyData> MyDatas
        {
            get { return (ObservableCollection<MyData>)GetValue(MyDatasProperty); }
            set { SetValue(MyDatasProperty, value); }
        }
        public static readonly DependencyProperty MyDatasProperty =
            DependencyProperty.Register(nameof(MyDatas), typeof(ObservableCollection<MyData>), typeof(MyData), new PropertyMetadata(null));

        #endregion 依存関係プロパティ
    }


    public class MyDTSelector : DataTemplateSelector
    {
        public DataTemplate? DT1 { get; set; }
        public DataTemplate? DT2 { get; set; }
        public DataTemplate? DT3 { get; set; }
        public DataTemplate? DT4 { get; set; }
        //public DataTemplate? DT5 { get; set; }
        //public DataTemplate? DT6 { get; set; }

        /// <summary>
        /// 今回の場合だと、引数のitemにMyDataが入っているので、
        /// データタイプを識別してそれぞれに合ったDataTemplateを返している
        /// それぞれのDataTemplateの設定はXAMLの方で行っている
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {

            if (item is not MyData)
            {
                return base.SelectTemplate(item, container);
            }
            else if (item is MyData dd)
            {
                if (dd.Type == ThumbType.Text) { return DT1; }
                else if (dd.Type == ThumbType.Ellipse) { return DT2; }
                else if (dd.Type == ThumbType.Rect) { return DT3; }
                else if (dd.Type == ThumbType.Group) { return DT4; }
                else { return base.SelectTemplate(item, container); }
            }
            return base.SelectTemplate(item, container);
        }
    }

}
