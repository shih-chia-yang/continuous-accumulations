using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
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
        public UpdateUserProfilePhotoCommandHandler(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task Handle(UpdateUserProfilePhotoCommand command)
        {
            var user = await _repo.LoadAsync(new UserId(command.UserId));
            if (user == null)
                throw new InvalidOperationException(
                    $"Entity with id {command.UserId} cannot be found"
                );
            user.UpdateProfilePhoto(new Uri(command.PhotoUrl));
            await _repo.UnitOfWork.Commit();
        }
    }
}