using System.Linq;
using marketplace.domain.exceptions;
using marketplace.domain.kernal;

namespace marketplace.domain.Validation
{
    public class ClassifiedAdValidation : VaildationContext<ClassifiedAd>
    {
        public ClassifiedAdValidation(ClassifiedAd entity):base(entity)
        {
            
        }
        public override bool Validate()
        {
            var valid = Entity.Id != null && Entity.OwnerId != null &&
            (Entity.State switch{
                ClassifiedState.PendingReview=>
                    Entity.Title!=null && Entity.Text!=null && Entity.Price?.Amount==0,
                ClassifiedState.Active=>
                    Entity.Title!=null && Entity.Text!=null 
                    && Entity.Price?.Amount==0 && Entity.ApprovedBy !=null,
                _ => true
            });
            if(!valid)
                throw new InvalidEntityStateException(Entity,$"Post-checks failed in state {Entity.State}");
            if(Entity.Title==null)
            {
                base.AddError(nameof(Entity.Title), "title cannot be null");
            }
            if(Entity.Text==null)
            {
                base.AddError(nameof(Entity.Text), "text cannot be null");
            }
            if(Entity.Price?.Amount==0)
            {
                base.AddError(nameof(Entity.Price), "price cannot be zero");
            }
            return base.Errors.Count() > 0 ? false : true;
        }
    }
}