using System;

namespace YourLedger.Functions.Services.Storage.Exceptions
{
    public class StorageServiceException : Exception
    {
        public StorageServiceException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}