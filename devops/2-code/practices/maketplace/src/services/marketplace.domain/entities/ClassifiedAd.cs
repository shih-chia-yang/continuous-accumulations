using System;
using marketplace.domain.entities;

namespace marketplace.domain
{
    public enum ClassifiedState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
    public class ClassifiedAd
    {
        public Guid Id { get; private set; }
        public UserId OwnerId { get;}
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }

        public ClassifiedState State { get; private set;}

        public UserId ApprovedBy{ get; private set;}
        public ClassifiedAd(Guid id,UserId ownerId)
        {
            if(id==default)
            {
                throw new ArgumentException("Identity must be specified", nameof(Id));
            }
            Id = id;
            OwnerId=ownerId;
            State = ClassifiedState.Inactive;
        }

        public void SetTitle(ClassifiedAdTitle title) => Title = title;

        public void UpdateText(ClassifiedAdText text) => Text = text;

        public void UpdatePrice(Price price) => Price = price;

        public void RequestToPublish()
        {
            this.State = ClassifiedState.PendingReview;
        }
    }
}
