using System.Windows;

namespace _20241228_DataTemplateSelector_de_Thumb
{
    /// <summary>
    /// データタイプの識別用列挙体
    /// </summary>
    public enum ThumbType { None = 0, Text, Ellipse, Rect }

    /// <summary>
    /// データ用
    /// 殆どを依存関係プロパティにしているのは、値を更新したときにBinding先に通知するため
    /// </summary>
    public class MyData : DependencyObject
    {
        public ThumbType Type { get; }

        public MyData(ThumbType type)
        {
            this.Type = type;
        }

        #region 依存関係プロパティ

        public string MyText
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }
        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register(nameof(MyText), typeof(string), typeof(MyData), new PropertyMetadata(string.Empty));


        public double MyLeft
        {
            get { return (double)GetValue(MyLeftProperty); }
            set { SetValue(MyLeftProperty, value); }
        }
        public static readonly DependencyProperty MyLeftProperty =
            DependencyProperty.Register(nameof(MyLeft), typeof(double), typeof(MyData), new PropertyMetadata(0.0));

        public double MyTop
        {
            get { return (double)GetValue(MyTopProperty); }
            set { SetValue(MyTopProperty, value); }
        }
        public static readonly DependencyProperty MyTopProperty =
            DependencyProperty.Register(nameof(MyTop), typeof(double), typeof(MyData), new PropertyMetadata(0.0));

        public double MyVolume
        {
            get { return (double)GetValue(MyVolumeProperty); }
            set { SetValue(MyVolumeProperty, value); }
        }
        public static readonly DependencyProperty MyVolumeProperty =
            DependencyProperty.Register(nameof(MyVolume), typeof(double), typeof(MyData), new PropertyMetadata(30.0));

        #endregion 依存関係プロパティ
    }


}
