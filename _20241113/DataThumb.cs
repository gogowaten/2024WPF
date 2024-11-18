using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _20241113
{
    public enum TType { None = 0, TextBlock, Rectangle, }


    public class DataThumb : DependencyObject, IExtensibleDataObject, INotifyPropertyChanged
    {
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

        #region コンストラクタ
        //c# - Datacontract exception. Cannot be serialized - Stack Overflow
        //        https://stackoverflow.com/questions/10077121/datacontract-exception-cannot-be-serialized

        //DependencyObjectを継承したクラスのシリアル化には、
        //引数のないコンストラクタが必要
        public DataThumb() { throw new NotImplementedException("このクラスのnewにはTTypeの引数が必要"); }
        public DataThumb(TType type)
        {
            switch (type)
            {
                case TType.None:
                    break;
                case TType.TextBlock:
                    break;
                case TType.Rectangle:
                    break;
                default:
                    break;
            }
        }

        #endregion コンストラクタ
    }
    /*
     データタイプごとに分ける
     DataThumb
        共通
            X
            Y
        TextBlock
            Text
            Background
        Rectangle
            Fill
            Background
        Image
            Source
        ...

    それとも一括ごちゃまぜ
    DataThumb
        X
        Y
        Text
        Background
        Fill
        Source
        ...
     */
}
