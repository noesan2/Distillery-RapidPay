using RapidPay.Models;

namespace RapidPay.Logic
{
    public interface IGenerateCardBalanceManager
    {
        CardModel Generate(long cardNumber);
    }
}
