using Microsoft.AspNetCore.Mvc.Formatters;
using RapidPay.Models;
using System.Diagnostics;

namespace RapidPay.Data
{
    public class CardsRepository : ICardsRepository
    {
        private Cards _cards;

        public CardsRepository(Cards cards) 
        {
            _cards = cards; 
        }

        public void Create(CardModel cardToCreate)
        {
            _cards.Insert(cardToCreate);
        }

        public bool IsCardNumberNotInUse(long cardNumber)
        {
            return ! _cards.TryGet(cardNumber);
        }

        public void Update(CardModel cardToUpdate)
        {
            var before = _cards.Get(cardToUpdate.Number);
            Debug.WriteLine(String.Format( "The new Balance is: {0}", before?.Balance.ToString()));
            
            _cards.Update(cardToUpdate);

            var after = _cards.Get(cardToUpdate.Number);
            Debug.WriteLine(String.Format("The new Balance is: {0}", after?.Balance.ToString()));
        }

        public CardModel? GetByNumber(long cardNumber)
        {
            return _cards.Get(cardNumber);
        }

    }
}
