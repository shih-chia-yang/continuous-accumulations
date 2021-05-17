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

        public Money Add(Money sumand) => new Money(this.Amount + sumand.Amount);

        public static Money operator +(Money sumand1, Money sumand2) => sumand1.Add(sumand2);
    }
}