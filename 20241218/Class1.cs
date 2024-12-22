using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace _20241218
{


    public class MyConverterVisible : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b == true) { return Visibility.Visible; }
            else { return Visibility.Collapsed; }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
