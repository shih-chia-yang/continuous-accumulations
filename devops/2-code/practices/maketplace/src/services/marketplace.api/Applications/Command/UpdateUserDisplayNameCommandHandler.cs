using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;

namespace marketplace.api.Applications.Command
{
    public class UpdateUserDisplayNameCommand
    {
        public Guid UserId { get; set; }
        public string DislayName { get; set; }
    }

    public class UpdateUserDisplayNameCommandHandler : ICommandHandler<UpdateUserDisplayNameCommand>
    {
        private readonly IUserRepository _repo;
        private readonly CheckTextForProfanity _checkText;
        public UpdateUserDisplayNameCommandHandler(
            IUserRepository repo,
            CheckTextForProfanity checkText
        )
        {
            _repo = repo;
            _checkText = checkText;
        }
        public async Task Handle(UpdateUserDisplayNameCommand command)
        {
            var user = await _repo.LoadAsync(new UserId(command.UserId));
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateDisplayName(DisplayName.FromString(command.DislayName,_checkText));
            await _repo.UnitOfWork.Commit();
        }
    }
}