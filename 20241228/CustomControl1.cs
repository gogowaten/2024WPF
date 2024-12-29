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

namespace _20241228
{

    public abstract class KisoThumb : Thumb
    {
        static KisoThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KisoThumb), new FrameworkPropertyMetadata(typeof(KisoThumb)));
        }
        public KisoThumb()
        {
            Focusable = true;
            MyData = new(ThumbType.None);
            DataContext = this;

            MySetBindings();

        }

        public MyData MyData { get; set; }

        public ThumbType MyType { get; internal set; }

        //親要素の識別用。自身がグループ化されたときに親要素のGroupThumbを入れておく
        public GroupThumb? MyParentThumb { get; internal set; }


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

        public double MyVolume
        {
            get { return (double)GetValue(MyVolumeProperty); }
            set { SetValue(MyVolumeProperty, value); }
        }
        public static readonly DependencyProperty MyVolumeProperty =
            DependencyProperty.Register(nameof(MyVolume), typeof(double), typeof(KisoThumb), new PropertyMetadata(30.0));

        public Brush MyBrush
        {
            get { return (Brush)GetValue(MyBrushProperty); }
            set { SetValue(MyBrushProperty, value); }
        }
        public static readonly DependencyProperty MyBrushProperty =
            DependencyProperty.Register(nameof(MyBrush), typeof(Brush), typeof(KisoThumb), new PropertyMetadata(Brushes.Transparent));


        public ObservableCollection<MyData> MyDatas
        {
            get { return (ObservableCollection<MyData>)GetValue(MyDatasProperty); }
            set { SetValue(MyDatasProperty, value); }
        }
        public static readonly DependencyProperty MyDatasProperty =
            DependencyProperty.Register(nameof(MyDatas), typeof(ObservableCollection<MyData>), typeof(KisoThumb), new PropertyMetadata(null));


        public bool MyIsSelected
        {
            get { return (bool)GetValue(MyIsSelectedProperty); }
            set { SetValue(MyIsSelectedProperty, value); }
        }
        public static readonly DependencyProperty MyIsSelectedProperty =
            DependencyProperty.Register(nameof(MyIsSelected), typeof(bool), typeof(KisoThumb), new PropertyMetadata(false));




        #endregion 依存関係プロパティ



        private void MySetBindings()
        {
            SetBinding(MyTextProperty, new Binding() { Path = new PropertyPath(MyData.MyTextProperty), Mode = BindingMode.TwoWay });
            SetBinding(MyLeftProperty, new Binding() { Path = new PropertyPath(MyData.MyLeftProperty), Mode = BindingMode.TwoWay });
            SetBinding(MyTopProperty, new Binding() { Path = new PropertyPath(MyData.MyTopProperty), Mode = BindingMode.TwoWay });
            SetBinding(MyVolumeProperty, new Binding() { Path = new PropertyPath(MyData.MyVolumeProperty), Mode = BindingMode.TwoWay });
            SetBinding(MyBrushProperty, new Binding() { Path = new PropertyPath(MyData.MyBrushProperty), Mode = BindingMode.TwoWay });

            //SetBinding(MyDatasProperty, new Binding() { Path = new PropertyPath(MyData.MyDatasProperty), Mode = BindingMode.TwoWay });

        }

    }

    /// <summary>
    /// 子要素の移動時にスクロール一時固定に使うアンカーThumb
    /// </summary>
    public class AnchorThumb : KisoThumb
    {
        static AnchorThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnchorThumb), new FrameworkPropertyMetadata(typeof(AnchorThumb)));
        }
        public AnchorThumb()
        {
            Focusable = false;
            MyType = ThumbType.Anchor;
        }

    }


    public class TextThumb : KisoThumb
    {
        static TextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextThumb), new FrameworkPropertyMetadata(typeof(TextThumb)));
        }

        public TextThumb()
        {
            MyType = ThumbType.Text;
            //SetBinding(MyTextProperty, new Binding() { Source = MyData, Path = new PropertyPath(MyData.MyTextProperty), Mode= BindingMode.TwoWay });

        }



    }

    [ContentProperty(nameof(MyThumbs))]
    public class GroupThumb : KisoThumb
    {
        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            MyType= ThumbType.Group;
            MyThumbs = [];
            MyDatas = [];

            Loaded += GroupThumb_Loaded;
            MyThumbs.CollectionChanged += MyThumbs_CollectionChanged;
        }


        public ItemsControl? MyItemsControl { get; set; }

        public ObservableCollection<KisoThumb> MyThumbs
        {
            get { return (ObservableCollection<KisoThumb>)GetValue(MyThumbsProperty); }
            set { SetValue(MyThumbsProperty, value); }
        }
        public static readonly DependencyProperty MyThumbsProperty =
            DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<KisoThumb>), typeof(GroupThumb), new PropertyMetadata(null));

        private void MyThumbs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems?[0] is KisoThumb ni)
            {
                ni.MyParentThumb = this;
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove && e.OldItems?[0] is KisoThumb oi)
            {
                oi.MyParentThumb = null;
            }
        }

        /// <summary>
        /// 起動直前、
        /// TemplateのRootにしているItemsControlを取得
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyItemsControl = GetTemplateChild("PART_ItemsControl") as ItemsControl;
        }


        /// <summary>
        /// 起動直後、
        /// ItemsPanelTemplateに使っている要素(ExCanvas)を取得して自身の縦横サイズとのBinding処理
        /// ItemsPanelTemplateの取得はOnApplyTemplateの時点では取得できなかったので、
        /// ここ、Loadedで行っている。
        /// </summary>
        private void GroupThumb_Loaded(object sender, RoutedEventArgs e)
        {
            //OnApplyTempleteでは取得できなかったので、ここLoadedで取得
            ExCanvas? ex = GetTemplateControl<ExCanvas>(MyItemsControl, "PART_ExCanvas");
            SetBinding(WidthProperty, new Binding() { Source = ex, Path = new PropertyPath(ActualWidthProperty) });
            SetBinding(HeightProperty, new Binding() { Source = ex, Path = new PropertyPath(ActualHeightProperty) });
        }

        /// <summary>
        /// Templateの中から指定要素を取得
        /// </summary>
        /// <typeparam name="T">取得要素の型</typeparam>
        /// <param name="d">Template</param>
        /// <param name="name">取得要素の名前</param>
        /// <returns></returns>
        internal T? GetTemplateControl<T>(DependencyObject? d, string name)
        {
            if (d == null) { return default; }
            if (d is FrameworkElement fe && fe.Name == name && fe is T t) { return t; }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                T? result = GetTemplateControl<T>(VisualTreeHelper.GetChild(d, i), name);
                if (result is not null) { return result; }
            }
            return default;
        }


        /// <summary>
        /// 再配置、ReLayoutからの改変、余計な処理をなくした。
        /// 子要素全体での左上座標を元に子要素全部と自身の位置を修正する
        /// ただし、自身がrootだった場合は子要素だけを修正する
        /// </summary>
        public void ReLayout3()
        {
            //全体での左上座標を取得
            double left = double.MaxValue; double top = double.MaxValue;
            foreach (var item in MyThumbs)
            {
                if (item.MyType != ThumbType.Anchor)
                {
                    if (left > item.MyLeft) { left = item.MyLeft; }
                    if (top > item.MyTop) { top = item.MyTop; }
                }
            }

            if (left != MyLeft)
            {
                //座標変化の場合は、自身と全ての子要素の座標を変更する
                foreach (var item in MyThumbs) { item.MyLeft -= left; }

                //自身がroot以外なら修正
                if (MyType != ThumbType.Root) { MyLeft += left; }
            }

            if (top != MyTop)
            {
                foreach (var item in MyThumbs) { item.MyTop -= top; }

                if (MyType != ThumbType.Root) { MyTop += top; }
            }

            //ParentThumbがあれば、そこでも再配置処理
            MyParentThumb?.ReLayout3();
        }



    }


    public class RootThumb:GroupThumb
    {
        static RootThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RootThumb), new FrameworkPropertyMetadata(typeof(RootThumb)));
        }
        public RootThumb()
        {
            MyType= ThumbType.Root;
        }
    }


}
