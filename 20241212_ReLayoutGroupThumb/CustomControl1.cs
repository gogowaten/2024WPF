using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Collections.Specialized;

namespace _20241212_ReLayoutGroupThumb
{


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

        #endregion 依存関係プロパティ

        //親要素の識別用。自身がグループ化されたときに親要素のGroupThumbを入れておく
        public GroupThumb? MyParentThumb { get; internal set; }
        static KisoThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KisoThumb), new FrameworkPropertyMetadata(typeof(KisoThumb)));
        }
        public KisoThumb()
        {
            DataContext = this;
            Focusable = true;
        }
    }

    public class TextBlockThumb : KisoThumb
    {
        static TextBlockThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockThumb), new FrameworkPropertyMetadata(typeof(TextBlockThumb)));
        }

    }

    public class EllipseTextThumb : TextBlockThumb
    {
        #region 依存関係プロパティ

        public double MySize
        {
            get { return (double)GetValue(MySizeProperty); }
            set { SetValue(MySizeProperty, value); }
        }
        public static readonly DependencyProperty MySizeProperty =
            DependencyProperty.Register(nameof(MySize), typeof(double), typeof(EllipseTextThumb), new PropertyMetadata(30.0));
        #endregion 依存関係プロパティ

        static EllipseTextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EllipseTextThumb), new FrameworkPropertyMetadata(typeof(EllipseTextThumb)));
        }

    }

    [ContentProperty(nameof(MyThumbs))]
    public class GroupThumb : KisoThumb
    {
        #region 依存関係プロパティ

        public ObservableCollection<KisoThumb> MyThumbs
        {
            get { return (ObservableCollection<KisoThumb>)GetValue(MyThumbsProperty); }
            set { SetValue(MyThumbsProperty, value); }
        }
        public static readonly DependencyProperty MyThumbsProperty =
            DependencyProperty.Register(nameof(MyThumbs), typeof(ObservableCollection<KisoThumb>), typeof(GroupThumb), new PropertyMetadata(null));

        #endregion 依存関係プロパティ

        #region コンストラクタ

        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            MyThumbs = [];
            Loaded += GroupThumb_Loaded;
            MyThumbs.CollectionChanged += MyThumbs_CollectionChanged;
        }

        #endregion コンストラクタ

        #region 初期化

        private void GroupThumb_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = GetTemplateChild("PART_ItemsControl");
            if (temp is ItemsControl ic)
            {
                var ec = GetExCanvas(ic);
                if (ec != null)
                {
                    _ = SetBinding(WidthProperty, new Binding() { Source = ec, Path = new PropertyPath(ActualWidthProperty) });
                    _ = SetBinding(HeightProperty, new Binding() { Source = ec, Path = new PropertyPath(ActualHeightProperty) });
                }
            }
        }


        //子要素を辿ってExCanvasを取り出す
        private static ExCanvas? GetExCanvas(DependencyObject d)
        {
            if (d is ExCanvas canvas) return canvas;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                ExCanvas? c = GetExCanvas(VisualTreeHelper.GetChild(d, i));
                if (c is not null) return c;
            }
            return null;
        }


        private void MyThumbs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems?[0] is KisoThumb nnt)
            {
                nnt.MyParentThumb = this;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems?[0] is KisoThumb ot)
            {
                ot.MyParentThumb = null;
            }
        }
        #endregion 初期化

        /// <summary>
        /// 再配置、子要素の座標を元に位置を修正する
        /// </summary>
        public void ReLayout()
        {
            //すべての子要素で最も左上の座標を取得
            double left = double.MaxValue; double top = double.MaxValue;
            foreach (var item in MyThumbs)
            {
                if (left > item.MyLeft) { left = item.MyLeft; }
                if (top > item.MyTop) { top = item.MyTop; }
            }

            //自身の座標と比較、同じ(変化なし)なら終了
            if (left == MyLeft && top == MyTop) return;

            //座標変化の場合は、自身と全ての子要素の座標を変更する
            if (left != 0)
            {
                foreach (var item in MyThumbs) { item.MyLeft -= left; }
                MyLeft += left;
            }
            if (top != 0)
            {
                foreach (var item in MyThumbs) { item.MyTop -= top; }
                MyTop += top;
            }

            //ParentThumbにも伝播させる
            MyParentThumb?.ReLayout();
        }

        /// <summary>
        /// ReLayoutの左上座標取得をRectのUnion版、いまいちなので未使用
        /// </summary>
        public void ReLayout2()
        {
            //子要素すべてが収まる範囲Rectを計算
            KisoThumb tt = MyThumbs[0];
            Rect uRect = new(tt.MyLeft, tt.MyTop, tt.ActualWidth, tt.ActualHeight);
            foreach (var item in MyThumbs)
            {
                Rect r = new(item.MyLeft, item.MyTop, item.ActualWidth, item.ActualHeight);
                uRect.Union(r);
            }

            //今の範囲Rectと比較、座標変化なしなら終了
            if (uRect.Left == MyLeft && uRect.Top == MyTop) return;

            //座標変化の場合は、自身と全ての子要素の座標を変更する
            if (uRect.Left != 0)
            {
                foreach (var item in MyThumbs) { item.MyLeft -= uRect.Left; }
                MyLeft += uRect.Left;
            }
            if (uRect.Top != 0)
            {
                foreach (var item in MyThumbs) { item.MyTop -= uRect.Top; }
                MyTop += uRect.Top;
            }

            //ParentThumbにも伝播させる
            MyParentThumb?.ReLayout2();
        }

    }

}
