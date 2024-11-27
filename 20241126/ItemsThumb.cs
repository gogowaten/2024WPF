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

namespace _20241126
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241126"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241126;assembly=_20241126"
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


    [ContentProperty(nameof(MyItems))]
    public class ItemsThumb : Thumb
    {
        static ItemsThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsThumb), new FrameworkPropertyMetadata(typeof(ItemsThumb)));
        }


        #region DependencyProperty


        //public ObservableCollection<UIElement> MyItems
        //{
        //    get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
        //    set { SetValue(MyItemsProperty, value); }
        //}
        //public static readonly DependencyProperty MyItemsProperty =
        //    DependencyProperty.Register(nameof(MyItems),
        //        typeof(ObservableCollection<UIElement>), typeof(ItemsThumb),
        //        new FrameworkPropertyMetadata(new ObservableCollection<UIElement>(),
        //            FrameworkPropertyMetadataOptions.AffectsRender |
        //            FrameworkPropertyMetadataOptions.AffectsMeasure |
        //            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        //↑は無限ループになる

        //FrameworkPropertyMetadata(null,
        //ここはnull、もしここで初期化するとネストした時に無限ループになるので
        //初期化はコンストラクタ内で行うことにした
        public ObservableCollection<UIElement> MyItems
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems),
                typeof(ObservableCollection<UIElement>), typeof(ItemsThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyX
        {
            get { return (double)GetValue(MyXProperty); }
            set { SetValue(MyXProperty, value); }
        }
        public static readonly DependencyProperty MyXProperty =
            DependencyProperty.Register(nameof(MyX), typeof(double), typeof(ItemsThumb),
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
            DependencyProperty.Register(nameof(MyY), typeof(double), typeof(ItemsThumb),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        #endregion DependencyProperty

        public ItemsThumb()
        {
            MyItems = [];
            DataContext = this;
        }

    }

    public class TextBlockThumb : Thumb
    {
        static TextBlockThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockThumb), new FrameworkPropertyMetadata(typeof(TextBlockThumb)));
        }

        #region 依存関係プロパティ
        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(TextBlockThumb),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double MyX
        {
            get { return (double)GetValue(MyXProperty); }
            set { SetValue(MyXProperty, value); }
        }
        public static readonly DependencyProperty MyXProperty =
            DependencyProperty.Register(nameof(MyX), typeof(double), typeof(TextBlockThumb),
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
            DependencyProperty.Register(nameof(MyY), typeof(double), typeof(TextBlockThumb),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion 依存関係プロパティ
        public TextBlockThumb() { DataContext = this; }



    }


    public abstract class CustomBase : Thumb
    {
        static CustomBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomBase), new FrameworkPropertyMetadata(typeof(CustomBase)));
        }
        //public CustomBase() { }

        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(CustomBase),
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
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(CustomBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }

    //CustomBaseを継承してみたけど、Styleは継承されないのでMyLeftのBindingもされない
    [ContentProperty(nameof(MyItems))]
    public class CustomItemsThumb2 : CustomBase
    {
        static CustomItemsThumb2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomItemsThumb2), new FrameworkPropertyMetadata(typeof(CustomItemsThumb2)));
        }

        public ObservableCollection<UIElement> MyItems
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<UIElement>), typeof(CustomItemsThumb2),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public CustomItemsThumb2() { DataContext = this; MyItems = []; }
    }

    public class CustomTextThumb2 : CustomBase
    {
        static CustomTextThumb2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextThumb2), new FrameworkPropertyMetadata(typeof(CustomTextThumb2)));
        }
        public CustomTextThumb2() { DataContext = this; }


        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(CustomTextThumb2),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }



















}
