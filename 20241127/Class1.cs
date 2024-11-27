﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//.net - WPF: キャンバスのサイズを自動変更するには? - Stack Overflow
//https://stackoverflow.com/questions/855334/wpf-how-to-make-canvas-auto-resize

namespace _20241127
{
    class Class1
    {
    }

    public class AutoResizeCanvas : Panel
    {

        private static void OnLayoutParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            while (d != null)
            {
                if (d is AutoResizeCanvas canvas)
                {
                    canvas.InvalidateMeasure();
                    return;
                }
                d = VisualTreeHelper.GetParent(d);
            }
        }

        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }

        public static readonly DependencyProperty LeftProperty =
           DependencyProperty.RegisterAttached("Left", typeof(double),
           typeof(AutoResizeCanvas),
           new FrameworkPropertyMetadata(0.0, OnLayoutParameterChanged));

        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached("Top",
                typeof(double), typeof(AutoResizeCanvas),
                new FrameworkPropertyMetadata(0.0, OnLayoutParameterChanged));


        public double MinimumWidth
        {
            get { return (double)GetValue(MinimumWidthProperty); }
            set { SetValue(MinimumWidthProperty, value); }
        }
        public static readonly DependencyProperty MinimumWidthProperty =
            DependencyProperty.Register(nameof(MinimumWidth), typeof(double), typeof(AutoResizeCanvas),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));


        public double MinimumHeight
        {
            get { return (double)GetValue(MinimumHeightProperty); }
            set { SetValue(MinimumHeightProperty, value); }
        }
        public static readonly DependencyProperty MinimumHeightProperty =
            DependencyProperty.Register(nameof(MinimumHeight), typeof(double), typeof(AutoResizeCanvas),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));




        private static void GetRequestedBounds(FrameworkElement el, out Rect bounds, out Rect marginBounds)
        {
            double left = 0, top = 0;
            Thickness margin = new();
            DependencyObject content = el;
            if (el is ContentPresenter)
            {
                content = VisualTreeHelper.GetChild(el, 0);
            }
            if (content != null)
            {
                left = GetLeft(content);
                top = GetTop(content);
                if (content is FrameworkElement element)
                {
                    margin = element.Margin;
                }
            }
            if (double.IsNaN(left)) left = 0;
            if (double.IsNaN(top)) top = 0;
            Size size = el.DesiredSize;
            bounds = new Rect(left + margin.Left, top + margin.Top, size.Width, size.Height);
            marginBounds = new Rect(left, top, size.Width + margin.Left + margin.Right, size.Height + margin.Top + margin.Bottom);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size availabeSize = new(double.MaxValue, double.MaxValue);
            double requestedWidth = MinimumWidth;
            double requestedHeight = MinimumHeight;
            foreach (var child in base.InternalChildren)
            {
                if (child is FrameworkElement el)
                {
                    el.Measure(availabeSize);
                    GetRequestedBounds(el, out Rect bounds, out Rect margin);
                    requestedWidth = Math.Max(requestedWidth, margin.Right);
                    requestedHeight = Math.Max(requestedHeight, margin.Bottom);
                }
            }
            return new Size(requestedWidth, requestedHeight);
            //return base.MeasureOverride(constraint);
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            //Size availableSize = new(double.MaxValue, double.MaxValue);
            double requestedWidth = MinimumWidth;
            double requestedHeight = MinimumHeight;
            foreach (var child in base.InternalChildren)
            {
                if (child is FrameworkElement el)
                {
                    GetRequestedBounds(el, out Rect bounds, out Rect marginBounds);
                    requestedWidth = Math.Max(marginBounds.Right, requestedWidth);
                    requestedHeight = Math.Max(marginBounds.Bottom, requestedHeight);
                    el.Arrange(bounds);
                }
            }
            return new Size(requestedWidth, requestedHeight);
            //return base.ArrangeOverride(finalSize);
        }




    }


}
