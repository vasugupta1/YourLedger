using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YourLedger.Common.Enum;
using YourLedger.Common.Models.PubSub;
using YourLedger.WebApi.Services.PubSub.Exception;
using YourLedger.WebApi.Services.PubSub.Interface;

namespace YourLedger.Services.PubSub
{
    public class PubSubService<T> : IPubSubService<T>
    {
        private readonly PublisherClient _pubClient;
        public PubSubService(PublisherClient pubClient)
        {
            _pubClient = pubClient ?? throw new ArgumentNullException(nameof(pubClient));
        }

        public async Task PublishMessage(T request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                await _pubClient.PublishAsync(new PubsubMessage()
                {
                    Data = ByteString.CopyFromUtf8(JsonConvert.SerializeObject(request))
                });
            }
            catch(System.Exception ex)
            {
                throw new PubSubServiceException("Error when trying to queue message", ex);
            }
        }
    }
}