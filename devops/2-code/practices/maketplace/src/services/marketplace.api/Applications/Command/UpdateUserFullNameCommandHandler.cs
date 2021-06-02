using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
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

        private readonly IUserRepository _repo;
        public UpdateUserFullNameCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task Handle(UpdateUserFullNameCommand command)
        {
            var user = await _repo.LoadAsync(new UserId(command.UserId));
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateFullName(FullName.FromString(command.FullName));
            await _repo.UnitOfWork.Commit();
        }
    }
}