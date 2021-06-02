using System;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using Xunit;

namespace marketplace.unittests.Domain
{
    public class UserAggregateTests
    {
        public const string Context = "User";

        private readonly UserProfile _fakeUser;
        public UserAggregateTests()
        {
            _fakeUser = new UserProfile(
                new UserId(Guid.NewGuid()),
                FullName.FromString("fake-full"),
                new DisplayName("fake-display"));
        }

        [Fact]
        [Trait(Context,"new")]
        public void test_new_user_profile()
        {
            //Given
            var userId = new UserId(Guid.NewGuid());
            var fullName = FullName.FromString("test-full");
            var displayName = new DisplayName("test-display");
            //When
            var user = new UserProfile(
                userId,
                fullName,
                displayName
                );
            //Then
            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(fullName, user.FullName);
            Assert.Equal(displayName, user.DisplayName);
        }

        [Fact]
        [Trait(Context,"update_full_name")]
        public void test_update_full_name()
        {
            //Given
            var fullName = FullName.FromString("updated-full");
            //When
            _fakeUser.UpdateFullName(fullName);
            //Then
            Assert.Equal(fullName, _fakeUser.FullName);
        }

        [Fact]
        [Trait(Context,"update_display_name")]
        public void test_update_display_name()
        {
            //Given
            var displayName = new DisplayName("updated-display");
            //When
            _fakeUser.UpdateDisplayName(displayName);
            //Then
            Assert.Equal(displayName, _fakeUser.DisplayName);
        }

        [Fact]
        [Trait(Context,"update_photo_url")]
        public void test_update_photo_url()
        {
            //Given
            var url = new Uri("https://google.com.tw");
            //When
            _fakeUser.UpdateProfilePhoto(url);
            //Then
            Assert.Equal(url.ToString(), _fakeUser.PhotoUrl);
        }
    }
}