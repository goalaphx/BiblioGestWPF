// Dans Converters/BooleanToVisibilityConverter.cs
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BiblioGest.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool b)
            {
                flag = b;
            }
            else if (value is bool?)
            {
                flag = ((bool?)value).GetValueOrDefault();
            }

            // Permet d'inverser la logique si parameter="Invert" ou "Reverse"
            if (parameter is string strParam && (strParam.Equals("Invert", StringComparison.OrdinalIgnoreCase) || strParam.Equals("Reverse", StringComparison.OrdinalIgnoreCase)))
            {
                flag = !flag;
            }

            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // La conversion inverse n'est généralement pas nécessaire pour ce convertisseur
            if (value is Visibility visibility)
            {
                bool flag = visibility == Visibility.Visible;
                if (parameter is string strParam && (strParam.Equals("Invert", StringComparison.OrdinalIgnoreCase) || strParam.Equals("Reverse", StringComparison.OrdinalIgnoreCase)))
                {
                    flag = !flag;
                }
                return flag;
            }
            return DependencyProperty.UnsetValue; // Indique une erreur de conversion
        }
    }
}