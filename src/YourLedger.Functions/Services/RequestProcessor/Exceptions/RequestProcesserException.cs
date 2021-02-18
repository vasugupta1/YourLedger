using System;

namespace YourLedger.Functions.Services.RequestProcessor.Exceptions
{
    public class RequestProcesserException : Exception
    {
        public RequestProcesserException(string message , Exception innerException): base(message , innerException)
        {

        }
    }
}