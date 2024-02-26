using RapidPay.Models;

namespace RapidPay.Data
{
    public interface ICardsRepository
    {
        void Create(CardModel cardToCreate);

        bool IsCardNumberNotInUse(long cardNumber);

        void Update(CardModel cardToUpdate);

        CardModel GetByNumber(long cardNumber);
    }
}
