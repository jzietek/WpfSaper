using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfSaper.Converters
{
    public class BombsAroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int v = (int) value;
            return (v == 0 ? string.Empty : v.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
