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
                return "$" + price.ToString();
            }
            return "$0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
