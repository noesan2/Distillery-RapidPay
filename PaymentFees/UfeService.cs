using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFees
{
    public class UfeService : IUfeService
    {
        private object _locker = new object();
        private double _feeRatio;

        public UfeService()
        {
            _feeRatio = 1.0;
        }

        public double UpdateFeeDecimal()
        {
            lock (_locker)
            {
                _feeRatio = new Random().NextDouble() * 2;
                return _feeRatio;
            }
        }

        public double GetFeeDecimal()
        {
            lock (_locker)
            {
                return _feeRatio;
            }
        }
    }
}
