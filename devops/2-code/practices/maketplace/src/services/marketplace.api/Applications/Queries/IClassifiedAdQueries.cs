using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using marketplace.api.Applications.Contracts;
using marketplace.api.ViewModels;

namespace marketplace.api.Applications.Queries
{
    public interface IClassifiedAdQueries
    {
        Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetPublishedClassifiedAds query);

        Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetPendingReviewClassifiedAds query);

        Task<ClassifiedAdDetailsViewModel> Query(GetPublicClassifiedAd query);

        Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetOwnersClassifiedAd query);
    }
}