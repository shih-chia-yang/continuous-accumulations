using System;
namespace marketplace.domain.entities
{
    public class Price : Money
    {
        public new static Price Create(decimal amount, Currency currency = null) => new Price(amount, currency);
        protected Price(decimal amount,Currency currency):base(amount,currency)
        {
            if(amount<0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }
    }
}