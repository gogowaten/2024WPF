using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


//WPF、マウスでTextBoxのサイズ変更するのにAdorner(装飾者)を使ってみた - 午後わてんのブログ
//https://gogowaten.hatenablog.com/entry/2023/03/05/142526

namespace _20241224
{
    public class EzAdorner : Adorner
    {
        private readonly Thumb thumb;
        private readonly VisualCollection visuals;
        private readonly FrameworkElement target;
        public Rectangle Waku;
        public Ellipse LeftUp;
        public Ellipse RightBottom;

        public EzAdorner(FrameworkElement adornedElement) : base(adornedElement)
        {
            target = adornedElement;
            visuals = new(parent: this);
            thumb = new() { Cursor = Cursors.SizeAll, Height = 20, Width = 20, Opacity = 0.5, Background = Brushes.DodgerBlue };
            thumb.DragDelta += Thumb_DragDelta;
            visuals.Add(thumb);

            Waku = new() { Stroke = Brushes.DodgerBlue, StrokeThickness = 1, Name = "waku", };
            visuals.Add(Waku);

            LeftUp = new() { Stroke = Brushes.Red, StrokeThickness = 1, Width = 10, Height = 10, Name = "左上" };
            visuals.Add(LeftUp);
            RightBottom = new() { Stroke = Brushes.Red, StrokeThickness = 1, Width = 10, Height = 10, Name = "右上" };
            visuals.Add(RightBottom);



            target.Width = target.DesiredSize.Width;
            target.Height = target.DesiredSize.Height;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            target.Width = Math.Max(target.ActualWidth + e.HorizontalChange, thumb.Width);
            target.Height = Math.Max(target.ActualHeight + e.VerticalChange, thumb.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //Thumbの表示位置を対象要素の右下へ設定
            //thumb.Arrange(new Rect(
            //    target.Width / 2,
            //    target.Height / 2,
            //    target.Width,
            //    target.Height));
            thumb.Arrange(new Rect(
                target.ActualWidth / 2,
                target.ActualHeight / 2,
                target.ActualWidth,
                target.ActualHeight));//これでも同じだった

            //Waku.Arrange(new Rect(-target.Width / 2, -target.Height / 2, 200, 100));
            //Waku.Arrange(new Rect(0,0, 100, 100));
            //Waku.Arrange(new Rect(0,0, 50, 50));//ぴったり重なる
            //Waku.Arrange(new Rect(0,0, target.Width, target.Height));//ぴったり重なる
            //Waku.Arrange(new Rect(target.Width, target.Height, target.Width, target.Height));
            Waku.Arrange(new Rect(-10, -10, target.DesiredSize.Width + 20, target.DesiredSize.Height + 20));//

            LeftUp.Arrange(new Rect(-target.Width / 2 - 20, -target.Height / 2 - 20, target.ActualWidth + 20, target.ActualHeight + 20));//10ピクセル左上に表示
            //LeftUp.Arrange(new Rect(-target.Width / 2, -target.Height / 2, target.ActualWidth, target.ActualHeight));//左上に表示

            RightBottom.Arrange(new Rect(target.Width / 2, target.Height / 2, target.ActualWidth + 20, target.ActualHeight + 20));//10ピクセル右下に表示

            return base.ArrangeOverride(finalSize);
        }

        //protected override int VisualChildrenCount => base.VisualChildrenCount;
        protected override int VisualChildrenCount => visuals.Count;

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
            //return base.GetVisualChild(index);
        }

    }
}
