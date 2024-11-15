using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace _20241113
{
    class Class2
    {
    }

    public class SleepClass
    {
        public event EventHandler? Sleep;
        public void Start()
        {
            Thread.Sleep(1000);
            Sleep?.Invoke(this, EventArgs.Empty);//イベント発生

        }
    }

    class MyClickEventArgs(int x, int y) : EventArgs
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
    }

    class MyButton
    {
        public delegate void MyEventHandler(EventArgs args);
        public MyEventHandler? ClickHnadler;
        public void Click()
        {
            int x = 10;
            int y = 100;
            if (ClickHnadler != null)
            {
                var args = new MyClickEventArgs(x, y);
                ClickHnadler(args);
            }
        }
    }


    public class TThumb : Thumb
    {
        public TThumb()
        {
            DragDelta += TThumb_DragDelta;
        }

        private void TThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void TThumb_DragDelta2(object sender, DragEventArgs e)
        {
            var tt = sender as TThumb;
        }

    }


}
