// In Converters/ZeroToBrushConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BiblioGest.Converters
{
    public class ZeroToBrushConverter : IValueConverter
    {
        public Brush ColorIfZero { get; set; } = Brushes.Green; // Default
        public Brush ColorIfNotZero { get; set; } = Brushes.Red;   // Default

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue == 0 ? ColorIfZero : ColorIfNotZero;
            }
            return Brushes.Black; // Fallback
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}