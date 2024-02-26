using PaymentFees;
using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Logic
{
    public class PaymentManager : IPaymentManager
    {
        private ICardsRepository _cardsRepository;
        private static decimal CurrentFeeInUse = 1;
        private IUfeService _ufeService;
        private object _locker = new object();

        public PaymentManager(ICardsRepository cardsRepository
            , IUfeService ufeService)
        {
            _cardsRepository = cardsRepository;
            CurrentFeeInUse = 1;
            _ufeService = ufeService;
        }

        public void ProcessPayment(long cardNumberFrom, long cardNumberTo, decimal amount)
        {
            lock (_locker)
            {
                var currentValueFrom = _cardsRepository.GetByNumber(cardNumberFrom);
                var currentValueTo = _cardsRepository.GetByNumber(cardNumberTo);

                if (currentValueFrom != null && currentValueTo != null)
                {
                    var newBalance = currentValueFrom.Balance - ( amount + (amount * GetLastFee()));
                    var newValue = new CardModel { Number = cardNumberFrom, Balance = newBalance };
                    _cardsRepository.Update(newValue);

                    var newBalanceTo = currentValueTo.Balance + amount;
                    var newValueTo = new CardModel { Number = cardNumberTo, Balance = newBalanceTo };
                    _cardsRepository.Update(newValueTo);
                }
                else
                {
                    throw new NullReferenceException();

                }

            }

        }

        private decimal GetLastFee()
        {
            CurrentFeeInUse *= Convert.ToDecimal(_ufeService.GetFeeDecimal());

            return CurrentFeeInUse;
        }
    }
}
