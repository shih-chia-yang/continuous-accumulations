using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using marketplace.domain.kernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketplace.domain.AggregateModels
{
    public class Money:ValueObject,ICurrencyExpression
    {
        
        public Currency Currency { get; internal set; }
        
        public decimal Amount { get; internal set; }

        protected Money() { }

        protected Money(decimal amount,Currency currency=null)
        {
            Currency = currency??Currency.Default;
            if(!Currency.InUse)
                throw new ArgumentException($"Currency {Currency.CurrencyCode} is not Valid");
            if (Decimal.Round(amount,Currency.DecimalPlace)!=amount)
            {
                throw new ArgumentOutOfRangeException($"Amount in {currency.CurrencyCode} cannot have more than {currency.DecimalPlace} decimals",nameof(amount));
            }
            Amount = amount;
        }

        public static Money Create(decimal amount,Currency currency=null)=>new Money(amount,currency);

        public static Money Create(string amount,Currency currency=null)
        {
            if(!Regex.IsMatch(amount,@"^\d*\.?\d*$"))
            {
                throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
            }
            return new Money(Decimal.Parse(amount),currency);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString() => $"{this.Currency.CurrencyCode} {this.Amount}";
    }
}