using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using YourLedger.Services.AlphaVantage.Interface;
using YourLedger.Common.Models.AlphaVantage.Response;
using YourLedger.WebApi.Services.AlphaVantage.Exception;

namespace YourLedger.Services.AlphaVantage
{
    public class AlphaVantageService : IAlphaVantageService
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;
        public AlphaVantageService(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _apiKey = !string.IsNullOrEmpty(apiKey) ? apiKey : throw new ArgumentNullException(nameof(apiKey));
        }

        public async Task<StockData> GetStockData(string stockSymbol)
        {
            if(string.IsNullOrEmpty(stockSymbol))
                throw new ArgumentNullException(nameof(stockSymbol));
            try
            {
              return await _baseUrl
              .SetQueryParams(
                  new 
                  { apikey = _apiKey,
                    function = "GLOBAL_QUOTE",
                    symbol = stockSymbol
                  })
              .GetJsonAsync<StockData>();
            }
            catch(System.Exception ex)
            {
                throw new AlphaVantageServiceException("Error occured when getting stock data", ex);
            }
        }

        public async Task<CryptoData> GetCryptoData(string cryptoSymbol, string currency)
        {
            if(string.IsNullOrEmpty(cryptoSymbol))
                throw new ArgumentNullException(nameof(cryptoSymbol)); 
            try
            {
              return await _baseUrl
              .SetQueryParams(
                  new 
                  { 
                    apikey = _apiKey,
                    function = "CURRENCY_EXCHANGE_RATE",
                    from_currency = cryptoSymbol,
                    to_currency = currency
                  })
              .GetJsonAsync<CryptoData>();
            }
            catch(System.Exception ex)
            {
                throw new AlphaVantageServiceException("Error occured when geting crypto data", ex);
            }
        }
    }
}