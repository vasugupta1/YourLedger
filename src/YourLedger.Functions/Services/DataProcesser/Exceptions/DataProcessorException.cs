using System;

namespace YourLedger.Functions.Services.DataProcesser.Exceptions
{
    public class DataProcessorException : Exception
    {
        public DataProcessorException(string message , Exception innerException): base(message , innerException)
        {

        }
    }
}