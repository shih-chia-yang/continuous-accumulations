using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using marketplace.domain.kernal;
using marketplace.domain.exceptions;

namespace marketplace.domain.entities
{
    public class Money:ValueObject,ICurrencyExpression
    {
        public static Money Create(decimal amount,string currency=DefaultCurrency)=>new Money(amount,currency);

        public static Money Create(string amount,string currency=DefaultCurrency)
        {
            if(!Regex.IsMatch(amount,@"/^\d*\.?\d*$/"))
            {
                throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
            }
            return new Money(Decimal.Parse(amount),currency);
        }

        private const string DefaultCurrency = "TWD";
        public string Currency { get; }
        public decimal Amount { get;}

        protected Money(decimal amount,string currency=DefaultCurrency)
        {
            if (Decimal.Round(amount,2)!=amount)
            {
                throw new ArgumentOutOfRangeException("Amount cannot have more than two decimal", nameof(amount));
            }
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money sumand)
        {
            if(this.Currency!=sumand.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            return new Money(this.Amount + sumand.Amount);
        }
        public Money Subtraction(Money subtrahend)
        {
            if(this.Currency!=subtrahend.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            return new Money(this.Amount - subtrahend.Amount);
        } 
            
        public static Money operator + (Money sumand1, Money sumand2) => sumand1.Add(sumand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtraction(subtrahend);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}