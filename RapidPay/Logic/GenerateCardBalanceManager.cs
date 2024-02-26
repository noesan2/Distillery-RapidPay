using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Logic
{
    public class GenerateCardBalanceManager : IGenerateCardBalanceManager
    {
        private ICardsRepository _cardsRepository;

        public GenerateCardBalanceManager(ICardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository; 
        }

        public CardModel Generate(long cardNumber)
        {
            return _cardsRepository.GetByNumber(cardNumber);
        }
    }
}
