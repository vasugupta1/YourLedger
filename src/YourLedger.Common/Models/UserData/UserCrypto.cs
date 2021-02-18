using System.Collections.Generic;
using Newtonsoft.Json;
using YourLedger.Common.Models.PubSub;

namespace YourLedger.Common.Models.UserData
{
  public class UserCrypto
    {
        [JsonProperty("totalInvestedAmount")]
        public double TotalInvestedAmount {get;set;}
        [JsonProperty("TotalAssetAmount")]
        public double TotalAssetAmount {get;set;}
        [JsonProperty("tradeHistory")]
        public Dictionary<string, List<CryptoMessage>> TradeHistory {get;set;}
        public UserCrypto(double totalInvestedAmount, double totalAssetAmount, Dictionary<string, List<CryptoMessage>> tradeHistory = null )
        {
            TotalInvestedAmount = totalInvestedAmount;
            TotalAssetAmount = totalAssetAmount;
            TradeHistory = tradeHistory ?? new Dictionary<string, List<CryptoMessage>>();
        }
    }
}