using System;
using YourLedger.Common.Enum;
using YourLedger.Common.Models.AlphaVantage.Response;

namespace YourLedger.Common.Models.PubSub
{
    public class StockMessage
    {
        public string UserId {get; private set;}
        public StockData CapturedStockData {get; private set;}
        public  float Amount {get; private set;}
        public OrderType OrderType {get; private set;}
        public StockMessage(string userId, StockData capturedStockData, float amount, OrderType orderType )
        {
            UserId = !string.IsNullOrEmpty(userId) ? userId : throw new ArgumentNullException(nameof(userId));
            CapturedStockData = capturedStockData ?? throw new ArgumentNullException(nameof(capturedStockData));
            Amount = amount <= 0.0 ? throw new ArgumentNullException(nameof(amount)) : amount;  
            OrderType = orderType;       
        }
    }
}