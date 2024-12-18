﻿using System.Text;
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

namespace _20241218
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int MyScale
        {
            get { return (int)GetValue(MyScaleProperty); }
            set { SetValue(MyScaleProperty, value); }
        }
        public static readonly DependencyProperty MyScaleProperty =
            DependencyProperty.Register(nameof(MyScale), typeof(int), typeof(MainWindow), new PropertyMetadata(1));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyComboScaleMode.ItemsSource = Enum.GetValues(typeof(BitmapScalingMode));
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                Canvas.SetLeft(t, Canvas.GetLeft(t) + e.HorizontalChange);
                Canvas.SetTop(t, Canvas.GetTop(t) + e.VerticalChange);
                e.Handled = true;
            }
        }

        private void Thumb_DragDelta2(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb t)
            {
                double hc = e.HorizontalChange;

                if (hc > 0)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc + 0.5));
                }
                else if (hc < 0)
                {
                    Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc - 0.5));
                }

                double vc = e.VerticalChange;
                if (vc > 0)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc + 0.5));
                }
                else if (vc < 0)
                {
                    Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc - 0.5));
                }
                e.Handled = true;
            }
            //if (sender is Thumb t)
            //{
            //    double hc = e.HorizontalChange;

            //    if (hc > 0)
            //    {
            //        //Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc + 0.5));
            //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale + 0.5));
            //    }
            //    else if (hc < 0)
            //    {
            //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale - 0.5));
            //    }

            //    double vc = e.VerticalChange;
            //    if (vc > 0)
            //    {
            //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale + 0.5));
            //    }
            //    else if (vc < 0)
            //    {
            //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale - 0.5));
            //    }
            //    e.Handled = true;
            //}

            //if (sender is Thumb t)
            //{
            //    double hc = e.HorizontalChange;

            //    if (hc > 0)
            //    {
            //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale + 0.5) * MyScale);
            //    }
            //    else if (hc < 0)
            //    {
            //        Canvas.SetLeft(t, Canvas.GetLeft(t) + (int)(hc / MyScale - 0.5) * MyScale);
            //    }

            //    double vc = e.VerticalChange;
            //    if (vc > 0)
            //    {
            //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale + 0.5) * MyScale);
            //    }
            //    else if (vc < 0)
            //    {
            //        Canvas.SetTop(t, Canvas.GetTop(t) + (int)(vc / MyScale - 0.5) * MyScale);
            //    }
            //    e.Handled = true;
            //}
        }

        private void Button_Click_100(object sender, RoutedEventArgs e)
        {
            MyScale = 1;
        }

        private void Button_Click_200(object sender, RoutedEventArgs e)
        {
            MyScale = 2;
        }

        private void Button_Click_1000(object sender, RoutedEventArgs e)
        {
            MyScale = 10;
        }

        private void Button_Click_300(object sender, RoutedEventArgs e)
        {
            MyScale = 3;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {

        }

        private void Button_Click_Rounding(object sender, RoutedEventArgs e)
        {
            MyCanvas.UseLayoutRounding = !MyCanvas.UseLayoutRounding;
        }


        private void Button_Click_EdgeMode(object sender, RoutedEventArgs e)
        {
            if (RenderOptions.GetEdgeMode(MyCanvas) == EdgeMode.Unspecified)
            {
                RenderOptions.SetEdgeMode(MyCanvas, EdgeMode.Aliased);
            }
            else
            {
                RenderOptions.SetEdgeMode(MyCanvas, EdgeMode.Unspecified);
            }
        }
    }
}