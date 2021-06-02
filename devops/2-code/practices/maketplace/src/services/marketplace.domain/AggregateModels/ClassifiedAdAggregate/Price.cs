using System;
namespace marketplace.domain.AggregateModels.ClassifiedAdAggregate
{
    public class Price : Money
    {
        public new static Price Create(decimal amount, Currency currency = null) => new Price(amount, currency);

        public static Price NoPrice() => new Price { Amount = -1, Currency = Currency.None };

        protected Price(){}
        protected Price(decimal amount,Currency currency):base(amount,currency)
        {
            if(amount<0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }
    }
}