using RapidPay.Models;
using System.Collections.Concurrent;

namespace RapidPay.Data
{
    public class Cards
    {
        private ConcurrentDictionary<long, CardModel> _cards = new ConcurrentDictionary<long, CardModel>();

        public void Insert(CardModel newCard)
        { 
            _cards.TryAdd(newCard.Number, newCard);
        }

        public CardModel? Get(long cardNumber)
        { 
            _cards.TryGetValue(cardNumber, out var card);
            return card;
        }

        public void Update(CardModel cardToUpdate)
        {
            _cards.TryGetValue(cardToUpdate.Number, out var card);

            _cards.TryUpdate(cardToUpdate.Number, cardToUpdate, card);
        }

        public bool TryGet(long number)
        {
            return _cards.TryGetValue(number, out var existing);
        }
    }

}
