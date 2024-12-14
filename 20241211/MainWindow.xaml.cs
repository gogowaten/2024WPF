﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

//スタイルの引き継ぎ
//[WPF/xaml]BasedOnを使って元のstyleを受け継ぐ #C# - Qiita
//https://qiita.com/tera1707/items/3c4f598c5d022e4987a2

namespace _20241211
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

        private void KisoThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {

            if (sender is KisoThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;
                GotFocus += MainWindow_GotFocus;
                GotKeyboardFocus += MainWindow_GotKeyboardFocus;
                PreviewGotKeyboardFocus += MainWindow_PreviewGotKeyboardFocus;
                //GotFocus += (a, b) => { if (a is KisoThumb kiso) MyFocus.Text = "GotFocus = " + kiso.MyText; };
                //GotKeyboardFocus += (a, b) => { if (sender is KisoThumb kiso) MyKeyFocus.Text = "GotKeyFocus = " + kiso.MyText; };
                //PreviewGotKeyboardFocus += (a, b) => { if (sender is KisoThumb kiso) { MyPreKeyFocus.Text = "PreGotKeyFocus = " + kiso.MyText; } };
            }



        }

        private void MainWindow_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is KisoThumb kiso) MyKeyFocus.Text = "GotKeyFocus = " + kiso.MyText;

        }

        private void MainWindow_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is KisoThumb kiso) MyPreKeyFocus.Text = "PreGotKeyFocus = " + kiso.MyText;
        }

        private void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is KisoThumb kiso) MyFocus.Text = "GotFocus = " + kiso.MyText;
        }

        private void KisoThumb_DragDeltaSleep1(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                if (t.MyLeft + t.ActualWidth > MyScroll.ActualWidth)
                {
                    MyScroll.ScrollToHorizontalOffset(MyScroll.HorizontalOffset + e.HorizontalChange);
                    t.MyLeft += e.HorizontalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyLeft += e.HorizontalChange;
                }

                if (t.MyTop + t.ActualHeight > MyScroll.ActualHeight)
                {
                    MyScroll.ScrollToVerticalOffset(MyScroll.VerticalOffset + e.VerticalChange);
                    t.MyTop += e.VerticalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyTop += e.VerticalChange;
                }
                e.Handled = true;
            }

        }
        private void KisoThumb_DragDeltaSleep11(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                if (t.MyLeft + t.ActualWidth > MyScroll.ActualWidth)
                {
                    MyScroll.ScrollToHorizontalOffset(MyScroll.HorizontalOffset + e.HorizontalChange);
                    t.MyLeft += e.HorizontalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyLeft += e.HorizontalChange;
                }

                if (t.MyTop + t.ActualHeight > MyScroll.ActualHeight)
                {
                    MyScroll.ScrollToVerticalOffset(MyScroll.VerticalOffset + e.VerticalChange);
                    t.MyTop += e.VerticalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyTop += e.VerticalChange;
                }
                e.Handled = true;
            }

        }
        private void KisoThumb_DragDeltaSleep2(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                if (t.MyLeft + t.ActualWidth > MyScroll.ActualWidth)
                {
                    MyScroll.ScrollToHorizontalOffset(MyScroll.HorizontalOffset + e.HorizontalChange);
                    t.MyLeft += e.HorizontalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyLeft += e.HorizontalChange;
                }

                if (t.MyTop + t.ActualHeight > MyScroll.ActualHeight)
                {
                    MyScroll.ScrollToVerticalOffset(MyScroll.VerticalOffset + e.VerticalChange);
                    t.MyTop += e.VerticalChange;
                    Thread.Sleep(100);
                }
                else
                {
                    t.MyTop += e.VerticalChange;
                }



                if (t.MyLeft < 0 || t.MyTop < 0)
                {
                    Thread.Sleep(100);
                    t.MyParentThumb?.ReLayout();
                    MyRootGroup.MyLeft = 0;
                    MyRootGroup.MyTop = 0;
                }
                //else
                //{
                //    if (MyScroll.VerticalScrollBarVisibility == ScrollBarVisibility.Visible)
                //    {
                //        MyScroll.ScrollToVerticalOffset(MyScroll.VerticalOffset + e.VerticalChange);
                //    t.MyParentThumb?.ReLayout();
                //    }
                //}

                t.MyParentThumb?.ReLayout();
                e.Handled = true;
            }

        }


        private void KisoThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.MyParentThumb?.ReLayout();

                MyRootGroup.MyLeft = 0;
                MyRootGroup.MyTop = 0;
                //t.Focus();

                e.Handled = true;
            }
        }

        private void KisoThumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                if (e.Key == Key.Left)
                {
                    t.MyLeft -= 10;
                    var hor = MyScroll.HorizontalOffset;
                    var scw = MyScroll.ActualWidth;
                    var rig = t.MyLeft + t.ActualWidth;
                    if (t.MyLeft < 0)
                    {
                        t.MyParentThumb?.ReLayout();
                    }
                    e.Handled = true;
                }
                else if (e.Key == Key.Right)
                {
                    t.MyLeft += 10;
                    //Thumbの右端が見切れたら、その分をスクロールさせる
                    var thumbRight = t.MyLeft + t.ActualWidth;
                    var scrollRight = MyScroll.ActualWidth + MyScroll.HorizontalOffset;
                    if (thumbRight > scrollRight)
                    {
                        MyScroll.ScrollToHorizontalOffset(MyScroll.HorizontalOffset + (thumbRight - scrollRight));
                    }
                    e.Handled = true;
                }
            }
        }


        //private void KisoThumb_DragDelta2(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        //{
        //    if (sender is KisoThumb t)
        //    {
        //        t.MyLeft += e.HorizontalChange;
        //        t.MyTop += e.VerticalChange;
        //        e.Handled = true;
        //    }
        //}

        //private void KisoThumb_DragCompleted2(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        //{
        //    if (sender is KisoThumb t)
        //    {
        //        t.MyParentThumb2?.ReLayout2();

        //        MyRootGroup.MyLeft = 0;
        //        MyRootGroup.MyTop = 0;

        //        e.Handled = true;
        //    }
        //}
    }
}