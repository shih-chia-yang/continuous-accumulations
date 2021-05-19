using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using marketplace.domain.kernal;
using marketplace.domain.exceptions;

namespace marketplace.domain.entities
{
    public class Money:ValueObject,ICurrencyExpression
    {
        public static Money Create(decimal amount,Currency currency=null)=>new Money(amount,currency);

        public static Money Create(string amount,Currency currency=null)
        {
            if(!Regex.IsMatch(amount,@"^\d*\.?\d*$"))
            {
                throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
            }
            return new Money(Decimal.Parse(amount),currency);
        }

        // private const string DefaultCurrency = "TWD";
        public Currency Currency { get; }
        public decimal Amount { get;}

        protected Money(decimal amount,Currency currency=null)
        {
            if (Decimal.Round(amount,2)!=amount)
            {
                throw new ArgumentOutOfRangeException("Amount cannot have more than two decimal", nameof(amount));
            }
            Amount = amount;
            Currency = currency??Currency.Default;
        }

        // public ICurrencyExpression Create(decimal amount)
        // {
        //     return Money.Create(amount);
        // }
            
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}