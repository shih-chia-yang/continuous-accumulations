using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;

namespace marketplace.api.Applications.Command
{
    public class RegisterUserCommand
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }

        public string DisplayName { get; set; }
    }
    
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _repo;

        private readonly CheckTextForProfanity _checkText;
        public RegisterUserCommandHandler(
            IUserRepository repo,
            CheckTextForProfanity checkText)
        {
            _repo = repo;
            _checkText = checkText;
        }
        public async Task Handle(RegisterUserCommand command)
        {
            if (await _repo.ExistsAsync(command.UserId))
            {
                throw new InvalidOperationException($"Entity with id {command.UserId} already exists");
            }
            var user = new UserProfile(
                new UserId(command.UserId),
                FullName.FromString(command.FullName),
                DisplayName.FromString(command.DisplayName, _checkText));
            await _repo.AddAsync(user);
            await _repo.UnitOfWork.Commit();
        }
    }
}