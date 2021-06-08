using System.Collections.Generic;
using System.Linq;
using marketplace.api.Applications.Contracts;
using marketplace.api.ViewModels;

namespace marketplace.api.Applications.Queries
{
    public static class Queries
    {
        public static ClassifiedAdDetailsViewModel Query(this IEnumerable<ClassifiedAdDetailsViewModel> items,GetPublicClassifiedAd query)
            => items.FirstOrDefault(x => x.ClassifiedAdId == query.ClassifiedAdId);
        
    }
}