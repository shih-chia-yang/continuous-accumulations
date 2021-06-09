using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.ViewModels;
using marketplace.domain.events.ClassifiedAdEvents;
using marketplace.domain.events.UserProfileEvents;

namespace marketplace.api.Infrastructure.Projections
{
    public class ClassifiedAdProjection : IProjection
    {
        private readonly IList<ClassifiedAdDetailsViewModel> _items;

        public ClassifiedAdProjection(List<ClassifiedAdDetailsViewModel> items)
        {
            _items = items;
        }
        public Task Project(object @event)
        {
            switch(@event)
            {
                case ClassifiedAdCreated e:
                    _items.Add(new ClassifiedAdDetailsViewModel
                    {
                        ClassifiedAdId = e.Id,
                        SellerId=e.OwnerId
                    });
                    break;
                case ClassifiedAdTitleChanged e:
                    UpdateItem(e.Id, ad => ad.Title = e.Title);
                    break;
                case ClassifiedAdTextUpdated e:
                    UpdateItem(e.Id, ad => ad.Description = e.Text);
                    break;
                case ClassifiedAdPriceUpdated e:
                    UpdateItem(e.Id, ad =>
                    {
                        ad.Price = e.Price;
                        ad.CurrencyCode = e.CurrencyCode;
                    });
                    break;
                case UserDisplayNameUpdated e:

                    UpdateMultipleItems(
                        x => x.SellerId == e.UserId, 
                        x => x.SellerDisplayName = e.DisplayName
                        );
                    break;
            }
            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id,Action<ClassifiedAdDetailsViewModel> update)
        {
            var item = _items.FirstOrDefault(x => x.ClassifiedAdId == id);
            if(item==null)return;
            update(item);
        }

        private void UpdateMultipleItems(
            Func<ClassifiedAdDetailsViewModel,bool> query,
            Action<ClassifiedAdDetailsViewModel> update
        )
        {
            foreach(var item in _items.Where(query))
                update(item);
        }
    }
}