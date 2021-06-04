using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications.Command;
using marketplace.api.Registry;
using marketplace.domain.kernel.commands;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace marketplace.api.Controllers
{
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private static ILogger Log = Serilog.Log.ForContext<UserProfileController>();
        private readonly ICommandHandler<RegisterUserCommand> _regisertUser;
        private readonly ICommandHandler<UpdateUserFullNameCommand> _updateFullName;
        private readonly ICommandHandler<UpdateUserDisplayNameCommand> _updateDisplayName;
        private readonly ICommandHandler<UpdateUserProfilePhotoCommand> _updateProfilePhoto;
        public UserProfileController(
            ICommandHandler<RegisterUserCommand> registerUser,
            ICommandHandler<UpdateUserFullNameCommand> updateFullName,
            ICommandHandler<UpdateUserDisplayNameCommand> updateDisplayName,
            ICommandHandler<UpdateUserProfilePhotoCommand> updateProfilePhoto
        )
        {
            _regisertUser = registerUser ?? throw new ArgumentNullException(nameof(registerUser));
            _updateFullName = updateFullName ?? throw new ArgumentNullException(nameof(updateFullName));
            _updateDisplayName = updateDisplayName ?? throw new ArgumentNullException(nameof(updateDisplayName));
            _updateProfilePhoto = updateProfilePhoto ?? throw new ArgumentNullException(nameof(updateProfilePhoto));
        }

        [HttpPost]
        public Task<IActionResult> Register(RegisterUserCommand request) =>
            RequestHandler.HandleRequest(request, _regisertUser.Handle,Log);

        [Route("fullname")]
        [HttpPut]
        public Task<IActionResult> UpdateFullName(UpdateUserFullNameCommand request)
            => RequestHandler.HandleRequest(request, _updateFullName.Handle, Log);

        [Route("displayname")]
        [HttpPut]
        public Task<IActionResult> UpdateDisplayName(UpdateUserDisplayNameCommand request)
            => RequestHandler.HandleRequest(request, _updateDisplayName.Handle, Log);

        [Route("photo")]
        [HttpPut]
        public Task<IActionResult> UpdateProfiePhoto(UpdateUserProfilePhotoCommand request)
            => RequestHandler.HandleRequest(request, _updateProfilePhoto.Handle, Log);
    }
}