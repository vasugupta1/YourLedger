using System;
using Newtonsoft.Json;

namespace YourLedger.Common.Models.AlphaVantage.Response
{
    public class StockData
    {
        [JsonProperty("Global Quote")]public GlobalQuote Data {get;set;}
    }

    public class GlobalQuote
    {
        [JsonProperty("01. symbol")]public string Symbol {get;set;}
        [JsonProperty("02. open")]public float Open {get;set;}
        [JsonProperty("03. high")]public float High {get;set;}
        [JsonProperty("04. low")]public float Low {get;set;}
        [JsonProperty("05. Price")]public float Price {get;set;}
        [JsonProperty("06. volume")]public float Volume {get;set;}
        [JsonProperty("07. latest trading day")]public DateTime LastestTradingDay {get;set;}
        [JsonProperty("08. previous close")]public float PreviousClose {get;set;}
        [JsonProperty("09. change")]public float Change {get;set;}
        [JsonProperty("10. change percent")]public string ChangePercent {get;set;}
    }
}