using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using System.Collections.Specialized;
using Microsoft.VisualBasic;//collectionChenged

namespace _20241129
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241129"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241129;assembly=_20241129"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    //public class CustomThumb : Control
    //{
    //    static CustomThumb()
    //    {
    //        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomThumb), new FrameworkPropertyMetadata(typeof(CustomThumb)));
    //    }
    //}

    public enum ThumbType { None = 0, Root, Group, Text, Rect }

    public abstract class BaseThumb : Thumb
    {
        public abstract ThumbType Type { get; set; }
        //public Data MyData { get; set; }
        public BaseCollectionThumb? ParentThumb { get; set; }//親Thumb
        public RootThumb? RootThumb { get; set; }

        #region 依存関係プロパティ
        [Category("My")]
        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(BaseThumb),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [Category("My")]
        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(BaseThumb),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion 依存関係プロパティ

        static BaseThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseThumb), new FrameworkPropertyMetadata(typeof(BaseThumb)));
        }

        public BaseThumb()
        {
            //MyData = new();
            Binding b;
            //b = new() { Source = this, Path = new PropertyPath(MyLeftProperty) };
            //SetBinding(Canvas.LeftProperty, b);            
            //b = new() { Source = this, Path = new PropertyPath(MyTopProperty) };
            //SetBinding(Canvas.TopProperty, b);



            b = new() { Source = this, Path = new PropertyPath(MyLeftProperty) };
            SetBinding(Canvas.LeftProperty, b);
            //BindingOperations.SetBinding(MyData, Data.MyLeftProperty, b);
            b = new() { Source = this, Path = new PropertyPath(MyTopProperty) };
            SetBinding(Canvas.TopProperty, b);

            PreviewMouseDown += OnMouseDown;
            MouseDown += BaseThumb_MouseDown;

            //DataのTopLeftと要素のTopLeftのBinding
            //ここで行いたいけどなぜかできない、loadedイベントでもできない。
            //しかし、派生先のクラスではできるので
            //派生先のコンストラクタから、ここに戻って共通の処理にしたのがSetTopLeftBinding()
            //b = new() { Source = this, Path = new PropertyPath(MyLeftProperty) };
            //BindingOperations.SetBinding(MyData, DataMoto.MyLeftProperty, b);

        }

        private void BaseThumb_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var source = e.Source;
            var osource = e.OriginalSource;
        }

        //public BaseThumb(Data data)
        //{
        //    MyData = data;
        //}



        /// <summary>
        /// 派生先クラスのコンストラクタから使う
        /// </summary>
        /// <param name="thumb"></param>
        /// <param name="data"></param>
        //public static void SetTopLeftBinding(Thumb thumb, DependencyObject d)
        //{

        //    Binding b;
        //    b = new() { Source = thumb, Path = new PropertyPath(MyLeftProperty) };
        //    BindingOperations.SetBinding(d, DataMoto.MyLeftProperty, b);
        //    b = new() { Source = thumb, Path = new PropertyPath(MyTopProperty) };
        //    BindingOperations.SetBinding(d, DataMoto.MyTopProperty, b);
        //}

        public static void SetTopLeftBinding(Thumb thumb, Data data)
        {

            //Binding b;
            //b = new() { Source = thumb, Path = new PropertyPath(MyLeftProperty) };
            //BindingOperations.SetBinding(data, Data.MyLeftProperty, b);
            //b = new() { Source = thumb, Path = new PropertyPath(MyTopProperty) };
            //BindingOperations.SetBinding(data, Data.MyTopProperty, b);

        }

    }



    [ContentProperty(nameof(MyChildren))]
    public abstract class BaseCollectionThumb : BaseThumb
    {
        #region DependencyProperty

        public ObservableCollection<UIElement> MyChildren
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyChildrenProperty); }
            set { SetValue(MyChildrenProperty, value); }
        }
        public static readonly DependencyProperty MyChildrenProperty =
            DependencyProperty.Register(nameof(MyChildren), typeof(ObservableCollection<UIElement>), typeof(BaseCollectionThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion




        public abstract override ThumbType Type { get; set; }
        public Data MyData { get; set; }
        static BaseCollectionThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseCollectionThumb), new FrameworkPropertyMetadata(typeof(BaseCollectionThumb)));
        }

        public BaseCollectionThumb()
        {
            MyData = new Data() { MyDatas = [] };
            DataContext = this;
            MyChildren = [];
            Loaded += CustomItemsThumb_Loaded;
            MyChildren.CollectionChanged += MyChildren_CollectionChanged;
            SetTopLeftBinding(this, MyData);//DataのTopとLeftのBinding

        }

        private void CustomItemsThumb_Loaded(object sender, RoutedEventArgs e)
        {
            //サイズのBinding
            //ソースはTemplateの中のCanvasのActualWidthを自身のWidthにBinding
            //起動直後にTemplateの中からTemplatePanelに使っているCanvasを取り出している            
            if (Template.FindName("MyTemp", this) is DependencyObject d)
            {
                if (GetCanvas(d) is Canvas templateCanvas)
                {
                    _ = SetBinding(WidthProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualWidthProperty) });

                    _ = SetBinding(HeightProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualHeightProperty) });
                }
            }

            //子要素を辿ってCanvasを取り出す
            static Canvas? GetCanvas(DependencyObject d)
            {
                if (d is Canvas canvas) return canvas;
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
                {
                    Canvas? c = GetCanvas(VisualTreeHelper.GetChild(d, i));
                    if (c is not null) return c;
                }
                return null;
            }


        }

        private void MyChildren_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {


            if (e.Action is NotifyCollectionChangedAction.Add && e.NewItems is not null)
            {
                if (e.NewItems[0] is BaseThumb bt)
                {
                    bt.ParentThumb = this;
                    if (bt is TextThumb tt) { MyData.MyDatas.Add(tt.MyData); }
                    else if (bt is GroupThumb group) { MyData.MyDatas.Add(group.MyData); }

                }
                //var nt = e.NewItems[0];
                //if (nt is TextThumb text) { MyData.MyDatas.Add(text.MyData); }
                //else if(nt is GroupThumb group) { MyData.MyDatas.Add(group.MyData); }

                //if(nt is BaseThumb bt) { MyData.MyDatas.Add(bt.MyData); }
            }


            //if (e.Action is NotifyCollectionChangedAction.Add && e.NewItems?[0] is BaseThumb nt)
            //{
            //    nt.ParentThumb = this;
            //    if (nt.MyData is Data data) { MyData.MyDatas.Add(data); }

            //    if (nt is TextThumb textThumb)
            //    {
            //        MyData.MyDatas.Add(textThumb.MyData);

            //    }

            //}


            //if (e.Action is NotifyCollectionChangedAction.Remove && e.OldItems?[0] is BaseThumb tt && tt.MyData != null)
            //{
            //    _ = MyData.MyDatas.Remove(tt.MyData);
            //}
        }

    }



    [ContentProperty(nameof(MyChildren))]
    public class RootThumb : BaseCollectionThumb
    {
        public override ThumbType Type { get; set; }
        static RootThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RootThumb), new FrameworkPropertyMetadata(typeof(RootThumb)));
        }
        public RootThumb()
        {
            RootThumb = this;
            ParentThumb = null;
            Type = ThumbType.Root;
        }

    }


    [ContentProperty(nameof(MyChildren))]
    public class GroupThumb : BaseCollectionThumb
    {
        public override ThumbType Type { get; set; }
        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            Type = ThumbType.Group;
        }
    }


    //[ContentProperty(nameof(MyChildren))]
    //public class GroupThumb : BaseThumb
    //{
    //    public override ThumbType Type { get; set; } = ThumbType.Group;
    //    public Data MyData { get; set; }

    //    static GroupThumb()
    //    {
    //        DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
    //    }
    //    public GroupThumb()
    //    {
    //        Type = ThumbType.Group;
    //        MyData = new Data() { MyDatas = [] };
    //        DataContext = this;
    //        MyChildren = [];
    //        Loaded += CustomItemsThumb_Loaded;
    //        MyChildren.CollectionChanged += MyChildren_CollectionChanged;
    //        SetTopLeftBinding(this, MyData);//DataのTopとLeftのBinding

    //    }

    //    //元のそれぞれの型のThumbにキャストしてからDataをリストに追加しているのは、
    //    //キャストしないとDataMotoにあるプロパティだけになってしまうから
    //    //System.Collections.Specialized.NotifyCollectionChangedEventArgs
    //    private void MyChildren_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    //    {
    //        //if (e.Action is NotifyCollectionChangedAction.Add && e.NewItems != null)
    //        //{
    //        //    Data? data = null;

    //        //    switch (e.NewItems[0])
    //        //    {
    //        //        case TextThumb txt:
    //        //            data = txt.MyData;
    //        //            break;
    //        //        case GroupThumb items:
    //        //            data = items.MyData;
    //        //            break;
    //        //    }

    //        //    if (data is not null)
    //        //    {
    //        //        MyData.MyDatas.Add(data);
    //        //    }
    //        //}

    //        if (e.Action is NotifyCollectionChangedAction.Add && e.NewItems is not null)
    //        {
    //            if (e.NewItems[0] is BaseThumb bt)
    //            {
    //                bt.ParentThumb = this;
    //                if (bt is TextThumb tt) { MyData.MyDatas.Add(tt.MyData); }
    //                else if (bt is GroupThumb group) { MyData.MyDatas.Add(group.MyData); }

    //            }
    //            //var nt = e.NewItems[0];
    //            //if (nt is TextThumb text) { MyData.MyDatas.Add(text.MyData); }
    //            //else if(nt is GroupThumb group) { MyData.MyDatas.Add(group.MyData); }

    //            //if(nt is BaseThumb bt) { MyData.MyDatas.Add(bt.MyData); }
    //        }


    //        //if (e.Action is NotifyCollectionChangedAction.Add && e.NewItems?[0] is BaseThumb nt)
    //        //{
    //        //    nt.ParentThumb = this;
    //        //    if (nt.MyData is Data data) { MyData.MyDatas.Add(data); }

    //        //    if (nt is TextThumb textThumb)
    //        //    {
    //        //        MyData.MyDatas.Add(textThumb.MyData);

    //        //    }

    //        //}


    //        //if (e.Action is NotifyCollectionChangedAction.Remove && e.OldItems?[0] is BaseThumb tt && tt.MyData != null)
    //        //{
    //        //    _ = MyData.MyDatas.Remove(tt.MyData);
    //        //}
    //    }

    //    private void CustomItemsThumb_Loaded(object sender, RoutedEventArgs e)
    //    {
    //        //サイズのBinding
    //        //ソースはTemplateの中のCanvasのActualWidthを自身のWidthにBinding
    //        //起動直後にTemplateの中からTemplatePanelに使っているCanvasを取り出している            
    //        if (Template.FindName("MyTemp", this) is DependencyObject d)
    //        {
    //            if (GetCanvas(d) is Canvas templateCanvas)
    //            {
    //                _ = SetBinding(WidthProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualWidthProperty) });

    //                _ = SetBinding(HeightProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualHeightProperty) });
    //            }
    //        }

    //        //子要素を辿ってCanvasを取り出す
    //        static Canvas? GetCanvas(DependencyObject d)
    //        {
    //            if (d is Canvas canvas) return canvas;
    //            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
    //            {
    //                Canvas? c = GetCanvas(VisualTreeHelper.GetChild(d, i));
    //                if (c is not null) return c;
    //            }
    //            return null;
    //        }


    //    }

    //    #region DependencyProperty


    //    public ObservableCollection<UIElement> MyChildren
    //    {
    //        get { return (ObservableCollection<UIElement>)GetValue(MyChildrenProperty); }
    //        set { SetValue(MyChildrenProperty, value); }
    //    }
    //    public static readonly DependencyProperty MyChildrenProperty =
    //        DependencyProperty.Register(nameof(MyChildren), typeof(ObservableCollection<UIElement>), typeof(GroupThumb),
    //            new FrameworkPropertyMetadata(null,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    //    private bool isActive;
    //    #endregion

    //}



    public class TextThumb : BaseThumb
    {
        public override ThumbType Type { get; set; } = ThumbType.Text;
        public Data MyData { get; set; }

        #region DependencyProperty


        [Category("My")]
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(TextThumb),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        static TextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextThumb), new FrameworkPropertyMetadata(typeof(TextThumb)));
        }
        public TextThumb() : this(new Data())
        {

        }

        public TextThumb(Data data)
        {
            DataContext = this;
            MyData = data;

            //TextのBinding
            Binding b;
            b = new() { Source = this, Path = new PropertyPath(MyTextProperty) };
            BindingOperations.SetBinding(MyData, Data.MyTextProperty, b);

            //↓だとBindingができない
            //b = new() { Source = MyData, Path = new PropertyPath(Data.MyTextProperty) };
            //SetBinding(MyTextProperty, b);

            //TopLeftのBinding
            //ThumbとCanvasはBinding済、SourceはThumb
            //残りはDataとThumbのBinding
            b = new() { Source = this, Path = new PropertyPath(MyLeftProperty) };
            BindingOperations.SetBinding(MyData, Data.MyLeftProperty, b);
            b = new() { Source = this, Path = new PropertyPath(MyTopProperty) };
            BindingOperations.SetBinding(MyData, Data.MyTopProperty, b);

        }


    }


}
