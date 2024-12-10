using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace _20241209_ResizeCanvasItemsControlThumb
{

    [ContentProperty(nameof(MyThumbs))]
    public class GroupThumb : Thumb
    {
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
            //Initialized += GroupThumb_Initialized;
        }

        //private void GroupThumb_Initialized(object? sender, EventArgs e)
        //{
        //    //ここでもCanvasが取得できない
        //}

        //TemplateのItemsControlのItemPanelのExCanvasを取得して、サイズをBinding
        private void GroupThumb_Loaded(object sender, RoutedEventArgs e)
        {
            if (GetTemplateChild("PART_ItemsControl") is ItemsControl ic)
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

        //子要素を辿ってCanvasを取得
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

    }
}
