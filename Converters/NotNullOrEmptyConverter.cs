using System;
using System.Globalization;
using System.Windows.Data;

namespace Project_Groceries.Converters
{
    internal class NotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value != null && (value as string) != string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
