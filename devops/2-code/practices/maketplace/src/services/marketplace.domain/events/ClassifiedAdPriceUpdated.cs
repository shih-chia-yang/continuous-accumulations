using System;

namespace marketplace.domain.events
{
    public class ClassifiedAdPriceUpdated
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }

        public string CurrencyCode{ get; set; }
    }
}