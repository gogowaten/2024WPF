using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace _20241123
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241123"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241123;assembly=_20241123"
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
    /// 

    //[DefaultProperty(nameof(MyProperty))]
    [ContentProperty(nameof(MyElements))]
    public class CustomControl1 : Thumb
    {

        public ObservableCollection<FrameworkElement> MyElements
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(MyElementsProperty); }
            set { SetValue(MyElementsProperty, value); }
        }
        public static readonly DependencyProperty MyElementsProperty =
            DependencyProperty.Register(nameof(MyElements), typeof(ObservableCollection<FrameworkElement>), typeof(CustomControl1),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public ObservableCollection<Item> MyItems
        {
            get { return (ObservableCollection<Item>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<Item>), typeof(CustomControl1),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public Thumb MyThumb;

        public CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));

            MyElements = [];
            MyItems = [];
            MyItems.Add(new TextBlockItem() { X = 20, Y = 10, Text = "Test" });
            this.DataContext = MyItems;
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);

        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
            Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MyThumb = this.GetTemplateChild("PART_Thumb") as Thumb;
            MyThumb.DragDelta += Thumb_DragDelta;
            //MyCanvas = this.GetTemplateChild("PART_Canvas") as Canvas;
            //MyItems = this.Template.FindName("PART_ItemsControl", this) as ItemsControl;
            //MyItems.SetBinding(ItemsControl.ItemsSourceProperty,
            //new Binding() { Path = new PropertyPath(MyElementsProperty), Source = this });
        }

        public void AddElement(FrameworkElement element)
        {

        }

    }

    public class TData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class TextData : TData
    {
        public string Text { get; set; } = string.Empty;

    }

}
