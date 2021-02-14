using System;
using Newtonsoft.Json;

namespace YourLedger.Common.Models.AlphaVantage.Response
{
    public class CryptoData
    {
        [JsonProperty("Realtime Currency Exchange Rate")]public CurrencyData Data {get;set;}
    }

    public class CurrencyData
    {
        [JsonProperty("1. From_Currency Code")]public string FromCurrencyCode {get;set;}
        [JsonProperty("2. From_Currency Name")]public string FromCurrencyName {get;set;}
        [JsonProperty("3. To_Currency Code")]public string ToCurrencyCode {get;set;}
        [JsonProperty("4. To_Currency Name")]public string ToCurrencyCo {get;set;}
        [JsonProperty("5. Exchange Rate")]public float ExchangeRate {get;set;}
        [JsonProperty("6. Last Refreshed")]public DateTime LastRefreshed {get;set;}
        [JsonProperty("7. Time Zone")]public string TimeZone {get;set;}
        [JsonProperty("8. Bid Price")]public float BidPrice {get;set;}
        [JsonProperty("9. Ask Price")]public float AskPrice {get;set;}
    }
}