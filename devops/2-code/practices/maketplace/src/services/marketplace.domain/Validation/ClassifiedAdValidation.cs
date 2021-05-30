using System.Diagnostics;
using System.Linq;
using marketplace.domain.entities;
using marketplace.domain.exceptions;
using marketplace.domain.kernel;

namespace marketplace.domain.Validation
{
    public class ClassifiedAdValidation : VaildationContext<ClassifiedAd>
    {
        public ClassifiedAdValidation(ClassifiedAd entity):base(entity)
        {
            
        }
        public override bool Validate()
        {
            var valid = IdIsNotNull() 
                && OwnerIdIsNotNull() 
                &&(Entity.State switch{
                    ClassifiedState.PendingReview=>
                        TitleIsNotNull() 
                        && TextIsNotNull() 
                        && AmountIsNotZero()
                        && PicturesHasCorrectSize(),
                    ClassifiedState.Active=>
                        TitleIsNotNull() 
                        && TextIsNotNull()
                        && AmountIsNotZero() 
                        && ApprovedByIsNotNull()
                        && PicturesHasCorrectSize(),
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

        /// <summary>
        /// 以合約方法逐條撰寫
        /// </summary>
        /// <returns></returns>
        public bool IdIsNotNull()=>Entity.Id ==default?false:true;
        public bool OwnerIdIsNotNull()=>Entity.OwnerId ==null?false:true;
        public bool TitleIsNotNull() => Entity.Title==null?false:true;
        public bool TextIsNotNull() => Entity.Text == null ? false : true;
        public bool AmountIsNotZero() => Entity.Price?.Amount >0 ? true : false;
        public bool ApprovedByIsNotNull() => Entity.ApprovedBy == null ? false : true;

        public bool PicturesHasCorrectSize() => Entity.Pictures.All(x => x.HasCorrectSize() == true);
    }
}