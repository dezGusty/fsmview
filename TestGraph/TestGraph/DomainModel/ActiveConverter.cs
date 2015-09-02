using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TestGraph.DomainModel
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class ActiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (bool)value;
            if (state)
            {
                return new SolidColorBrush(Colors.WhiteSmoke);
            }
            else
            {
                return new SolidColorBrush(Colors.LightSalmon);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
