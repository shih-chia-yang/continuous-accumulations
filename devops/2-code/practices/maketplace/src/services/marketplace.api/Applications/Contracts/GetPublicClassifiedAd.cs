using System;

namespace marketplace.api.Applications.Contracts
{
    public record GetPublicClassifiedAd
    {
        public Guid ClassifiedAdId{ get; init; }
    }
}