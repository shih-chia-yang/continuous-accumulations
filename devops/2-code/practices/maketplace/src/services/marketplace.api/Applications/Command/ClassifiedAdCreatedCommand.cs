using System;
using System.Threading.Tasks;
using marketplace.api.Applications.Contracts;
using marketplace.domain;
using marketplace.domain.entities;
using marketplace.domain.kernal.commands;
using marketplace.domain.repositories;

namespace marketplace.api.Applications.Command
{
    public class ClassifiedAdCreatedCommand:ICommandHandler< ClassifiedAds.V1.Create>
    {
        private readonly IClassifiedAdRepository _repo;
        public ClassifiedAdCreatedCommand(IClassifiedAdRepository repo)
        {
            _repo = repo;
        }
        public Task Handle (ClassifiedAds.V1.Create command)
        {
            var classifiedAd = new ClassifiedAd(Guid.NewGuid(), new UserId(Guid.NewGuid()));
            return _repo.Save(classifiedAd);
        }
    }
}