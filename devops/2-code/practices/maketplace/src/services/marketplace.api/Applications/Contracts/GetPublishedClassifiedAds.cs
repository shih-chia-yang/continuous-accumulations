namespace marketplace.api.Applications.Contracts
{
    public record GetPublishedClassifiedAds
    {
        public int Page { get; init;}
        public int PageSize { get; init; }
    }
}