using PaymentFees;
using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Logic
{
    public class PaymentManager : IPaymentManager
    {
        private ICardsRepository _cardsRepository;
        private static double CurrentFeeInUse = 0;
        private IUfeService _ufeService;

        public PaymentManager(ICardsRepository cardsRepository
            , IUfeService ufeService)
        {
            _cardsRepository = cardsRepository;
            CurrentFeeInUse = 0;
            _ufeService = ufeService;
        }

        public void ProcessPayment(int cardNumber, decimal amount)
        {

            var currentValue = _cardsRepository.GetByNumber(cardNumber);
            if (currentValue != null)
            {
                var newBalance = currentValue.Balance - amount;
                var newValue = new CardModel { Number = cardNumber, Balance = newBalance };
                _cardsRepository.Update(newValue);
            }
            else
            {
                throw new NullReferenceException();

            }

        }
    }
}
