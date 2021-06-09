using System;

namespace marketplace.api.ViewModels
{
    public class UserDetailsViewModel
    {
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }

        public UserDetailsViewModel(Guid userId,string displayName,string photoUrl)
        {
            UserId = userId;
            DisplayName = displayName;
            PhotoUrl = photoUrl;
        }
    }
}