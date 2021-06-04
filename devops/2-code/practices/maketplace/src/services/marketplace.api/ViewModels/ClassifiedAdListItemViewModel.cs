using System;

namespace marketplace.api.ViewModels
{
    public class ClassifiedAdListItemViewModel
    {
        public Guid ClassifiedAdId { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }

        public string PhotoUrl { get; set; }
    }
}