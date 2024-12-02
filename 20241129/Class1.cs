using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20241129
{
    public enum DataType { None = 0, Items, Text, Maru, Rect }
    public class Data : DependencyObject
    {
        #region 依存関係プロパティ

        public ObservableCollection<Data> MyDatas
        {
            get { return (ObservableCollection<Data>)GetValue(MyDatasProperty); }
            set { SetValue(MyDatasProperty, value); }
        }
        public static readonly DependencyProperty MyDatasProperty =
            DependencyProperty.Register(nameof(MyDatas), typeof(ObservableCollection<Data>), typeof(Data),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #region 基本共通

        
        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(Data),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(Data),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion 基本共通

        #region Text

        
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(Data),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region Shape

        
        public Brush MyFill
        {
            get { return (Brush)GetValue(MyFillProperty); }
            set { SetValue(MyFillProperty, value); }
        }
        public static readonly DependencyProperty MyFillProperty =
            DependencyProperty.Register(nameof(MyFill), typeof(Brush), typeof(Data),
                new FrameworkPropertyMetadata(Brushes.RosyBrown,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyWidth
        {
            get { return (double)GetValue(MyWidthProperty); }
            set { SetValue(MyWidthProperty, value); }
        }
        public static readonly DependencyProperty MyWidthProperty =
            DependencyProperty.Register(nameof(MyWidth), typeof(double), typeof(Data),
                new FrameworkPropertyMetadata(20.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyHeight
        {
            get { return (double)GetValue(MyHeightProperty); }
            set { SetValue(MyHeightProperty, value); }
        }
        public static readonly DependencyProperty MyHeightProperty =
            DependencyProperty.Register(nameof(MyHeight), typeof(double), typeof(Data),
                new FrameworkPropertyMetadata(20.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion Shape

        #endregion
        public DataType Type { get; set; }
        
        public Data() { }
    }

    //public class Datas : Data
    //{
    //    public ObservableCollection<Data> MyDatas
    //    {
    //        get { return (ObservableCollection<Data>)GetValue(MyDatasProperty); }
    //        set { SetValue(MyDatasProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyDatasProperty =
    //        DependencyProperty.Register(nameof(MyDatas), typeof(ObservableCollection<Data>), typeof(Datas),
    //            new FrameworkPropertyMetadata(null,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    //    public Datas() { Type = DataType.Items; MyDatas = []; }
    //}

    //public class DataText : Data
    //{
    //    #region DependencyProperty
    //    public string MyText
    //    {
    //        get { return (string)GetValue(MyTextProperty); }
    //        set { SetValue(MyTextProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyTextProperty =
    //        DependencyProperty.Register(nameof(MyText), typeof(string), typeof(DataText),
    //            new FrameworkPropertyMetadata(string.Empty,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    //    #endregion
    //    public DataText() { this.Type = DataType.Text;}
    //}

    //public abstract class DataShape : Data
    //{
    //    #region DependencyProperty

    //    public Brush MyFill
    //    {
    //        get { return (Brush)GetValue(MyFillProperty); }
    //        set { SetValue(MyFillProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyFillProperty =
    //        DependencyProperty.Register(nameof(MyFill), typeof(Brush), typeof(DataMaru),
    //            new FrameworkPropertyMetadata(Brushes.RosyBrown,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    //    public double MyWidth
    //    {
    //        get { return (double)GetValue(MyWidthProperty); }
    //        set { SetValue(MyWidthProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyWidthProperty =
    //        DependencyProperty.Register(nameof(MyWidth), typeof(double), typeof(DataShape),
    //            new FrameworkPropertyMetadata(20.0,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    //    public double MyHeight
    //    {
    //        get { return (double)GetValue(MyHeightProperty); }
    //        set { SetValue(MyHeightProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyHeightProperty =
    //        DependencyProperty.Register(nameof(MyHeight), typeof(double), typeof(DataShape),
    //            new FrameworkPropertyMetadata(20.0,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




    //    #endregion
    //}
    //public class DataMaru : DataShape
    //{
    //    public DataMaru() { this.Type = DataType.Maru; }
    //}
    //public class DataRect : DataShape
    //{
    //    public DataRect() { Type = DataType.Rect; }
    //}

    //今回使っているCanvas
    //これだけでは不十分かも？
    public class ExCanvas : Canvas
    {
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (double.IsNaN(Width) && double.IsNaN(Height))
            {
                base.ArrangeOverride(arrangeSize);
                Size resultSize = new();
                foreach (var item in Children.OfType<FrameworkElement>())
                {
                    double x = GetLeft(item) + item.ActualWidth;
                    double y = GetTop(item) + item.ActualHeight;
                    if (resultSize.Width < x) resultSize.Width = x;
                    if (resultSize.Height < y) resultSize.Height = y;
                }
                return resultSize;
            }
            else
            {
                return base.ArrangeOverride(arrangeSize);
            }
        }
    }

    //public class DType
    //{
    //    public DataType Type { get; private set; }
    //    public DType(DataType type) { this.Type = type; }
    //}

}









