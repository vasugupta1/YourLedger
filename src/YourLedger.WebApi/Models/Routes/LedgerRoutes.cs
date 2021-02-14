namespace YourLedger.WebApi.Models.Routes
{
    public class LedgerRoutes
    {
        public const string BuyEquityOrder = "/users/{userId}/buy/equity/{symbol}/{amount}";
        public const string BuyCryptoOrder = "/users/{userId}/buy/crypto/{crypto}/{currency}/{amount}";
        public const string SellEquityOrder = "/users/{userId}/sell-order/equity/{symbol}/{amount}";
        public const string SellCryptoOrder = "/users/{userId}/sell-order/crypto/{crypto}/{currency}/{amount}";
    }
}