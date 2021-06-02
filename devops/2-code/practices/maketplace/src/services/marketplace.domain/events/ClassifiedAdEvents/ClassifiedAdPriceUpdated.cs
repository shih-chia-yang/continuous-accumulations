using System;

namespace marketplace.domain.events
{
    public class ClassifiedAdPriceUpdated
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }

        public string CurrencyCode{ get; set; }

        public ClassifiedAdPriceUpdated(Guid id,decimal price,string currencyCode)
        {
            Id = id;
            Price = price;
            CurrencyCode = currencyCode;
        }
    }
}