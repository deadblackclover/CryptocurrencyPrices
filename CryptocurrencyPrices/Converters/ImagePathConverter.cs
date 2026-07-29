using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace CryptocurrencyPrices.Converters
{
    class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bitmapImage = new BitmapImage();
            var uriString = CreatePath(value is string name && !string.IsNullOrEmpty(name) ? name : "Bitcoin");
            bitmapImage.UriSource = new Uri(uriString);
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string CreatePath(string name) => $"ms-appx:///Assets/Coins/{name}.png";
    }
}
