using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;

namespace marketplace.api.Applications.Command
{
    public class UpdateUserProfilePhotoCommand
    {
        public Guid UserId { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class UpdateUserProfilePhotoCommandHandler : ICommandHandler<UpdateUserProfilePhotoCommand>
    {
        private readonly IUserRepository _repo;

        private readonly IAggregateStore _store;
        public UpdateUserProfilePhotoCommandHandler(
            IUserRepository repo,
            IAggregateStore store)
        {
            _repo = repo;
            _store = store;
        }
        public async Task Handle(UpdateUserProfilePhotoCommand command)
        {
            // var user = await _repo.LoadAsync(new UserId(command.UserId));
            var user = await _store.Load<UserProfile, Guid>(command.UserId);
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateProfilePhoto(new Uri(command.PhotoUrl));
            await _store.Save<UserProfile, Guid>(user);
            // await _repo.UnitOfWork.Commit();
        }
    }
}