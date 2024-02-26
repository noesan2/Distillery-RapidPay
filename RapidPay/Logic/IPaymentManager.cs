namespace RapidPay.Logic
{
    public interface IPaymentManager
    {
        void ProcessPayment(int card, decimal amount);
    }
}
