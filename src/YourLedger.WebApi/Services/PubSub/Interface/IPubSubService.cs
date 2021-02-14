using System.Threading.Tasks;

namespace YourLedger.WebApi.Services.PubSub.Interface
{
    public interface IPubSubService<T1,T2>
    {
        Task PublishMessage(T1 request);
        Task PublishMessage(T2 request);
    }
}