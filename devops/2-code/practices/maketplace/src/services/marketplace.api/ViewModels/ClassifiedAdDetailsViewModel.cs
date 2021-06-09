using System;

namespace marketplace.api.ViewModels
{
    public class ClassifiedAdDetailsViewModel
    {
        public Guid ClassifiedAdId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }
        
        public string Description { get; set; }

        public Guid SellerId { get; set;}

        public string  SellerDisplayName { get; set; }

        public string[] PhotoUrls { get; set; }
    }
}