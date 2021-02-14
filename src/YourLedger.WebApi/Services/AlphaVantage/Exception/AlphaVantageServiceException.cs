using System;

namespace YourLedger.WebApi.Services.AlphaVantage.Exception
{
    public class AlphaVantageServiceException : System.Exception
    {
        public AlphaVantageServiceException(string message, System.Exception innerException): base(message, innerException)
        {

        }
       
    }
}