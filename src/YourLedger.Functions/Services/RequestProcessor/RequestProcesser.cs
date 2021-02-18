using System;
using Google.Events.Protobuf.Cloud.PubSub.V1;
using Newtonsoft.Json;
using YourLedger.Functions.Services.RequestProcessor.Interface;
using YourLedger.Functions.Services.RequestProcessor.Exceptions;

namespace YourLedger.Functions.Services.RequestProcessor
{
    public class RequestProcesser<T> : IRequestProcesser<T>
    {
        public T GetRequest(MessagePublishedData eventData)
        {
            if(eventData == null)
                throw new ArgumentException(nameof(eventData));
            try
            {
                return JsonConvert.DeserializeObject<T>(eventData.Message.TextData);
            }
            catch(Exception ex)
            {
                throw new RequestProcesserException("Problem when deserialzing text data", ex);
            }
        }
    }
}