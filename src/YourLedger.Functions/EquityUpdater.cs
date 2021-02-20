using CloudNative.CloudEvents;
using Google.Cloud.Functions.Framework;
using Google.Events.Protobuf.Cloud.PubSub.V1;
using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Functions.Hosting;
using YourLedger.Functions.Services.Storage.Interface;
using YourLedger.Functions.Services.DataProcesser.Interface;
using YourLedger.Common.Models.PubSub;
using YourLedger.Common.Models.UserData;
using YourLedger.Functions.Models.Exceptions;
using Microsoft.Extensions.Logging;
using YourLedger.Functions.Services.RequestProcessor.Interface;

namespace YourLedger.Functions
{
    [FunctionsStartup(typeof(Startup))]
    public class EquityUpdater : ICloudEventFunction<MessagePublishedData>
    {
        private readonly IStorageService<UserEquity> _storageService;
        private readonly IDataProcessor<StockMessage, UserEquity> _dataProcessor;
        private readonly IRequestProcesser<StockMessage> _requestProcesser;
        private readonly ILogger _logger;
        private const string _userIdAttribute = "UserId";
        public EquityUpdater(ILogger<EquityUpdater> logger ,IStorageService<UserEquity> storageService, IDataProcessor<StockMessage, UserEquity> dataProcessor, IRequestProcesser<StockMessage> requestProcesser)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _dataProcessor = dataProcessor ?? throw new ArgumentNullException(nameof(dataProcessor));
            _requestProcesser = requestProcesser ?? throw new ArgumentNullException(nameof(requestProcesser));
        }

       public async Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Function has started");

            if(data == null)
            {
                _logger.LogError(new FunctionException("Data cannot be null", new ArgumentNullException(nameof(data))), "Updater Function Error");
            }
            try
            {
                _logger.LogInformation("Function has started");
                var request = _requestProcesser.GetRequest(data);
                UserEquity updatedUserData;
                var fileName = $"{request.UserId}/{request.CapturedStockData.Data.Symbol}";
                switch(request.OrderType.ToString())
                {
                    case "Buy":
                        updatedUserData = _dataProcessor.ProcessBuyOrder(request, await _storageService.FileExists(fileName)? await _storageService.GetFile(fileName) : new UserEquity(0.0, 0.0) );
                        await _storageService.UploadFile(fileName, updatedUserData);
                        break;

                    case "Sell":
                        updatedUserData = _dataProcessor.ProcessSellOrder(request, await _storageService.FileExists(fileName)? await _storageService.GetFile(fileName) : throw new Exception("Cannot sell something you don't have"));
                        await _storageService.UploadFile(fileName, updatedUserData);
                        break;
                        
                    default :
                        throw new Exception($"This order type doesn't exist,{request.UserId}");
                }         
               _logger.LogInformation("Function has ended");
            }
            catch(Exception ex)
            {
                _logger.LogError(new FunctionException("Something went wrong, check the inner exception", ex), "Updater Function Error");
            }      
        }
    }
}
