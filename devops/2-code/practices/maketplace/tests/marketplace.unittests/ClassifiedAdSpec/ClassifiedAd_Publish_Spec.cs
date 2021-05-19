using System;
using marketplace.domain;
using marketplace.domain.entities;

namespace marketplace.unittests.ClassifiedAdSpec
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;
        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(Guid.NewGuid(), new UserId(Guid.NewGuid()));
        }
    }
}