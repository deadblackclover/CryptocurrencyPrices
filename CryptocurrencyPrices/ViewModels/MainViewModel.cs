using CryptocurrencyPrices.Models;
using CryptocurrencyPrices.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                var ids = currencies.Select(item => item.Id).ToArray();
                var result = await _apiService.GetPrices(ids);

                _prices.Clear();

                foreach (var c in currencies)
                {
                    if (result.TryGetValue(c.Id, out JToken cryptocurrencyToken))
                    {
                        if (cryptocurrencyToken is JObject cryptocurrency && cryptocurrency.TryGetValue(TARGET_CURRENCIES, out JToken price))
                        {
                            _prices.Add(new CryptocurrencyPrice { Cryptocurrency = c, Price = (float)price });
                        }
                    }
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
