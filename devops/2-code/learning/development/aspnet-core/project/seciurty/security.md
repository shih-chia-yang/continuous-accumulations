# security

- security
- Authentication
- authorization
- prevent SQL Injection attacks
- Implementing Authentication examples
- ClaimsPrincipal
- HttpContext.User
- Cookie based Authentication
- Cookie Expiration
- Persistent cookies
- Absolute cookie expiration


## prevent SQL injection attacks

- data input must be sanitized
- Sensitive data must be encrypted and Protected
- Accessing to the database using an account with the least privileges are necessary
- install the database using an account with the least privileges are necessary
- Validate the data and ensure that data is valid
- Do a code review to check for the possibility of second-order attacks
- Use parameterized queries
- Use stored procedures
- Re-Validate data in stored procedures
- Ensure that error message give nothing away about the internal architecture of the application or the database
- Use service layer when is possible to decouple database


## implementing Authentication

1. create project name it SaAuthentication, folder structure:
    - Controller
    - Models
    - Security
    - Views
    - Home
2. before endpoint routing,add `UseAuthentication` and after `UseRouting`.
3. add SecurityController
4. add LoginModel
    
    ```aspx-csharp
    public enum Roles
    {
        User,Admin
    }

    public class LoginModel
    {
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string RequestPath { get;set;}
        
        public Roles Role {get;set;}
    }
    ```
5. add Login.cshtml
    1. add email text and label
    2. add password text and label
    3. add submit button

6. add Access.cshtml
7. add Index.cshtml,show login information & logout hyperlink
8. add validate & login & logout method in SecurityController
    1. add Login method
    2. add Post/Login method
    3. add Logout method
    4. add IsAuthentic method

Authentication middleware intercepts incoming request and checks for the existing of a cookie that holding encrypted user data.
to create a cookie holding user information,you must construct a `ClaimsPrincipal`.the user information is serialized and stored in the cookie.
    - if a cookie is found,it will be serialized into a `ClaimsPrincipal` type and can be accessed via `HttpContext.User` property.
    - if a cookie is not found,middleware redirect to the login page

```aspx-csharp
ClaimsIdentity identity = new ClaimsIdentity(claims,"cookie");
```
initialize a new instance of the `ClaimsIdentity` with the specified claim and the authentication type which our case we are setting a cookie authentication.

```aspx-csharp
ClaimsPrincipal principal = new ClaimsPrincipal(identity);
```
initialize a new instance of the `ClaimsPrincipal` class from the specified identity

### Logout

calling the `HttpContext.SignOutAsync` deletes the authentication cookie

## add [Authorize] at controller or action

**Cookie Expiration**

`ExpiresUtc =DateTimeOffset.UtcNow.AddMinutes(15)`
the time at which the authentication ticket expires.
a value set here override the 'ExpireTimeSpan' option of `CookieAuthenticationOptions` set with AddCookie

**IsPersistent=true**
whether the authentication session is persisted across multiple requests.
Required when setting the `ExpireTimeSpan` option of `CookieAuthenticationOptions` set with AddCookie
required when setting ExpiresUtc

**Persistent cookie**
this persistance should only be enabled with explicit user consent with a `Remember Me` checkbox on login or a similar mechanism

**ExpiresUtc = DateTime.UtcNow.AddMinutes(15)**

create cookie that lasts for 15 min. this will ignore any sliding expiration settings previously configured