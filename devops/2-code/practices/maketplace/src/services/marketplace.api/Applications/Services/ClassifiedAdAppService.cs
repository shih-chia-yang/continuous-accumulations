using System;
using System.Runtime.Intrinsics;
using System.Threading.Tasks;
using marketplace.domain;
using marketplace.domain.entities;
using marketplace.domain.repositories;
using static marketplace.api.Applications.Contracts.ClassifiedAds;

namespace marketplace.api.Applications.Services
{
    public class ClassifiedAdAppService : IAppService
    {
        private readonly IClassifiedAdRepository _repo;
        public ClassifiedAdAppService(IClassifiedAdRepository repo)
        {
            _repo = repo;
        }
        public Task Handle(object command) =>
        command switch
        {
            V1.Create cmd => Created(cmd),
            V1.SetTitle cmd => SetTileAsync(cmd),
            V1.UpdateText cmd => UpdateTextAsync(cmd),
            V1.UpdatePrice cmd => UpdatePriceAsync(cmd),
            V1.RequestToPublish cmd => RequestToPublishAsync(cmd),
            _ => Task.CompletedTask
        };

        private async Task RequestToPublishAsync(V1.RequestToPublish cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.RequestToPublish();
            await _repo.UnitOfWork.Commit();
        }

        private async Task UpdatePriceAsync(V1.UpdatePrice cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.UpdatePrice(Price.Create(cmd.Price, Currency.Create(cmd.Currency, 2)));
            await _repo.UnitOfWork.Commit();
        }

        private async Task<ClassifiedAd> FindAsync(string id)
        {
            var entity = await _repo.Load(id);
            if(entity==null)
                throw new InvalidOperationException($"Entity with id {id} cannot be found");
            return entity;
        }

        private async Task UpdateTextAsync(V1.UpdateText cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.UpdateText(ClassifiedAdText.FromString(cmd.Text));
            await _repo.UnitOfWork.Commit();
        }


        private async Task SetTileAsync(V1.SetTitle cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));
            await _repo.UnitOfWork.Commit();
        }

        private async Task Created(V1.Create cmd)
        {
            if(await _repo.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exist");
            var classifiedAd = new ClassifiedAd(cmd.Id, new UserId(cmd.OwnerId));
            await _repo.Add(classifiedAd);
            await _repo.UnitOfWork.Commit();
        }
    }
}