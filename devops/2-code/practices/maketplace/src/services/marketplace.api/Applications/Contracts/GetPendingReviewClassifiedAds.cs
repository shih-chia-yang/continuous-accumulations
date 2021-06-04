namespace marketplace.api.Applications.Contracts
{
    public record GetPendingReviewClassifiedAds
    {
        public int Page { get; init;}
        public int PageSize { get; init; }
    }
}