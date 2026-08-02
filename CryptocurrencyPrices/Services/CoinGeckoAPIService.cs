using CryptocurrencyPrices.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptocurrencyPrices.Services
{
    class CoinGeckoAPIService : IAPIService
    {
        const string BASE_URL = "https://api.coingecko.com/api/v3";

        private readonly HttpClient _httpClient;
        private readonly string _targetCurrencies;

        public CoinGeckoAPIService(double timeout, string targetCurrencies = "usd")
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/151.0.0.0 Safari/537.36 Edg/151.0.0.0");

            _targetCurrencies = targetCurrencies;
        }

        public async Task<List<CryptocurrencyPrice>> GetPrices(List<Cryptocurrency> currencies)
        {
            var ids = currencies.Select(item => item.CoinGeckoId);
            var idsString = string.Join(",", ids);
            var url = $"{BASE_URL}/simple/price?ids={idsString}&vs_currencies={_targetCurrencies}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(json);

            var result = new List<CryptocurrencyPrice>();

            foreach (var c in currencies)
            {
                if (data.TryGetValue(c.CoinGeckoId, out JToken cryptocurrencyToken))
                {
                    if (cryptocurrencyToken is JObject cryptocurrency && cryptocurrency.TryGetValue(_targetCurrencies, out JToken price))
                    {
                        result.Add(new CryptocurrencyPrice { Cryptocurrency = c, Price = (float)price });
                    }
                }
            }

            return result;
        }
    }
}
