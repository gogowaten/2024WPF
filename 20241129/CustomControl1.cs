﻿using System;
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

    public enum ThumbType { None = 0, Items, Text, Rect }
    //public interface IData
    //{
    //    DataMoto? MyData { get; set; }
    //    abstract ThumbType Type { get; set; }
    //}

    public abstract class BaseThumb : Thumb
    {
        public abstract ThumbType Type { get; set; }
        public abstract DataMoto MyData { get; set; }
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




    [ContentProperty(nameof(MyChildren))]
    public class ItemsThumb : BaseThumb
    {
        public override ThumbType Type { get; set; }
        public override DataMoto MyData { get; set; }
        static ItemsThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsThumb), new FrameworkPropertyMetadata(typeof(ItemsThumb)));
        }
        public ItemsThumb()
        {
            Type = ThumbType.Items;
            MyData = new DataItems();
            DataContext = this;
            MyChildren = [];
            Loaded += CustomItemsThumb_Loaded;
            MyChildren.CollectionChanged += MyChildren_CollectionChanged;
        }

        private void MyChildren_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var neko = e.NewItems[0];
            var tako = neko as BaseThumb;
            var inu = tako.MyData;
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
                    _ = SetBinding(WidthProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualWidthProperty) });

                    _ = SetBinding(HeightProperty, new Binding() { Source = templateCanvas, Path = new PropertyPath(ActualHeightProperty) });
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


        public ObservableCollection<UIElement> MyChildren
        {
            get { return (ObservableCollection<UIElement>)GetValue(MyChildrenProperty); }
            set { SetValue(MyChildrenProperty, value); }
        }
        public static readonly DependencyProperty MyChildrenProperty =
            DependencyProperty.Register(nameof(MyChildren), typeof(ObservableCollection<UIElement>), typeof(ItemsThumb),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    }


    public class TextThumb : BaseThumb
    {
        public override ThumbType Type { get; set; }
        public override DataMoto MyData { get; set; }
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
            MyData = new DataText();
            //SetBinding(MyTextProperty, new Binding() { Source = MyData, Path = new PropertyPath(MyData.MyText) });
            Binding b = new() { Source = this, Path = new PropertyPath(MyTextProperty) };
            BindingOperations.SetBinding(MyData, DataText.MyTextProperty, b);
        }
    }
}