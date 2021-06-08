using System;
using Xunit;
using marketplace.unittests.builder;
using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using marketplace.domain.events.ClassifiedAdEvents;

namespace marketplace.unittests.ClassifiedAdSpec
{
    public class PictureSpec
    {
        private readonly FakePicture _picture;

        public const string context = "Picture";

        public PictureSpec()
        {
            _picture = new FakePicture(null);
        }

        [Fact]
        [Trait(context,"new")]
        public void test_new_picture()
        {
            //Given
            var added = new PictureAdded(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Uri("https://google.com.tw").ToString(),
                801,
                601,
                1);
            //When
            _picture.Handle(added);
            //Then
            Assert.Equal(added.PictureId, _picture.Id);
            Assert.Equal(added.Url, _picture.Location.ToString());
            Assert.Equal(added.Width, _picture.Size.Width);
            Assert.Equal(added.Height, _picture.Size.Height);
            Assert.Equal(added.Order, _picture.Order);
        }

        [Fact]
        [Trait(context,"resize")]
        public void test_resize_picture()
        {
            var added = new PictureAdded(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Uri("https://google.com.tw").ToString(),
                800,
                600,
                1);
            _picture.Handle(added);
            var resize = new PictureSize(801, 601);
            //When

            _picture.Handle(new PictureResized(Guid.NewGuid(), resize.Height, resize.Width));
            //Then
            Assert.Equal(resize.Width, _picture.Size.Width);
            Assert.Equal(resize.Height, _picture.Size.Height);
        }

        [Theory]
        [Trait(context,"rule")]
        [InlineData(800,600,true)]
        [InlineData(800,599,false)]
        [InlineData(799,600,false)]
        [InlineData(799,599,false)]
        public void test_rule_when_picture_size_match_spec(int width,int height,bool expect)
        {
            //Given
            var added = new PictureAdded(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Uri("https://google.com.tw").ToString(),
                height,
                width,
                1);
            _picture.Handle(added);
            //When
            //When
            var correct=_picture.HasCorrectSize();
            //Then
            Assert.Equal(expect, correct);
        }
    }
}