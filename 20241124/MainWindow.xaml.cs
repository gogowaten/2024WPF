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

namespace _20241124
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddEllipseThumb();
        }

        private void AddEllipseThumb()
        {
            MyCanvas.Children.Add(new CustomControl2() { Width = 50, Height = 50, Fill = Brushes.Red, Background = Brushes.MediumPurple });
            //var aa = new CustomControl2();


        }
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is UIElement element)
            {
                Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
            }
        }


    }
}