using jwt_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_app.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController:ControllerBase
{
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public IActionResult Login([FromForm]AuthenticationRequest request)
    {
        var JwtAuthenticationManager = new JwtAuthenticationManager();
        var authResult = JwtAuthenticationManager.Authenticate(request.UserName, request.Password);
        if (authResult ==null)
            return Unauthorized();
        return Ok(authResult);
    }
}