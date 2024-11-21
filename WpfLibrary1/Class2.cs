using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfLibrary1
{
    internal class Class2
    {
    }
    public class TDataBase : DependencyObject, INotifyPropertyChanged, IExtensibleDataObject
    {
        public TDataBase() { }

        #region インターフェースとの関連
        //IExtensibleDataObjectとの関連、Dataの互換性維持のため？
        public ExtensionDataObject? ExtensionData { get; set; }

        //INotifyPropertyChangedとの関連、依存関係プロパティ
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void SetProperty<T>(ref T oldValue, ref T newValue,
            [System.Runtime.CompilerServices.CallerMemberName] string? name = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return;
            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion インターフェースとの関連

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(double), typeof(TDataBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(double), typeof(TDataBase),
                new FrameworkPropertyMetadata(0.0,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }

    public class TextBlockData : TDataBase
    {

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBlockData),
                new FrameworkPropertyMetadata("",
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TextBlockData()
        {
            ControlTemplate ct = new(typeof(CanvasThumb));
            
        }
    }


    public class CanvasThumb : Thumb
    {
        public CanvasThumb() { }
    }

}
