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
    public class CustomThumb : Control
    {
        static CustomThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomThumb), new FrameworkPropertyMetadata(typeof(CustomThumb)));
        }
    }

    public abstract class BaseThumb : Thumb
    {
        public DataMoto? MyData { get; set; }
        static BaseThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseThumb), new FrameworkPropertyMetadata(typeof(BaseThumb)));
        }
        #region 依存関係プロパティ

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
        public BaseThumb()
        {
            SetBinding(Canvas.LeftProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyLeftProperty)
            });
            SetBinding(Canvas.TopProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyTopProperty)
            });



        }
    }




    [ContentProperty(nameof(MyItems))]
    public class ItemsThumb : BaseThumb
    {
        static ItemsThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsThumb), new FrameworkPropertyMetadata(typeof(ItemsThumb)));
        }
        public ItemsThumb()
        {
            DataContext = this;
            MyItems = [];
            Loaded += CustomItemsThumb_Loaded; MyData = new DataText();
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
                    //SetBinding(Thumb.WidthProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.WidthProperty) });

                    _ = SetBinding(WidthProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualWidthProperty) });

                    _ = SetBinding(HeightProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualHeightProperty) });

                    //SetBinding(Thumb.BackgroundProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.BackgroundProperty) });

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


        public ObservableCollection<UIElement> MyItems
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<UIElement>), typeof(ItemsThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }


    public class TextThumb : BaseThumb
    {
        static TextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextThumb), new FrameworkPropertyMetadata(typeof(TextThumb)));
        }

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
        public TextThumb()
        {
            DataContext = this;
        }
    }
}
