using System;
using Windows.UI.Xaml.Data;

namespace CryptocurrencyPrices.Converters
{
    class WidthToZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double currentWidth && parameter is string minWidthString)
            {
                var minWidth = double.Parse(minWidthString);
                return currentWidth <= minWidth ? 0.0 : double.NaN;
            }

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
