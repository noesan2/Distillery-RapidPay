namespace RapidPay.Logic
{
    public interface IGenerateCardBalanceManager
    {
        object Generate(int cardNumber);
    }
}
