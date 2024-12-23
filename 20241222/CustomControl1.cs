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
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }



    /// <summary>
    /// 基礎Thumb、すべてのCustomControlThumbの派生元
    /// </summary>    
    public abstract class KisoThumb : Thumb
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
            //DataContext = this;
            Focusable = true;
            MyType = Type.None;
            ApplyTemplate();
            //PreviewMouseDown += KisoThumb_PreviewMouseDown;
            //PreviewMouseUp += KisoThumb_PreviewMouseUp;
            //Loaded += KisoThumb_Loaded;
            Loaded += KisoThumb_Loaded;
            OnApplyTemplate();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var neko = this.Template;
        }

        private void KisoThumb_Loaded(object sender, RoutedEventArgs e)
        {
            var neko = this.Template;
        }

        #region 枠の設定        

        //private void KisoThumb_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.ApplyTemplate();
        //    var neko = this.Template;
        //    var temp = GetTemplateChild("mytemp");
        //    MyContentControl = GetTemplateControl<ContentControl>(temp, "PART_ContentControl");

        //    if (MyContentControl != null)
        //    {
        //        MyContentControl.SetBinding(ContentControl.ContentProperty, new Binding()
        //        {
        //            Source = this,
        //            Path = new PropertyPath(MyContentProperty),
        //        });

        //        Rectangle? stroke1 = GetTemplateControl<Rectangle>(temp, "Stroke1");
        //        Rectangle? stroke2 = GetTemplateControl<Rectangle>(temp, "Stroke2");
        //        if (stroke1 != null && stroke2 != null)
        //        {
        //            Binding b;
        //            b = new() { Source = this, Path = new PropertyPath(MyIsSelectedProperty), Converter = new MyConverterVisible() };
        //            stroke1.SetBinding(VisibilityProperty, b);
        //            stroke2.SetBinding(VisibilityProperty, b);
        //            SetBinding(MyIsSelectedProperty, new Binding() { Source = this, Path = new PropertyPath(IsKeyboardFocusedProperty) });
        //            //SetBinding(MyIsSelectedProperty, new Binding() { Source = this, Path = new PropertyPath(IsFocusedProperty) });

        //        }
        //    }

        //}


        internal T? GetTemplateControl<T>(DependencyObject d, string name)
        {
            if (d == null) { return default; }
            if (d is FrameworkElement fe && fe.Name == name && fe is T t) { return t; }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                T? c = GetTemplateControl<T>(VisualTreeHelper.GetChild(d, i), name);
                if (c is not null) { return c; }
            }
            return default;
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

    [ContentProperty(nameof(MyContent))]
    public class ItemThumb : KisoThumb
    {
        static ItemThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemThumb), new FrameworkPropertyMetadata(typeof(ItemThumb)));
        }
        public ItemThumb()
        {
            Loaded += ItemThumb_Loaded;
        }

        private void ItemThumb_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = this.Template;
            var neko = GetTemplateChild("mytemp");
        }

        private ContentControl? MyContentControl { get; set; }

        public UIElement MyContent
        {
            get { return (UIElement)GetValue(MyContentProperty); }
            set { SetValue(MyContentProperty, value); }
        }
        public static readonly DependencyProperty MyContentProperty =
            DependencyProperty.Register(nameof(MyContent), typeof(UIElement), typeof(KisoThumb), new PropertyMetadata(null));





    }




    //public class GroupThumb : KisoThumb
    //{
    //    static GroupThumb()
    //    {
    //        DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
    //    }
    //    public GroupThumb()
    //    {

    //    }

    //    //子要素移動時にスクロールバー固定用のアンカー
    //    //public AnchorThumb MyAnchorThumb { get; private set; }

    //    #region 依存関係プロパティ

    //    public ObservableCollection<KisoThumb> MyThumbs
    //    {
    //        get { return (ObservableCollection<KisoThumb>)GetValue(MyThumbsProperty); }
    //        set { SetValue(MyThumbsProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyThumbsProperty =
    //        DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<KisoThumb>), typeof(GroupThumb), new PropertyMetadata(null));



    //    #endregion 依存関係プロパティ


    //}

    public class Kiso2Thumb : Thumb
    {
        static Kiso2Thumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Kiso2Thumb), new FrameworkPropertyMetadata(typeof(Kiso2Thumb)));
        }
        public Kiso2Thumb()
        {
            DataContext = this;
            PreviewMouseDown += Kiso2Thumb_PreviewMouseDown;
            //PreviewMouseUp += Kiso2Thumb_PreviewMouseUp;
        }

        //private void Kiso2Thumb_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if(sender is Kiso2Thumb t) { t.Focusable = true; t.Focus(); }
        //}

        private void Kiso2Thumb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Kiso2Thumb t) { t.MyIsSelected = !t.MyIsSelected; }
            //if (sender is Kiso2Thumb t) { t.Focusable = false; }
        }

        public Rectangle? MyStroke1 { get; set; }
        public Rectangle? MyStroke2 { get; set; }

        #region 依存関係プロパティ

        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(Kiso2Thumb), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(Kiso2Thumb), new PropertyMetadata(0.0));

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(Kiso2Thumb), new PropertyMetadata(string.Empty));


        public bool MyIsSelected
        {
            get { return (bool)GetValue(MyIsSelectedProperty); }
            set { SetValue(MyIsSelectedProperty, value); }
        }
        public static readonly DependencyProperty MyIsSelectedProperty =
            DependencyProperty.Register(nameof(MyIsSelected), typeof(bool), typeof(Kiso2Thumb), new PropertyMetadata(false));






        #endregion 依存関係プロパティ


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyStroke1 = GetTemplateChild("Stroke1") as Rectangle;
            MyStroke2 = GetTemplateChild("Stroke2") as Rectangle;
            if (MyStroke1 != null && MyStroke2 != null)
            {
                Binding b = new Binding() { Source = this, Path = new PropertyPath(MyIsSelectedProperty), Converter = new MyConverterVisible() };
                MyStroke1.SetBinding(VisibilityProperty, b);
                MyStroke2.SetBinding(VisibilityProperty, b);

            }
        }
    }

    [ContentProperty(nameof(MyContent))]
    public class Kiso3 : Kiso2Thumb
    {
        static Kiso3()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Kiso3), new FrameworkPropertyMetadata(typeof(Kiso3)));
        }
        public Kiso3()
        {

        }


        public ContentControl? MyContentControl { get; set; }

        public UIElement MyContent
        {
            get { return (UIElement)GetValue(MyContentProperty); }
            set { SetValue(MyContentProperty, value); }
        }
        public static readonly DependencyProperty MyContentProperty =
            DependencyProperty.Register(nameof(MyContent), typeof(UIElement), typeof(Kiso3), new PropertyMetadata(null));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyContentControl = GetTemplateChild("myContentControl") as ContentControl;

            //if (MyContentControl != null)
            //{
            //    _ = MyContentControl.SetBinding(ContentControl.ContentProperty, new Binding()
            //    {
            //        Source = this,
            //        Path = new PropertyPath(MyContentProperty)
            //    });
            //}
        }

    }

    [ContentProperty(nameof(MyThumbs))]
    public class Kiso4 : Kiso2Thumb
    {
        static Kiso4()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Kiso4), new FrameworkPropertyMetadata(typeof(Kiso4)));
        }
        public Kiso4()
        {
            MyThumbs = [];
            Loaded += Kiso4_Loaded;

        }


        private void Kiso4_Loaded(object sender, RoutedEventArgs e)
        {
            //ここじゃないと取得できない
            ExCanvas? ex = GetTemplateControl<ExCanvas>(MyItemsControl, "myCanvas");
            SetBinding(WidthProperty, new Binding() { Source = ex, Path = new PropertyPath(ActualWidthProperty) });
            SetBinding(HeightProperty, new Binding() { Source = ex, Path = new PropertyPath(ActualHeightProperty) });
        }

        internal T? GetTemplateControl<T>(DependencyObject? d, string name)
        {
            if (d == null) { return default; }
            if (d is FrameworkElement fe && fe.Name == name && fe is T t) { return t; }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                T? c = GetTemplateControl<T>(VisualTreeHelper.GetChild(d, i), name);
                if (c is not null) { return c; }
            }
            return default;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyItemsControl = GetTemplateChild("myItemsControl") as ItemsControl;


        }

        public ItemsControl? MyItemsControl { get; set; }

        #region 依存関係プロパティ

        public ObservableCollection<Kiso3> MyThumbs
        {
            get { return (ObservableCollection<Kiso3>)GetValue(MyThumbsProperty); }
            set { SetValue(MyThumbsProperty, value); }
        }
        public static readonly DependencyProperty MyThumbsProperty =
            DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<Kiso3>), typeof(Kiso4), new PropertyMetadata(null));



        #endregion 依存関係プロパティ


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