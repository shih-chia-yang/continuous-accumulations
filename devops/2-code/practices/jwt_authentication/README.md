# jwt authentication

[[web_api]]
[[netcore]]

## what is jwt

JSON Web Token，一種在server與client用來安的地共享資訊的開放標準，每一個JWT包含被加密的json object與一組claims。
JWT利用加密演算法來保證在token產生之後claims無法被替換。
普遍使用方式是使用JWTs做為持有憑證，該方法在request of a client產生JWT並且簽名，使json object無法被其他人替換，
client會將JWT連同request傳送給Rest API，而rest api會使用憑證驗證JWT的payload與header，當API驗證JWT無誤後，
會取得並透過claims中的資訊允許或禁止這個client request

## first step

建立方案檔案專案
````
dotnet new sln -o jwt_authentication
cd jwt_authentication
dotnet new webapi -o jwt
dotnet sln add ./jwt/jwt.csproj
````

在api專案中，安裝libraries
````
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
````

- create JwtAuthResponse
    > 登入成功後，將claim、必要資料加密寫入token後，使用該結構回應client請求
````
[Serializable]
public class JwtAuthResponse
{
    public string token { get; set; }
    public string user_name { get; set; }
    public int expires_in { get; set; }
}
````

- create Constants
    > 建立常數，提供憑證效期與api_key
````
public class Constants
{
    public const int JWT_TOKEN_VALIDITY_MINS = 30;
    public const string JWT_SECURITY_KEY = "coding_dropLET_12345";
}
````

- 新增JwtAuthenticationManager
    >透過驗證帳號、密碼有效後，建立SecurityTokenDescriptor，並將登入資訊封裝至claims，設定登入時效、SigningCredentials
    >SecurityTokenDescriptor透過JwtSecurityHandler產生token
    >將token字串，username，expire date寫入JwtResponse，並回傳

````
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
````
- 新增AuthenticationRequest
    > 建立登入請求資料結構，為API提供外部使用參數
````
[Serializable]
public class AuthenticationRequest
{
    public string UserName { get; set; }
    public string  Password { get; set; }
}
````

- 新增AccountController
    > 新增IActionResult，當發生登入請求時，透過AuthenticationRequest，提供帳號與密碼
    > 使用JwtAuthenticationManager驗證入登入資訊，並回傳token字串
````
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
````

- 設定 Configure Service Authentication

````
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options=>{
        // options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});
builder.Services.AddAuthorization();
````

````
app.UseAuthentication();
app.UseAuthorization();
````

- 新增ArithmeticController
    > 新增Sum方法，測試授權

````
[Route("api/[controller]")]
[ApiController]
public class ArithmeticController:ControllerBase
{
    [Authorize]
    [Route("SumValues")]
    public IActionResult Sum([FromQuery(Name ="Value1")] int value1,[FromQuery(Name="Value2")]int value2)
    {
        var result = value1 + value2;
        return Ok(result);
    }
}
````

[//begin]: # "Autogenerated link references for markdown compatibility"
[web_api]: ../../learning/development/aspnet-core/project/webapi/web_api.md "web_api"
[netcore]: ../../learning/tool/dotnet/netcore.md "netcore"
[//end]: # "Autogenerated link references"