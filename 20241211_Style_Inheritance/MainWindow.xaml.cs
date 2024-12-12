using System.Windows;
using System.Windows.Controls.Primitives;

//参照したところ
//【C#-WPF】XAMLの記述に便利なResourcesとStyleについて - 業務のためのC#・C言語・C++学習
//https://gaishiengineer.hatenablog.com/entry/2022/09/18/131611

namespace _20241211_Style_Inheritance
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is KisoThumb t)
            {
                t.MyLeft += e.HorizontalChange;
                t.MyTop += e.VerticalChange;
                e.Handled = true;
            }
        }

    }
}