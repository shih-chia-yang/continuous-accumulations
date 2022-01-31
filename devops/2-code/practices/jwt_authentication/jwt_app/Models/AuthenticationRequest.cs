namespace jwt_app.Models;

[Serializable]
public class AuthenticationRequest
{
    public string UserName { get; set; }
    public string  Password { get; set; }
}