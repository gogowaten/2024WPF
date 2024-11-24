using System;
using System.Collections.Generic;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20241124
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241124"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241124;assembly=_20241124"
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
    public class CustomControl1 : Thumb
    {

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomControl1),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));

            DataContext = this;
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    Thumb tt = this.GetTemplateChild("PART_Thumb") as Thumb;
        //}
    }

    //public class CustomControl2 : Thumb
    //{

    //    public Brush Fill
    //    {
    //        get { return (Brush)GetValue(FillProperty); }
    //        set { SetValue(FillProperty, value); }
    //    }
    //    public static readonly DependencyProperty FillProperty =
    //        DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(CustomControl2),
    //            new FrameworkPropertyMetadata(Brushes.Beige,
    //                FrameworkPropertyMetadataOptions.AffectsRender |
    //                FrameworkPropertyMetadataOptions.AffectsMeasure |
    //                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    //    public CustomControl2()
    //    {
    //        DefaultStyleKeyProperty.OverrideMetadata(
    //            typeof(CustomControl2), new FrameworkPropertyMetadata(typeof(CustomControl2)));

    //        DataContext = this;

    //    }
    //}





}
