﻿using System.Windows;
using System.Windows.Controls.Primitives;

//WPF、子要素のドラッグ移動で自動リサイズするCanvasに、スクロールバーを表示したときの不自然な挙動を改善 - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2024/12/17/153512

namespace _20241217_ScrollViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;
            }
        }

        ////失敗
        ////スクロールオフセットでスクロール位置を固定はできない
        //private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
        //{
        //    if (sender is KisoThumb t)
        //    {
        //        var scrollWidth = MyScrollV.ActualWidth;
        //        var scrollHOffset = MyScrollV.HorizontalOffset;
        //        var scrollRight = MyScrollV.ActualWidth + MyScrollV.HorizontalOffset;
        //        double parentLeft = t.MyParentThumb?.MyLeft ?? 0;
        //        var thumbRight = parentLeft + t.MyLeft + t.ActualWidth;

        //        t.MyLeft += e.HorizontalChange;
        //        t.MyTop += e.VerticalChange;


        //        var thumbRight2 = parentLeft + t.MyLeft + t.ActualWidth;
        //        //固定
        //        if (scrollRight > thumbRight2)
        //        {
        //            MyScrollV.ScrollToHorizontalOffset(scrollHOffset);
        //        }

        //        e.Handled = true;
        //    }
        //}


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


}