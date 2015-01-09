using System;
using System.Globalization;
using System.Windows.Data;

namespace WL2.Editor.Views.Converters
{
    public class StatisticFillValueConverter : IValueConverter
    {
        public object FilledBrush { get; set; }
        public object EmptyBrush { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentValue = int.Parse(value.ToString());
            var boxIndex = int.Parse(parameter.ToString());

            return currentValue >= boxIndex ? FilledBrush : EmptyBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
