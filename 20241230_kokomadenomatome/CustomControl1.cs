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

namespace _20241230_kokomadenomatome
{
    //Thumbの種類の識別用
    public enum Type { None = 0, Root, Group, Item, Anchor }

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

        public Type MyType { get; internal set; }

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
            MyType = Type.None;
            PreviewMouseDown += KisoThumb_PreviewMouseDown;
            PreviewMouseUp += KisoThumb_PreviewMouseUp;
            DragStarted += KisoThumb_DragStarted;
            DragCompleted += KisoThumb_DragCompleted;
        }

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

        /// <summary>
        /// ドラッグ移動終了時
        /// アンカーThumbをCollapsed化と再配置後に親要素の再配置
        /// </summary>
        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is KisoThumb t && t.MyParentThumb is not null)
            {
                if (t.MyParentThumb is GroupThumb gt)
                {
                    AnchorThumb anchor = gt.MyAnchorThumb;
                    anchor.Visibility = Visibility.Collapsed;
                    anchor.MyLeft = t.MyLeft;
                    anchor.MyTop = t.MyTop;
                }
                t.MyParentThumb.ReLayout3();
            }

            //イベントをここで停止
            e.Handled = true;
        }

        /// <summary>
        /// ドラッグ移動開始時
        /// アンカーThumbをHidden化、サイズと位置を移動要素に合わせる
        /// </summary>
        private void KisoThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (e.Source is KisoThumb t)
            {
                //アンカーThumbをHidden、在るけど見えないだけ
                if (t.MyParentThumb is GroupThumb gt)
                {
                    AnchorThumb anchor = gt.MyAnchorThumb;
                    anchor.Visibility = Visibility.Hidden;
                    anchor.Width = t.ActualWidth;
                    anchor.Height = t.ActualHeight;
                    anchor.MyLeft = t.MyLeft;
                    anchor.MyTop = t.MyTop;

                }
            }
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
            MyType = Type.Anchor;
            Focusable = false;
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

        //子要素移動時にスクロールバー固定用のアンカー
        public AnchorThumb MyAnchorThumb { get; private set; }

        #region コンストラクタ

        static GroupThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupThumb), new FrameworkPropertyMetadata(typeof(GroupThumb)));
        }
        public GroupThumb()
        {
            MyType = Type.Group;
            MyThumbs = [];
            MyAnchorThumb = new AnchorThumb() { Visibility = Visibility.Collapsed };
            MyThumbs.Add(MyAnchorThumb);//ダミー設置
            Loaded += GroupThumb_Loaded;
            MyThumbs.CollectionChanged += MyThumbs_CollectionChanged;
        }

        #endregion コンストラクタ

        #region 初期化

        /// <summary>
        /// 起動直後にBindingの設定
        /// Templateの中にあるExCanvasを取得して、自身の縦横サイズのBinding
        /// </summary>
        private void GroupThumb_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = GetTemplateChild("PART_ItemsControl");
            if (temp is ItemsControl ic)
            {
                var canvas = GetExCanvas(ic);
                if (canvas != null)
                {
                    _ = SetBinding(WidthProperty, new Binding() { Source = canvas, Path = new PropertyPath(ActualWidthProperty) });
                    _ = SetBinding(HeightProperty, new Binding() { Source = canvas, Path = new PropertyPath(ActualHeightProperty) });
                }
            }
        }

        /// <summary>
        /// Templateの中にあるExCanvasの取得
        /// </summary>
        private static ExCanvas? GetExCanvas(DependencyObject d)
        {
            if (d is ExCanvas canvas) { return canvas; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                ExCanvas? c = GetExCanvas(VisualTreeHelper.GetChild(d, i));
                if (c is not null) return c;
            }
            return null;
        }

        /// <summary>
        /// 子要素の追加時
        /// 子要素に親要素(自身)を登録
        /// </summary>
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
                if (item.MyType != Type.Anchor)
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
                if (MyType != Type.Root) { MyLeft += left; }
            }

            if (top != MyTop)
            {
                foreach (var item in MyThumbs) { item.MyTop -= top; }

                if (MyType != Type.Root) { MyTop += top; }
            }

            //ParentThumbがあれば、そこでも再配置処理
            MyParentThumb?.ReLayout3();
        }



    }

    /// <summary>
    /// root用Thumb
    /// rootは移動させない、というか移動させないときの識別用クラス
    /// </summary>
    
    public class RootThumb : GroupThumb
    {
        static RootThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RootThumb), new FrameworkPropertyMetadata(typeof(RootThumb)));
        }
        public RootThumb()
        {
            MyType = Type.Root;
        }
    }
}

