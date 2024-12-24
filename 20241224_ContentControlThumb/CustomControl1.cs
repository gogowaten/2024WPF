using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace _20241224_ContentControlThumb
{

    /// <summary>
    /// 基礎(ベース、派生元用)Thumb
    /// </summary>
    public class Kiso2Thumb : Thumb
    {
        static Kiso2Thumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Kiso2Thumb), new FrameworkPropertyMetadata(typeof(Kiso2Thumb)));
        }
        public Kiso2Thumb()
        {
            DataContext = this;
        }


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

        #endregion 依存関係プロパティ

    }


    /// <summary>
    /// 単体要素用のThumb
    /// </summary>
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

        /// <summary>
        /// Templete構築時、
        /// Templateの中からContentControlを取得
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyContentControl = GetTemplateChild("myContentControl") as ContentControl;
        }

    }


    /// <summary>
    /// 複数要素用のThumb
    /// </summary>
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

        /// <summary>
        /// 起動直後、
        /// ItemsPanelTemplateに使っている要素(ExCanvas)を取得して自身とのBinding処理
        /// ItemsPanelTemplateの取得はOnApplyTemplateの時点では取得できなかったので、
        /// ここ、Loadedで行っている。
        /// </summary>
        private void Kiso4_Loaded(object sender, RoutedEventArgs e)
        {
            //OnApplyTempleteでは取得できなかったので、ここLoadedで取得
            ExCanvas? ex = GetTemplateControl<ExCanvas>(MyItemsControl, "myCanvas");
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

        /// <summary>
        /// Templete構築時、
        /// Templateの中からItemsControlを取得
        /// </summary>

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MyItemsControl = GetTemplateChild("myItemsControl") as ItemsControl;
        }

    }


}
