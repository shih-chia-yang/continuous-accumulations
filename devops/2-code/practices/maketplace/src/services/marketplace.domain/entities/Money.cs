using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class Money:ValueObject
    {
        public static Money Create(decimal amount)=>new Money(amount);

        public static Money Create(string amount)
        {
            if(!Regex.IsMatch(amount,@"/^\d*\.?\d*$/"))
            {
                throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
            }
            return new Money(Decimal.Parse(amount));
        }

        public decimal Amount { get;}

        protected Money(decimal amount) => Amount = amount;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }

        public Money Add(Money sumand) => new Money(this.Amount + sumand.Amount);
        public Money Subtraction(Money subtrahend) => new Money(this.Amount - subtrahend.Amount);
        public static Money operator + (Money sumand1, Money sumand2) => sumand1.Add(sumand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtraction(subtrahend);
    }
}