using Google.Events.Protobuf.Cloud.PubSub.V1;

namespace YourLedger.Functions.Services.RequestProcessor.Interface
{
    public interface IRequestProcesser<T>
    {
        T GetRequest(MessagePublishedData eventData);
    }
}