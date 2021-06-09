using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;
using marketplace.domain.kernel;

namespace marketplace.api.Applications.Command
{
    public class UpdateUserDisplayNameCommand
    {
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
    }

    public class UpdateUserDisplayNameCommandHandler : ICommandHandler<UpdateUserDisplayNameCommand>
    {
        private readonly IAggregateStore _store;
        private readonly IUserRepository _repo;
        private readonly CheckTextForProfanity _checkText;
        public UpdateUserDisplayNameCommandHandler(
            IUserRepository repo,
            IAggregateStore store,
            CheckTextForProfanity checkText
        )
        {
            _repo = repo;
            _store = store;
            _checkText = checkText;
        }
        public async Task Handle(UpdateUserDisplayNameCommand command)
        {
            // var user = await _repo.LoadAsync(new UserId(command.UserId));
            var user = await _store.Load<UserProfile, Guid>(command.UserId);
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateDisplayName(DisplayName.FromString(command.DisplayName,_checkText));
            await _store.Save<UserProfile, Guid>(user);
            // await _repo.UnitOfWork.Commit();
        }
    }
}