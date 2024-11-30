using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public abstract class DataMoto : DependencyObject
    {
        #region 依存関係プロパティ

        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(DataMoto),
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
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(DataMoto),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion
        public DataType Type { get; set; }
        public DataMoto() { }
    }

    public class DataItems : DataMoto
    {

        public ObservableCollection<DataMoto> MyItems
        {
            get { return (ObservableCollection<DataMoto>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<DataMoto>), typeof(DataItems),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public DataItems() { Type = DataType.Items; }
    }

    public class DataText : DataMoto
    {
        #region DependencyProperty
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(DataText),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        #endregion
        public DataText() { this.Type = DataType.Text; }
    }

    public abstract class DataShape : DataMoto
    {
        #region DependencyProperty

        public Brush MyFill
        {
            get { return (Brush)GetValue(MyFillProperty); }
            set { SetValue(MyFillProperty, value); }
        }
        public static readonly DependencyProperty MyFillProperty =
            DependencyProperty.Register(nameof(MyFill), typeof(Brush), typeof(DataMaru),
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
            DependencyProperty.Register(nameof(MyWidth), typeof(double), typeof(DataShape),
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
            DependencyProperty.Register(nameof(MyHeight), typeof(double), typeof(DataShape),
                new FrameworkPropertyMetadata(20.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        #endregion
    }
    public class DataMaru : DataShape
    {
        public DataMaru() { this.Type = DataType.Maru; }
    }
    public class DataRect : DataShape
    {
        public DataRect() { Type = DataType.Rect; }
    }

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

}









