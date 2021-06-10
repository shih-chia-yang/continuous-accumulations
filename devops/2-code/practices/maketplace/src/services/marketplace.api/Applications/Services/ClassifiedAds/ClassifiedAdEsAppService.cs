using System.Diagnostics;
using System;
using System.Runtime.Intrinsics;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using marketplace.domain.kernel;
using static marketplace.api.Applications.Contracts.ClassifiedAds;
using marketplace.domain.AggregateModels;

namespace marketplace.api.Applications.Services.ClassifiedAds
{
    public class ClassifiedAdEsAppService : IClassifiedAdAppService
    {
        private readonly IAggregateStore _store;
        public ClassifiedAdEsAppService(IAggregateStore store)
        {
            _store = store;
        }
        public Task Handle(object command)
            => command switch
            {
                V1.Create cmd =>HandleCreate(cmd),
                V1.SetTitle cmd =>HandleUpdate(
                    cmd.Id,
                    c=>c.SetTitle(
                        ClassifiedAdTitle.FromString(cmd.Title)
                        )),
                V1.UpdateText cmd=>HandleUpdate(
                    cmd.Id,
                    c=>c.UpdateText(
                        ClassifiedAdText.FromString(cmd.Text)
                        )),
                V1.UpdatePrice cmd =>HandleUpdate(
                    cmd.Id,
                    c=>c.UpdatePrice(
                        Price.Create(cmd.Price,Currency.Create(cmd.Currency,2))
                        )),
                V1.RequestToPublish cmd=>HandleUpdate(
                        cmd.Id,
                        c=>c.RequestToPublish()
                        ),
                V1.Publish cmd =>HandleUpdate(
                        cmd.Id,
                        c=>c.Publish(
                            new UserId(cmd.ApprovedBy)
                            )),
                _ =>Task.CompletedTask
            };
        private async Task HandleCreate(V1.Create command)
        {
            if(await _store.Exists<ClassifiedAd,Guid>(command.Id))
            {
                throw new InvalidOperationException($"Entity with Id {command.Id} already exists");
            }
            var newClassfiedAd = new ClassifiedAd(command.Id, new UserId(command.OwnerId));
            await _store.Save<ClassifiedAd, Guid>(newClassfiedAd);
        }

        private Task HandleUpdate(Guid id, Action<ClassifiedAd> update)
            => this.HandleUpdate(_store, id, update);
    }
}