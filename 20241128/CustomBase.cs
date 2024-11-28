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

namespace _20241128
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241128"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241128;assembly=_20241128"
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
    public abstract class CustomBase : Thumb
    {
        static CustomBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomBase), new FrameworkPropertyMetadata(typeof(CustomBase)));
        }
        #region 依存関係プロパティ

        public double MyX
        {
            get { return (double)GetValue(MyXProperty); }
            set { SetValue(MyXProperty, value); }
        }
        public static readonly DependencyProperty MyXProperty =
            DependencyProperty.Register(nameof(MyX), typeof(double), typeof(CustomBase),
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
            DependencyProperty.Register(nameof(MyY), typeof(double), typeof(CustomBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion 依存関係プロパティ
        public CustomBase()
        {
            SetBinding(Canvas.LeftProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyXProperty)
            });
            SetBinding(Canvas.TopProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(MyYProperty)
            });


            //  SetBinding(AutoResizeCanvas.LeftProperty, new Binding()
            //{
            //    Source = this,
            //    Path = new PropertyPath(MyXProperty)
            //});
            //SetBinding(AutoResizeCanvas.TopProperty, new Binding()
            //{
            //    Source = this,
            //    Path = new PropertyPath(MyYProperty)
            //});

        }
    }




    [ContentProperty(nameof(MyItems))]
    public class CustomItemsThumb : CustomBase
    {
        static CustomItemsThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomItemsThumb), new FrameworkPropertyMetadata(typeof(CustomItemsThumb)));
        }
        public CustomItemsThumb()
        {
            DataContext = this;
            MyItems = [];
            //Background = Brushes.Red;
            Loaded += CustomItemsThumb_Loaded;
        }

        private static Canvas? GetCanvas(DependencyObject d)
        {
            if (d is Canvas canvas) return canvas;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                //Canvas? re = GetCanvas(VisualTreeHelper.GetChild(d, i));
                //if (re != null)
                //{
                //    return re;
                //}

                return GetCanvas(VisualTreeHelper.GetChild(d, i));
            }
            return null;
        }
        private void CustomItemsThumb_Loaded(object sender, RoutedEventArgs e)
        {

            //ControlTemplate tem = Template;
            //object can2 = tem.FindName("MyTemp", this);//itemscontrol
            //if (can2 is DependencyObject dp)
            //{
            //    if (GetCanvas(dp) is Canvas ccv)
            //    {
            //        var ccvn = ccv.Name;
            //    }
            //}
            object temp = Template.FindName("MyTemp", this);
            if (temp is DependencyObject d)
            {
                if (GetCanvas(d) is Canvas tpcan)
                {
                    //SetBinding(Thumb.WidthProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.WidthProperty) });

                    SetBinding(Thumb.WidthProperty, new Binding() { Source = tpcan, Path = new PropertyPath(Canvas.ActualWidthProperty) });

                    SetBinding(Thumb.HeightProperty, new Binding() { Source = tpcan, Path = new PropertyPath(Canvas.ActualHeightProperty) });

                    //SetBinding(Thumb.BackgroundProperty,new Binding() { Source =tpcan,Path=new PropertyPath(Canvas.BackgroundProperty) });

                }
            }

            //if (can2 is ItemsControl ic)
            //{
            //    //int ccount = VisualTreeHelper.GetChildrenCount(ic);
            //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(ic); i++)
            //    {

            //        DependencyObject icc = VisualTreeHelper.GetChild(ic, i);
            //        if (icc is Border border)
            //        {
            //            for (int j = 0; j < VisualTreeHelper.GetChildrenCount(border); i++)
            //            {
            //                DependencyObject jcc = VisualTreeHelper.GetChild(border, j);
            //                if (jcc is ItemsPresenter itemP)
            //                {
            //                    for (int k = 0; k < VisualTreeHelper.GetChildrenCount(itemP); k++)
            //                    {
            //                        var kcc = VisualTreeHelper.GetChild(itemP, k);
            //                        if (kcc is Canvas canvas)
            //                        {
            //                            string cc = canvas.Name;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    var can3 = ic.Template.FindName("MyCanvas", ic);
            //    ItemsPanelTemplate ip = ic.ItemsPanel;
            //    TemplateContent iptemp = ip.Template;
            //    var c1 = VisualTreeHelper.GetChild(ic, 0);//border
            //    var c2 = VisualTreeHelper.GetChild(c1, 0);//itemsPresenter
            //    var c3 = VisualTreeHelper.GetChild(c2, 0);//canvas
            //    ItemsPresenter ipp = new();

            //}
            //if(tem is FrameworkElement elm)
            //{

            //}
            //var tt = VisualTreeHelper.GetChild(tem, 0);
        }

        public ObservableCollection<UIElement> MyItems
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<UIElement>), typeof(CustomItemsThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }


    public class CustomTextThumb : CustomBase
    {
        static CustomTextThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextThumb), new FrameworkPropertyMetadata(typeof(CustomTextThumb)));
        }

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(CustomTextThumb),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public CustomTextThumb()
        {
            DataContext = this;
        }
    }

}
