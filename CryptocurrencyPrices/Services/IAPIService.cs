using CryptocurrencyPrices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptocurrencyPrices.Services
{
    interface IAPIService
    {
        Task<List<CryptocurrencyPrice>> GetPrices(List<Cryptocurrency> currencies);
    }
}
