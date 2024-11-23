using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;


//カスタムコントロール(CustomControl)を作る - tera1707’s blog
//https://tera1707.com/entry/2023/06/16/135543

namespace _20241123_CustomControl
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241123_CustomControl"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_20241123_CustomControl;assembly=_20241123_CustomControl"
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
    ///     <MyNamespace:MySignal/>
    ///
    /// </summary>
    public class MySignal : Control
    {

        [Category("Signal")]
        public TimeSpan IntervalTime
        {
            get => (TimeSpan)GetValue(IntervalTimeProperty);
            set => SetValue(IntervalTimeProperty, value);
        }

        public static readonly DependencyProperty IntervalTimeProperty =
            DependencyProperty.Register("IntervalTime", typeof(TimeSpan), typeof(MySignal), new PropertyMetadata(TimeSpan.FromSeconds(1)));

        private Grid? signalGreen;
        private Grid? signalRed;
        private Button? startButton;
        private DispatcherTimer signalIntervalTimer = new DispatcherTimer();
        private bool? IsGoing = null;//null=停止、false=赤、true=緑


        public MySignal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MySignal), new FrameworkPropertyMetadata(typeof(MySignal)));
            signalIntervalTimer.Tick += SignalIntervalTime_Tick;
        }

        private void SignalIntervalTime_Tick(object? sender, EventArgs e)
        {
            if (IsGoing == false)
            {
                signalGreen.Background = Brushes.Green;
                signalRed.Background = Brushes.Gray;
                IsGoing = true;
            }
            else
            {
                signalGreen.Background = Brushes.Gray;
                signalRed.Background = Brushes.Red;
                IsGoing = false;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (startButton is not null) startButton.Click -= StartButton_Click;

            signalGreen = this.GetTemplateChild("PART_SignalGreen") as Grid;
            signalRed = this.GetTemplateChild("PART_SignalRed") as Grid;
            startButton = this.GetTemplateChild("PART_StartButton") as Button;

            if (startButton is not null)
                startButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsGoing == null)
            {
                signalIntervalTimer.Interval = IntervalTime;
                signalIntervalTimer.Start();
                IsGoing = false;
            }
            else
            {
                signalIntervalTimer.Stop();
                IsGoing = null;
            }

            if (signalGreen is not null) signalGreen.Background = Brushes.Gray;
            if (signalRed is not null) signalRed.Background = Brushes.Gray;
        }
    }
}
