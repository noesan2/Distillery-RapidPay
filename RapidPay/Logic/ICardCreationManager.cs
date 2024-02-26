using RapidPay.Data;
using RapidPay.Models;

namespace RapidPay.Logic
{
    public interface ICardCreationManager
    {
        CardModel Create();
    }
}
