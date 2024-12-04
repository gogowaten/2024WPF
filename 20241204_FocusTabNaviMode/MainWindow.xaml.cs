using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _20241204_FocusTabNaviMode
{
    public partial class MainWindow : Window
    {
        public Array MyEnumArray { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyEnumArray = Enum.GetValues(typeof(KeyboardNavigationMode));
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
            }
        }

    }
}