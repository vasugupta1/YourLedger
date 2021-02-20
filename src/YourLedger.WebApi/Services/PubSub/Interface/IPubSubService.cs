using System.Threading.Tasks;
using YourLedger.Common.Enum;

namespace YourLedger.WebApi.Services.PubSub.Interface
{
    public interface IPubSubService<T>
    {
        Task PublishMessage(T request);
    }
}