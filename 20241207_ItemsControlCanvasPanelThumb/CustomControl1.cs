﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace _20241207_ItemsControlCanvasPanelThumb
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241207_ItemsControlCanvasPanelThumb"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241207_ItemsControlCanvasPanelThumb;assembly=_20241207_ItemsControlCanvasPanelThumb"
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
    public class ItemsControlThumb : Thumb
    {

        public ObservableCollection<FrameworkElement> MyItems
        {
            get { return (ObservableCollection<FrameworkElement>)GetValue(MyItemsProperty); }
            set { SetValue(MyItemsProperty, value); }
        }
        public static readonly DependencyProperty MyItemsProperty =
            DependencyProperty.Register(nameof(MyItems), typeof(ObservableCollection<FrameworkElement>), typeof(ItemsControlThumb), new PropertyMetadata(null));

        static ItemsControlThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsControlThumb), new FrameworkPropertyMetadata(typeof(ItemsControlThumb)));
        }
        public ItemsControlThumb()
        {
            DataContext = this;
            MyItems = [];
        }

    }
}
