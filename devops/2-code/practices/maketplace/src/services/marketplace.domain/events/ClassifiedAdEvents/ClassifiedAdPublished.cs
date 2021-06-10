using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdPublished
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public string SellerPhotoUrl{ get; set; }
        public Guid ApprovedBy { get; set; }

        public ClassifiedAdPublished()
        {
            
        }
        public ClassifiedAdPublished(Guid id,Guid ownerId,Guid approveBy)
        {
            Id = id;
            OwnerId = ownerId;
            ApprovedBy = approveBy;
        }
    }
}