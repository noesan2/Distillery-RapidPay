namespace RapidPay.Controllers.RequestDto
{
    public class PaymentDetailsDto
    {
        public int CardNumberTo { get; set; }
        
        public int CardNumberFrom { get; set; }

        public decimal Amount { get; set; }
    }
}
