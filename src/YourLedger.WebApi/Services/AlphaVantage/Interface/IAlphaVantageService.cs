using System;
using System.Threading.Tasks;
using YourLedger.Common.Models.AlphaVantage.Response;

namespace YourLedger.Services.AlphaVantage.Interface
{
    public interface IAlphaVantageService
    {
         Task<StockData> GetStockData(string symbol);
         Task<CryptoData> GetCryptoData(string cryptoSymbol, string currency);
    }
}