using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Logic
{
    public class CardCreationManager : ICardCreationManager
    {
        private ICardsRepository _cardsRepository;
        private CardNumbers _cardNumbers;

        public CardCreationManager(ICardsRepository cardsRepository
            , CardNumbers carNumbers)
        {
            _cardsRepository = cardsRepository;
            _cardNumbers = carNumbers;
        }

        public CardModel Create()
        {
            if (_cardNumbers.IsAnyAvailable())
            {
                var newNumber = _cardNumbers.GetNext();
                var card = new CardModel { Number = newNumber, Balance = 20000 };
                _cardsRepository.Create(card);

                return card;
            }

            throw new NullReferenceException();

        }
    }
}
