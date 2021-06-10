using System;

namespace marketplace.api.Infrastructure.Projections
{
    public class PhotoUrlUpdatedIntegrationEvent
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        public string SellerPhotoUrl { get; set; }

        public Guid ApprovedBy { get; set; }
    }
}