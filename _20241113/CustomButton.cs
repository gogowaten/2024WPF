using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

//カスタム ルーティング イベントを作成する方法 - WPF .NET | Microsoft Learn
//https://learn.microsoft.com/ja-jp/dotnet/desktop/wpf/events/how-to-create-a-custom-routed-event?view=netdesktop-9.0

namespace _20241113
{
    internal class CustomButton : Button
    {
        // バブル ルーティング戦略を使用してカスタム ルーティング イベントを登録します。
        public static readonly RoutedEvent ConditionalClickEvent = EventManager.RegisterRoutedEvent(
            name: "ConditionalClick",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(CustomButton));

        // イベント ハンドラーを割り当てるための CLR アクセサーを提供します。
        public event RoutedEventHandler ConditionalClick
        {
            add { AddHandler(ConditionalClickEvent, value); }
            remove { RemoveHandler(ConditionalClickEvent, value); }
        }


        void RaiseCustomRoutedEvent()
        {
            // RoutedEventArgs インスタンスを作成します。
            RoutedEventArgs routedEventArgs = new(routedEvent: ConditionalClickEvent);
            // イベントを発生させ、要素ツリーを介してバブルアップします。
            RaiseEvent(routedEventArgs);
        }

        // デモの目的で、Click イベントをトリガーとして使用します。
        protected override void OnClick()
        {
            // Click イベントと組み合わされた条件によって、ConditionalClick イベントがトリガーされます。
            if (DateTime.Now > new DateTime())
                RaiseCustomRoutedEvent();
            // 基本クラスの OnClick() メソッドを呼び出して、Click イベント サブスクライバーに通知します。
            base.OnClick();
        }
    }
}
