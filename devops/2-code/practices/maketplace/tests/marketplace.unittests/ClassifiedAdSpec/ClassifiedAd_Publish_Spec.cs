using System;
using marketplace.domain;
using marketplace.domain.entities;
using marketplace.domain.exceptions;
using marketplace.domain.Validation;
using Xunit;

namespace marketplace.unittests.ClassifiedAdSpec
{
    public class ClassifiedAd_Publish_Spec
    {
        private readonly ClassifiedAd _classifiedAd;
        public ClassifiedAd_Publish_Spec()
        {
            _classifiedAd = new ClassifiedAd(Guid.NewGuid(), new UserId(Guid.NewGuid()));
        }

        [Fact]
        [Trait("ClassifiedAd","publish")]
        public void test_publish_a_valid_ad_should_be_success()
        {
            //Given
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("covid-19 comming"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("keeping social distancing"));
            _classifiedAd.UpdatePrice(Price.Create(100M, Currency.Default));
            //When
            _classifiedAd.RequestToPublish();
            //Then
            Assert.Equal(ClassifiedState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        [Trait("ClassifiedAd","null_of_title")]
        public void test_when_publish_without_title_should_throw_exception()
        {
            //Given
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("keeping social distancing"));
            _classifiedAd.UpdatePrice(Price.Create(100M, Currency.Default));
            //When
            Action publish=()=>_classifiedAd.RequestToPublish();
            //Then
            Assert.Throws<InvalidEntityStateException>(publish);
        }

        [Fact]
        [Trait("ClassifiedAd","null_of_text")]
        public void test_publish_without_text_should_throw_exception()
        {
            //Given
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("covid-19 comming"));
            _classifiedAd.UpdatePrice(Price.Create(100M, Currency.Default));
            //When
            Action publish=()=>_classifiedAd.RequestToPublish();
            //Then
            Assert.Throws<InvalidEntityStateException>(publish);
        }

        [Fact]
        [Trait("ClassifiedAd","null_price")]
        public void test_publish_without_price_should_throw_exception()
        {
            //Given
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("covid-19 comming"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("keeping social distancing"));
            //When
            Action publish=()=>_classifiedAd.RequestToPublish();
            //Then
            Assert.Throws<InvalidEntityStateException>(publish);
        }

        [Fact]
        [Trait("ClassifiedAd","zero_price")]
        public void test_publish_with_zero_price_should_throw_exception()
        {
            //Given
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("covid-19 comming"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("keeping social distancing"));
            _classifiedAd.UpdatePrice(Price.Create(0, Currency.Default));
            //When
            Action publish=()=>_classifiedAd.RequestToPublish();
            //Then
            Assert.Throws<InvalidEntityStateException>(publish);
        }
    }
}