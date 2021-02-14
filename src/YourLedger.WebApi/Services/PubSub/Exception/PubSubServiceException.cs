namespace YourLedger.WebApi.Services.PubSub.Exception
{
    public class PubSubServiceException: System.Exception
    {
        public PubSubServiceException(string message, System.Exception innerException):base(message, innerException)
        {

        }
    }
}