using System;

namespace marketplace.domain.events.UserProfileEvents
{
    public class UserFullNameUpdated
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }

        public UserFullNameUpdated(Guid userId,string fullName)
        {
            UserId = userId;
            FullName = fullName;
        }
    }
}