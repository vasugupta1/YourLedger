using System.Collections.Generic;
using Newtonsoft.Json;
using YourLedger.Common.Models.PubSub;

namespace YourLedger.Common.Models.UserData
{
   public class UserEquity
    {
        [JsonProperty("totalInvestedAmount")]
        public double TotalInvestedAmount {get;set;}
        [JsonProperty("totalEquityAmount")]
        public double TotalEquityAmount {get;set;}
        [JsonProperty("tradeHistory")]
        public Dictionary<string, List<StockMessage>> TradeHistory {get;set;}
        public UserEquity(double totalEquityAmount, double totalInvestedAmount, Dictionary<string, List<StockMessage>> tradeHistory = null )
        {
            TotalInvestedAmount = totalInvestedAmount;
            TotalEquityAmount = totalEquityAmount;
            TradeHistory = tradeHistory ?? new Dictionary<string, List<StockMessage>>();
        }
    }
}