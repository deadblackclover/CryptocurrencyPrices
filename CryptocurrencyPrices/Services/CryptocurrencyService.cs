using CryptocurrencyPrices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace CryptocurrencyPrices.Services
{
    class CryptocurrencyService
    {
        const string PATH = "ms-appx:///Assets/data/cryptocurrency.json";

        public async Task<List<Cryptocurrency>> LoadCurrencies()
        {
            var jsonString = await LoadJsonFromFileAsync();
            var currencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(jsonString);
            return currencies;
        }

        private async Task<string> LoadJsonFromFileAsync()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(PATH));
            string json = await FileIO.ReadTextAsync(file);
            return json;
        }
    }
}
