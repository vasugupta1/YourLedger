using YourLedger.Functions.Services.DataProcesser.Interface;
using YourLedger.Common.Models.PubSub;
using System;
using System.Collections.Generic;
using YourLedger.Common.Models.UserData;
using YourLedger.Functions.Services.DataProcesser.Exceptions;

namespace YourLedger.Functions.Services.DataProcesser
{
    public class DataProcessor : IDataProcessor<StockMessage, UserEquity>, IDataProcessor<CryptoMessage,UserCrypto>
    {
        public UserEquity ProcessBuyOrder(StockMessage buyData, UserEquity userData)
        {
            try
            {
                if(buyData == null)
                    throw new ArgumentNullException(nameof(buyData));

                if(userData == null)
                    throw new ArgumentNullException(nameof(userData));

                var currentDate = DateTime.Today.ToShortDateString();
                
                userData.TotalInvestedAmount += buyData.Amount;
                userData.TotalEquityAmount += buyData.Amount / buyData.CapturedStockData.Data.Price;
        
                if(userData.TradeHistory.ContainsKey(currentDate))
                {
                    userData.TradeHistory[currentDate].Add(buyData);
                    return userData;
                }
                else
                {
                    var tradingHistory = new List<StockMessage>();
                    tradingHistory.Add(buyData);     
                    userData.TradeHistory.Add(currentDate, tradingHistory);
                    return userData;
                }
            }
            catch(Exception ex)
            {
                throw new DataProcessorException("Something went wrong in data processor", ex);
            }
        }

        public UserCrypto ProcessBuyOrder(CryptoMessage buyData, UserCrypto userData)
        {
            try
            {
                if(buyData == null)
                    throw new ArgumentNullException(nameof(buyData));

                if(userData == null)
                    throw new ArgumentNullException(nameof(userData));

                var currentDate = DateTime.Today.ToShortDateString();

                userData.TotalInvestedAmount += buyData.Amount;
                userData.TotalAssetAmount += buyData.Amount / buyData.CapturedStockData.Data.ExchangeRate;

                if(userData.TradeHistory.ContainsKey(currentDate))
                {
                    userData.TradeHistory[currentDate].Add(buyData);
                    return userData;
                }
                else
                {
                    var tradingHistory = new List<CryptoMessage>();
                    tradingHistory.Add(buyData);     
                    userData.TradeHistory.Add(currentDate, tradingHistory);
                    return userData;
                }
            }
            catch(Exception ex)
            {
                throw new DataProcessorException("Something went wrong in data processor", ex);
            }
        }

        public UserEquity ProcessSellOrder(StockMessage sellData, UserEquity userData)
        {
            try
            {
                if(sellData == null)
                    throw new ArgumentNullException(nameof(sellData));

                if(userData == null)
                    throw new ArgumentNullException(nameof(userData));

                var currentDate = DateTime.Today.ToShortDateString();
                
                userData.TotalInvestedAmount -= sellData.Amount;
                userData.TotalEquityAmount -= sellData.Amount / sellData.CapturedStockData.Data.Price;
        
                if(userData.TradeHistory.ContainsKey(currentDate))
                {
                    userData.TradeHistory[currentDate].Add(sellData);
                    return userData;
                }
                else
                {
                    var tradingHistory = new List<StockMessage>();
                    tradingHistory.Add(sellData);     
                    userData.TradeHistory.Add(currentDate, tradingHistory);
                    return userData;
                }
            }   
            catch(Exception ex)
            {
                throw new DataProcessorException("Something went wrong in data processor", ex);
            }
        }

        public UserCrypto ProcessSellOrder(CryptoMessage sellData, UserCrypto userData)
        {
             try
            {
                if(sellData == null)
                    throw new ArgumentNullException(nameof(sellData));

                if(userData == null)
                    throw new ArgumentNullException(nameof(userData));

                var currentDate = DateTime.Today.ToShortDateString();

              userData.TotalInvestedAmount -= sellData.Amount;
                userData.TotalAssetAmount -= sellData.Amount / sellData.CapturedStockData.Data.ExchangeRate;

                if(userData.TradeHistory.ContainsKey(currentDate))
                {
                    userData.TradeHistory[currentDate].Add(sellData);
                    return userData;
                }
                else
                {
                    var tradingHistory = new List<CryptoMessage>();
                    tradingHistory.Add(sellData);     
                    userData.TradeHistory.Add(currentDate, tradingHistory);
                    return userData;
                }
            }   
            catch(Exception ex)
            {
                throw new DataProcessorException("Something went wrong in data processor", ex);
            }
        }
    }
}