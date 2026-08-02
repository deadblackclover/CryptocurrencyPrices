using CryptocurrencyPrices.Models;
using CryptocurrencyPrices.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CryptocurrencyPrices.ViewModels
{
    class MainViewModel
    {
        const double TIMEOUT = 10;
        const string TARGET_CURRENCIES = "usd";

        private IAPIService _apiService;
        private CryptocurrencyService _cryptocurrencyService;
        private ObservableCollection<CryptocurrencyPrice> _prices;

        public MainViewModel()
        {
            _apiService = new CoinGeckoAPIService(TIMEOUT, TARGET_CURRENCIES);
            _cryptocurrencyService = new CryptocurrencyService();
            _prices = new ObservableCollection<CryptocurrencyPrice>();
        }

        public ObservableCollection<CryptocurrencyPrice> Prices
        {
            get => _prices;
        }

        public async Task LoadDataAsync()
        {
            try
            {
                var currencies = await _cryptocurrencyService.LoadCurrencies();
                var prices = await _apiService.GetPrices(currencies);

                _prices.Clear();

                foreach (var price in prices)
                {
                    _prices.Add(price);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"[Error] {e}");
                await ShowErrorAsync("Error", e.Message);
            }
        }

        private async Task ShowErrorAsync(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Close",
                DefaultButton = ContentDialogButton.Close
            };

            await dialog.ShowAsync();
        }
    }
}
