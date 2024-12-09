using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _20241207
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241207"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241207;assembly=_20241207"
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
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }
    }

    //[TemplatePart(Name = nameof(TempName), Type = typeof(ItemsControl))]
    [ContentProperty(nameof(MyThumbs))]
    public class GroupThumb : Thumb
    {
        private const string TempName = "PART_ItemsControl";

        //public ObservableCollection<Thumb> MyThumbs
        //{
        //    get { return (ObservableCollection<Thumb>)GetValue(MyThumbsProperty); }
        //    set { SetValue(MyThumbsProperty, value); }
        //}
        //public static readonly DependencyProperty MyThumbsProperty =
        //    DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<Thumb>), typeof(GroupThumb), new FrameworkPropertyMetadata(null,
        //        FrameworkPropertyMetadataOptions.AffectsRender |
        //        FrameworkPropertyMetadataOptions.AffectsMeasure |
        //        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ObservableCollection<Thumb> MyThumbs
        {
            get { return (ObservableCollection<Thumb>)GetValue(MyThumbsProperty); }
            set { SetValue(MyThumbsProperty, value); }
        }
        public static readonly DependencyProperty MyThumbsProperty =
            DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<Thumb>), typeof(GroupThumb), new PropertyMetadata(null));

        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            DataContext = this;
            MyThumbs = [];
            Loaded += GroupThumb_Loaded;
        }

        //TemplateのItemsControlのItemPanelのExCanvasを取得して、サイズをBinding
        private void GroupThumb_Loaded(object sender, RoutedEventArgs e)
        {

            if (GetTemplateChild(TempName) is ItemsControl ic)
            {
                var c = GetCanvas(ic);
                if (c != null)
                {
                    _ = SetBinding(WidthProperty, new Binding() { Source = c, Path = new PropertyPath(ActualWidthProperty) });

                    _ = SetBinding(HeightProperty, new Binding() { Source = c, Path = new PropertyPath(ActualHeightProperty) });
                }
            }
        }

        ////TemplateのItemsControlのItemPanelのExCanvasを取得して、サイズをBindingなんだけど
        ////OnApplyTempleteではExCanvasが取得できない、この時点ではItemPanelが構築されていないみたい？
        ////なので、この処理はLoadedですることにした
        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    if (GetTemplateChild(TempName) is ItemsControl ic)
        //    {
        //        //ic.OnApplyTemplate();//効果がない
        //        var c = GetCanvas(ic);//null
        //        if (c != null)
        //        {
        //            _ = SetBinding(WidthProperty, new Binding() { Source = c, Path = new PropertyPath(ActualWidthProperty) });
        //            _ = SetBinding(HeightProperty, new Binding() { Source = c, Path = new PropertyPath(ActualHeightProperty) });
        //        }
        //    }
        //}

        //子要素を辿ってCanvasを取り出す
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



        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    DependencyObject temp = GetTemplateChild(TempName);
        //    if (temp is ItemsControl ic)
        //    {
        //        SetBinding(WidthProperty, new Binding() { Source = ic, Path = new PropertyPath(ActualWidthProperty) });
        //        SetBinding(HeightProperty, new Binding() { Source = ic, Path = new PropertyPath(ActualHeightProperty) });
        //    }

        //}


        //protected override Size ArrangeOverride(Size arrangeBounds)
        //{
        //    if (double.IsNaN(Width) && double.IsNaN(Height))
        //    {
        //        base.ArrangeOverride(arrangeBounds);
        //        Size resultSize = new();
        //        foreach (var item in MyThumbs.OfType<FrameworkElement>())
        //        {
        //            double x = Canvas.GetLeft(item) + item.ActualWidth;
        //            double y = Canvas.GetTop(item) + item.ActualHeight;
        //            if (resultSize.Width < x) resultSize.Width = x;
        //            if (resultSize.Height < y) resultSize.Height = y;
        //        }
        //        return resultSize;
        //    }
        //    else
        //    {
        //        return base.ArrangeOverride(arrangeBounds);
        //    }
        //}

        //[ContentProperty(nameof(MyThumbs))]
        //public class GroupThumb : Thumb
        //{

        //    public ObservableCollection<Thumb> MyThumbs
        //    {
        //        get { return (ObservableCollection<Thumb>)GetValue(MyThumbsProperty); }
        //        set { SetValue(MyThumbsProperty, value); }
        //    }
        //    public static readonly DependencyProperty MyThumbsProperty =
        //        DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<Thumb>), typeof(GroupThumb), new PropertyMetadata(null));

        //    static GroupThumb()
        //    {
        //        DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        //    }
        //    public GroupThumb()
        //    {
        //        DataContext = this;
        //        MyThumbs = [];
        //    }
        //}


    }
}
