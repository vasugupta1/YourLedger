using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourLedger.WebApi.Services.AlphaVantage.Exception;
using YourLedger.Services.AlphaVantage.Interface;
using YourLedger.WebApi.Services.PubSub.Interface;
using YourLedger.Common.Models.PubSub;
using YourLedger.WebApi.Services.PubSub.Exception;
using YourLedger.Common.Enum;
using Microsoft.AspNetCore.Http;
using YourLedger.WebApi.Models.Routes;
namespace YourLedger.WebApi.Controllers
{
    [ApiController]
    [Route("")]
    public class LedgerController : ControllerBase
    {
        private readonly ILogger<LedgerController> _logger;
        private readonly IAlphaVantageService _alphaVantageService;
        private readonly IPubSubService<StockMessage, CryptoMessage> _pubService;
       
        public LedgerController(ILogger<LedgerController> logger, IAlphaVantageService alphaVantageService, IPubSubService<StockMessage, CryptoMessage> pubService)
        {
            _logger = logger;
            _alphaVantageService = alphaVantageService ?? throw new ArgumentNullException(nameof(alphaVantageService));
            _pubService = pubService ?? throw new ArgumentNullException(nameof(pubService));
        }
        
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(LedgerRoutes.BuyEquityOrder)]
        public async Task<IActionResult> BuyEquityOrder(string userId, string symbol, float amount)
        {
            if(string.IsNullOrEmpty(userId))
               throw new ArgumentNullException(nameof(userId));

            if(string.IsNullOrEmpty(symbol))
               throw new ArgumentNullException(nameof(symbol));

            if(amount <= 0.0 )
               throw new ArgumentNullException(nameof(amount));
            try
            {
                var stockData = await _alphaVantageService.GetStockData(symbol);
                await _pubService.PublishMessage(new StockMessage(userId ,stockData, amount, OrderType.Buy));
                return NoContent();
            }
            catch(AlphaVantageServiceException avex)
            {
                _logger.LogError("Error with alpha-vantage-service", avex);
                return StatusCode(500);
            }
            catch(PubSubServiceException psex)
            {
                _logger.LogError("Error with pubSubService", psex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured", ex);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(LedgerRoutes.BuyCryptoOrder)]
        public async Task<IActionResult> BuyCryptoOrder(string userId, string crypto, string currency, float amount)
        {
            if(string.IsNullOrEmpty(userId))
               throw new ArgumentNullException(nameof(userId));

            if(string.IsNullOrEmpty(crypto))
                throw new ArgumentNullException(nameof(crypto));

            if(string.IsNullOrEmpty(currency))
                throw new ArgumentNullException(nameof(currency));
            
            if(amount <= 0.0)
                throw new ArgumentNullException(nameof(amount));
            try
            {
                var cryptoData = await _alphaVantageService.GetCryptoData(crypto, currency);
                await _pubService.PublishMessage(new CryptoMessage(userId, cryptoData, amount, OrderType.Buy));
                return NoContent();
            }
            catch(AlphaVantageServiceException avex)
            {
                _logger.LogError("Error with alpha-vantage-service", avex);
                return StatusCode(500);
            }
            catch(PubSubServiceException psex)
            {
                _logger.LogError("Error with pubSubService", psex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured", ex);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(LedgerRoutes.SellEquityOrder)]
        public async Task<IActionResult> SellEquityOrder(string userId, string symbol, float amount)
        {
            if(string.IsNullOrEmpty(userId))
               throw new ArgumentNullException(nameof(userId));

            if(string.IsNullOrEmpty(symbol))
               throw new ArgumentNullException(nameof(symbol));

            if(amount <= 0.0)
                throw new ArgumentNullException(nameof(amount));        

            try
            {
                var stockData = await _alphaVantageService.GetStockData(symbol);
                await _pubService.PublishMessage(new StockMessage(userId ,stockData, amount, OrderType.Sell));
                return NoContent();
            }
            catch(AlphaVantageServiceException avex)
            {
                _logger.LogError("Error with alpha-vantage-service", avex);
                return StatusCode(500);
            }
            catch(PubSubServiceException psex)
            {
                _logger.LogError("Error with pubSubService", psex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured", ex);
                return StatusCode(500);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(LedgerRoutes.SellCryptoOrder)]
        public async Task<IActionResult> SellCryptoOrder(string userId, string crypto, string currency, float amount)
        {
            if(string.IsNullOrEmpty(userId))
               throw new ArgumentNullException(nameof(userId));

            if(string.IsNullOrEmpty(crypto))
                throw new ArgumentNullException(nameof(crypto));

            if(string.IsNullOrEmpty(currency))
                throw new ArgumentNullException(nameof(currency));
            
            if(amount <= 0.0)
                throw new ArgumentNullException(nameof(amount));
            try
            {
                var cryptoData = await _alphaVantageService.GetCryptoData(crypto, currency);
                await _pubService.PublishMessage(new CryptoMessage(userId, cryptoData, amount, OrderType.Sell));
                return NoContent();
            }
            catch(AlphaVantageServiceException avex)
            {
                _logger.LogError("Error with alpha-vantage-service", avex);
                return StatusCode(500);
            }
            catch(PubSubServiceException psex)
            {
                _logger.LogError("Error with pubSubService", psex);
                return StatusCode(500);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occured", ex);
                return StatusCode(500);
            }
        }
    }
}
