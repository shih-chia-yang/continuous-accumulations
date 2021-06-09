using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;

namespace marketplace.api.Applications.Command
{
    public class UpdateUserFullNameCommand
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }

    public class UpdateUserFullNameCommandHandler : ICommandHandler<UpdateUserFullNameCommand>
    {
        private readonly IAggregateStore _store;
        private readonly IUserRepository _repo;
        public UpdateUserFullNameCommandHandler(
            IUserRepository repo,
            IAggregateStore store)
        {
            _repo = repo;
            _store = store;
        }
        public async Task Handle(UpdateUserFullNameCommand command)
        {
            // var user = await _repo.LoadAsync(new UserId(command.UserId));
            var user = await _store.Load<UserProfile, Guid>(command.UserId);
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateFullName(FullName.FromString(command.FullName));
            await _store.Save<UserProfile, Guid>(user);
            // await _repo.UnitOfWork.Commit();
        }
    }
}