namespace RapidPay.Logic
{
    public interface IPaymentManager
    {
        void ProcessPayment(long cardFrom, long cardTo, decimal amount);
    }
}
