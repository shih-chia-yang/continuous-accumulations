using System;

namespace marketplace.api.Infrastructure.Projections
{
    public static class ClassifiedAdUpcastedEvents
    {
        public static class V1
        {

            public class ClassifiedAdPublished
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }

                public string SellerPhotoUrl { get; set; }

                public Guid ApprovedBy { get; set; }
            }
        }
    }
}