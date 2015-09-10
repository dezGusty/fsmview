using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FiniteStateMachine.Graph
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
