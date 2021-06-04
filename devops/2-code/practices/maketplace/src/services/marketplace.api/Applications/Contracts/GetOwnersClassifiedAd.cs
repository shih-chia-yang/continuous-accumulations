using System;

namespace marketplace.api.Applications.Contracts
{
    public record GetOwnersClassifiedAd
    {
        public Guid OwnerId { get; init; }

        public int Page { get; init; }

        public int PageSize { get; init; }
        public GetOwnersClassifiedAd(string id,int page,int pageSize)
        {
            OwnerId = new Guid(id);
            Page = page;
            PageSize = pageSize;
        }
    }
}