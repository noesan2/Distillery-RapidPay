using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidPay.API.AuthPolicies;
using RapidPay.Controllers.RequestDto;
using RapidPay.Data;
using RapidPay.Logic;

namespace RapidPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagmentController : ControllerBase
    {
        private ICardCreationManager _cardCreationManager;
        private IGenerateCardBalanceManager _generateCardBalanceManager;
        private IPaymentManager _paymentManager;

        public CardManagmentController(ICardCreationManager cardCreationManager
            , IGenerateCardBalanceManager cardBalanceManager
            , IPaymentManager paymentManager)
        {
            _cardCreationManager = cardCreationManager;
            _generateCardBalanceManager = cardBalanceManager;
            _paymentManager = paymentManager;
        }

        [HttpGet]
        [Route("Create")]
        [Authorize]
        public IActionResult Create()
        {
            return Ok(_cardCreationManager.Create());
        }

        [HttpPost]
        [Route("Pay")]
        [Authorize(Policy = Holders.AccountHolderPolicyName)]
        public IActionResult Pay([FromBody] PaymentDetailsDto details)
        {
            _paymentManager.ProcessPayment(details.CardNumberFrom, details.CardNumberTo, details.Amount);
            return Ok();
        }

        [HttpGet]
        [Route("GetBalance/{cardId}")]
        [Authorize]
        public IActionResult GetBalance(long cardId)
        {
            var balance = _generateCardBalanceManager.Generate(cardId);
            return Ok(balance);
        }
    }
}
