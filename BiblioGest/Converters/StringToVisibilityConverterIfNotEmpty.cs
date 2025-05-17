// Dans Converters/StringToVisibilityConverterIfNotEmpty.cs
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BiblioGest.Converters
{
    public class StringToVisibilityConverterIfNotEmpty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasText = !string.IsNullOrEmpty(value as string);
            
            // Invert logic if parameter is "Invert" or similar
            bool invert = parameter is string strParam && 
                          (strParam.Equals("Invert", StringComparison.OrdinalIgnoreCase) || 
                           strParam.Equals("InvertIfNotEmpty", StringComparison.OrdinalIgnoreCase));

            if (invert)
            {
                return hasText ? Visibility.Collapsed : Visibility.Visible;
            }
            return hasText ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}