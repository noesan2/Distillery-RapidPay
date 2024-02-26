using System.Collections.Concurrent;

namespace RapidPay.Data
{
    public class CardNumbers
    {
        private ConcurrentQueue<long> availableCardNumbers = new ConcurrentQueue<long>();

        public CardNumbers()
        {
            for (long i = 1; i < 999; i++)
            {
                availableCardNumbers.Enqueue(i); 
            }

        }

        public long GetNext()
        { 
            availableCardNumbers.TryDequeue(out var cardNumber);
            return cardNumber;
        }

        public bool IsAnyAvailable()
        {
            return ! availableCardNumbers.IsEmpty;
        }
    }
}
