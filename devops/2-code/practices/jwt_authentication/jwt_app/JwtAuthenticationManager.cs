using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace jwt_app.Models;

public class JwtAuthenticationManager
{
    public JwtAuthResponse Authenticate(string userName,string password)
    {
        //first validaing user name and password
        //here is hard code to verify, and you can validate username and password from database
        if(userName != "user1" || password !="password1234")
            return null;
        //declare jwt security handler
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        // create a key for encryption
        var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
        var tokenExpireTimeStamp = DateTime.UtcNow.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            //save username in the token
            //the claim will be save in  encrypted token
            //will be able to retrieve data from the token in the api request
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim("username",userName),
                new Claim(ClaimTypes.PrimaryGroupSid,"use group 01"),
            }),
            Expires=tokenExpireTimeStamp,
            SigningCredentials=new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                ),
        };

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        return new JwtAuthResponse
        {
            token = token,
            user_name = userName,
            expires_in = (int)tokenExpireTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }
}