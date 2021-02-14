using System;

namespace YourLedger.Functions.Models.Exceptions
{
     public class FunctionException : Exception
    {
        public FunctionException(string message, Exception innerException):base(message, innerException)
        {

        }
    }
}