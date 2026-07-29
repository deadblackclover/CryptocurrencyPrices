using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace CryptocurrencyPrices.Converters
{
    class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmapImage = new BitmapImage
            {
                UriSource = CreatePath(value is string name && !string.IsNullOrEmpty(name) ? name : "Bitcoin")
            };
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private Uri CreatePath(string name)
        {
            var uriString = $"ms-appx:///Assets/Coins/{name.Replace(" ", "_")}.png";
            return new Uri(uriString);
        }
    }
}
