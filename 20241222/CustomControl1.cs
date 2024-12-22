using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20241222
{

    //Thumbの種類の識別用
    public enum Type { None = 0, Root, Group, Item, Anchor, TextBlock, EllipseText }

    public abstract class CustomBase : Thumb
    {
        static CustomBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomBase), new FrameworkPropertyMetadata(typeof(CustomBase)));
        }
        #region 依存関係プロパティ

        public double MyX
        {
            get { return (double)GetValue(MyXProperty); }
            set { SetValue(MyXProperty, value); }
        }
        public static readonly DependencyProperty MyXProperty =
            DependencyProperty.Register(nameof(MyX), typeof(double), typeof(CustomBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyY
        {
            get { return (double)GetValue(MyYProperty); }
            set { SetValue(MyYProperty, value); }
        }
        public static readonly DependencyProperty MyYProperty =
            DependencyProperty.Register(nameof(MyY), typeof(double), typeof(CustomBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion 依存関係プロパティ
        public CustomBase()
        {
            SetBinding(Canvas.LeftProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyXProperty)
            });
            SetBinding(Canvas.TopProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyYProperty)
            });


        }
    }




    [ContentProperty(nameof(MyItems))]
    public class CustomItemsThumb : CustomBase
    {
        static CustomItemsThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomItemsThumb), new FrameworkPropertyMetadata(typeof(CustomItemsThumb)));
        }
        public CustomItemsThumb()
        {
            DataContext = this;
            MyItems = [];
            //Background = Brushes.Red;
            Loaded += CustomItemsThumb_Loaded;
        }

        private static Canvas? GetCanvas(DependencyObject d)
        {
            if (d is Canvas canvas) return canvas;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                Canvas? c = GetCanvas(VisualTreeHelper.GetChild(d, i));
                if (c is not null) return c;
            }
            return null;
        }
        private void CustomItemsThumb_Loaded(object sender, RoutedEventArgs e)
        {

            object temp = Template.FindName("MyTemp", this);
            if (temp is DependencyObject d)
            {
                if (GetCanvas(d) is Canvas tpcan)
                {
                    //SetBinding(Thumb.WidthProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.WidthProperty) });

                    SetBinding(Thumb.WidthProperty, new Binding() { Source = tpcan, Path = new PropertyPath(Canvas.ActualWidthProperty) });

                    SetBinding(Thumb.HeightProperty, new Binding() { Source = tpcan, Path = new PropertyPath(Canvas.ActualHeightProperty) });

                    //SetBinding(Thumb.BackgroundProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.BackgroundProperty) });

                }
            }

        }

        public ObservableCollection<UIElement> MyItems
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<UIElement>), typeof(CustomItemsThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }


    public class CustomTextThumb : CustomBase
    {
        static CustomTextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextThumb), new FrameworkPropertyMetadata(typeof(CustomTextThumb)));
        }

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(CustomTextThumb),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public CustomTextThumb()
        {
            DataContext = this;
        }
    }



    /// <summary>
    /// 基礎Thumb、すべてのCustomControlThumbの派生元
    /// </summary>
    public class KisoThumb : Thumb
    {
        #region 依存関係プロパティ

        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(KisoThumb), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(KisoThumb), new PropertyMetadata(0.0));

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(KisoThumb), new PropertyMetadata(string.Empty));


        public bool MyIsSelected
        {
            get { return (bool)GetValue(MyIsSelectedProperty); }
            set { SetValue(MyIsSelectedProperty, value); }
        }
        public static readonly DependencyProperty MyIsSelectedProperty =
            DependencyProperty.Register(nameof(MyIsSelected), typeof(bool), typeof(KisoThumb), new PropertyMetadata(false));






        #endregion 依存関係プロパティ

        public Type MyType { get; internal set; }

        //親要素の識別用。自身がグループ化されたときに親要素のGroupThumbを入れておく
        //public GroupThumb? MyParentThumb { get; internal set; }
        static KisoThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KisoThumb), new FrameworkPropertyMetadata(typeof(KisoThumb)));
        }
        public KisoThumb()
        {
            DataContext = this;
            Focusable = true;
            MyType = Type.None;
            //PreviewMouseDown += KisoThumb_PreviewMouseDown;
            //PreviewMouseUp += KisoThumb_PreviewMouseUp;
            Loaded += KisoThumb_Loaded;
        }

        #region 枠の設定        

        private void KisoThumb_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = GetTemplateChild("PART_Grid");
            if (temp is Grid ic)
            {
                ContentControl? cc = GetRectangle(ic);
                
                if (cc != null)
                {
                    cc.Content = new TextBlock() { Text = "22222222222222"};


                    //Binding b;
                    //b = new() { Source = this, Path = new PropertyPath(MyIsSelectedProperty), Converter = new MyConverterVisible() };
                    //cc.SetBinding(VisibilityProperty, b);
                    


                }
            }
        }
        private static ContentControl? GetRectangle(DependencyObject d)
        {
            if (d is ContentControl rectan) { return rectan; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                ContentControl? c = GetRectangle(VisualTreeHelper.GetChild(d, i));
                if (c is not null) { return c; }
            }
            return null;
        }

        #endregion 枠の設定



        /// <summary>
        /// マウスアップ時、フォーカスを有効化してフォーカスする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KisoThumb_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.Focusable = true;
                t.Focus();
            }
        }

        /// <summary>
        /// クリックダウン時、フォーカス無効化する。
        /// フォーカスでスクロール位置がガクッと変更されて不自然なのを防ぐ
        /// </summary>
        private void KisoThumb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.Focusable = false;
            }
        }


    }


    




    public class MyConverterVisible : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b == true) { return Visibility.Visible; }
            else { return Visibility.Collapsed; }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
