using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.ViewModels;
using marketplace.domain.events.UserProfileEvents;

namespace marketplace.api.Infrastructure.Projections
{
    public class UserDetailsProjection : IProjection
    {
        private IList<UserDetailsViewModel> _items;

        public UserDetailsProjection(List<UserDetailsViewModel> items)
        {
            _items = items;
        }

        public Task Project(object @event)
        {
            switch(@event)
            {
                case UserRegistered e:
                    _items.Add(new UserDetailsViewModel(e.UserId, e.DisplayName, ""));
                    break;
                case UserDisplayNameUpdated e:
                    UpdateItem(e.UserId, x => x.DisplayName = e.DisplayName);
                    break;
                case ProfilePhotoUploaded e:
                    UpdateItem(
                        e.UserId,
                        x => x.PhotoUrl = e.PhotoUrl
                    );
                    break;
            }
            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id,Action<UserDetailsViewModel> update)
        {
            var item = _items.FirstOrDefault(x => x.UserId == id);
            if(item==null)return;
            update(item);
        }
    }
}