namespace RapidPay.Controllers.RequestDto
{
    public class PaymentDetailsDto
    {
        public long CardNumberFrom { get; set; }
        
        public long CardNumberTo { get; set; }

        public decimal Amount { get; set; }
    }
}
