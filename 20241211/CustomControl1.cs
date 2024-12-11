using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace _20241211
{
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

        
        #endregion 依存関係プロパティ

        static KisoThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KisoThumb), new FrameworkPropertyMetadata(typeof(KisoThumb)));
        }
        public KisoThumb()
        {
            DataContext = this;
        }
    }

    public class TextBlockThumb : KisoThumb
    {
        #region 依存関係プロパティ

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(TextBlockThumb), new PropertyMetadata(string.Empty));

        #endregion 依存関係プロパティ
        static TextBlockThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockThumb), new FrameworkPropertyMetadata(typeof(TextBlockThumb)));
        }
        public TextBlockThumb()
        {
            
        }
    }


    [ContentProperty(nameof(MyThumbs))]
    public class GroupThumb : KisoThumb
    {

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
        #region DepandencyProperty

        public ObservableCollection<Thumb> MyThumbs
        {
            get { return (ObservableCollection<Thumb>)GetValue(MyThumbsProperty); }
            set { SetValue(MyThumbsProperty, value); }
        }
        public static readonly DependencyProperty MyThumbsProperty =
            DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<Thumb>), typeof(GroupThumb), new PropertyMetadata(null));
        #endregion


        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            DataContext = this;
            MyThumbs = [];
            Loaded += GroupThumb_Loaded;
            MyThumbs.CollectionChanged += MyThumbs_CollectionChanged;
        }

        //子要素を追加時に親要素のGroupThumbを追加、削除時には削除
        private void MyThumbs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems?[0] is Thumb t)
                {
                    t.Tag = this;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems?[0] is Thumb t) { t.Tag = null; }
            }
        }

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




    }




}
