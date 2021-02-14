using System.Collections.Generic;
using Newtonsoft.Json;
using YourLedger.Common.Models.PubSub;

namespace YourLedger.Common.Models.UserData
{
  public class UserCrypto
    {
       [JsonProperty("totalInvestedAmount")]
        public double TotalInvestedAmount {get;set;}
        [JsonProperty("totalEquityAmount")]
        public double TotalEquityAmount {get;set;}
        [JsonProperty("tradeHistory")]
        public Dictionary<string, List<CryptoMessage>> TradeHistory {get;set;}
        public UserCrypto(double totalEquityAmount, double totalInvestedAmount, Dictionary<string, List<CryptoMessage>> tradeHistory = null )
        {
            TotalInvestedAmount = totalInvestedAmount;
            TotalEquityAmount = totalEquityAmount;
            TradeHistory = tradeHistory ?? new Dictionary<string, List<CryptoMessage>>();
        }
    }
}