using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain
{
    public class Money:ValueObject
    {
        public decimal Amount { get;}

        public Money(decimal amount) => Amount = amount;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }
    }
}