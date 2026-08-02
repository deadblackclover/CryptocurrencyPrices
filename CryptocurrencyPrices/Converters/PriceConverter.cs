using System;
using Windows.UI.Xaml.Data;

namespace CryptocurrencyPrices.Converters
{
    class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is float price)
            {
                var format = price < 1 ? "F4" : "F2";
                return "$" + price.ToString(format);
            }
            return "$0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
