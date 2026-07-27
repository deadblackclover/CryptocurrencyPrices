using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace CryptocurrencyPrices.Services
{
    interface IAPIService
    {
        Task<JObject> GetPrices(string[] ids);
    }
}
