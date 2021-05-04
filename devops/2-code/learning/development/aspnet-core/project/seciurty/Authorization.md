# Authentication

## [Authorize] attribute

1. 在`Controller` or `Action` 加上`[Authorize]`可限制任何被驗證使用者存取

2. `[AllowAnonymous]` attribute to allow access by non-authenticated users to individual actions.

## Claims-Based Authorization

ASP.NET Core can be achieved by first assigning claims to the user,and then base on those claims defining policies to determine user permissions

**Claims**
a claims is a name value pair that represents what the subject is,not what the subject can do.

在驗證流程中將`Claims`分配給`ClaimsIdentity`，再由`ClaimsIdentity`分配給`ClaimsPrincipal`

Claim can contain information about the user like their name,email,address ,birth date etc

**Policy**

Policy is what the user is allowed to do,it is the permission rule.

Developers add policies when Configuring `Authorization service` in `Startup.cs`

**Adding claims checks**

Claim based authorization checks are declarative

針對controller,或是controller中的action去識別使用者那些claims為必要，那些為選擇性的

claims必須保留才能對資源進行訪問或存取

- 簡易的claim policy只尋找claim是否存在，而不檢查其值。

## building claim and registering policies

```aspx-csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    ...
    services.AddAuthentication(options=>{
        options.AddPolicy("Manager",policy=>policy.RequireClaim("CanManaged"));
    });
}
```
Manager Policy會尋找current identity的claims中是否有`CanManaged`

`[Authorize]`中的Policy property可指定 特定policy name，而`[Authorize]`可以加在controller或是action

```aspx-csharp
[Authorize(Policy="Manager")]
public class ManagerController:Controller
{

}
```

以上的例子中，controller內都會受到`Authorize`所保護，如果有個別動作不要特別驗證，可以加上`[AllowAnonymous]`

```aspx-csharp
[Authorize(Policy="Manager")]
public class ManagerController:Controller
{

    [AllowAnonymous]
    public IActionResult List()
    {
    }
}
```

## implementing claims policy,test simple claim policy

1. add User Model
2. add IUserRepository & UserRepository
3. initialize UserRepository
4. register UserRepository to DI 
5. modify IsAuthentic ，改由UserRepository取得使用者資料
6. modify Login 當CanManaged=true時，增加claim `CanManaged`
7. HomeController add `[Authorize(Policy="Manager")]`
8. Privacy action add `[AllowAnonymous]` 



## Multiple Policy Evaluation

when multiple policies are applied on a controller/action, they from `AND` logical operation

1. add Admin Claim Policy
將policy 設定檢查值新增 Id，並設值"1"

```aspx-csharp
options.AddPolicy("Admin",policy=>policy.RequireClaim("Id","1"));
```
2. HomeController add About action and add `[Authorize(Policy="Admin")]`

3. if we want to access About action ,must be have `Manager` & `Admin` policy holder

## writing Custom Policy Handler

an authorization requirement is a collection of data parameter that a policy can use to evaluate the current user principal

implements `IAuthorizationRequirement`, which is an empty marker interface.

1. create a `Admin` claims policy at action
    
    ```aspx-csharp
    [Authorize(Policy="Admin")]
    ```

2. create `ManagerRequirement` inherited from `IAuthorizationRequirement`
    
    ```aspx-csharp
    public class ManagerRequirement : IAuthorizationRequirement
    {
        public bool IsAdmin { get; set; }
        public ManagerRequirement(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
    ```

3. create `ManagerRequirementHandler` inherited from `AuthorizationHandler<ManagerRequirement>`
    
    ```aspx-csharp
    public class ManagerRequirementHandler : AuthorizationHandler<ManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRequirement requirement)
        {
            if(!context.User.HasClaim(c=>c.Type=="CanManaged"))
                return Task.CompletedTask;
            bool isAdmin=Convert.ToBoolean(context.User.FindFirst(c=>c.Type=="CanManaged").Value);
            if(isAdmin==requirement.IsAdmin)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    ```

4. add policy and Requirement
    
    ```aspx-csharp
    options.AddPolicy("Admin",policy=>policy.AddRequirements(new ManagerRequirement(true)));
    ```

5. register ManagerRequirementHandler to DI
    
    ```aspx-csharp
    services.AddScoped<IAuthorizationHandler, ManagerRequirementHandler>();
    ```

6. test ManagerRequirement
    1. test user:stone expected result is can access, then execute result was access successfully
    2. test user:john expected result is can't access, then execute result was access denied

## Handler Registration

1. add PayExpense folder
    - add ManagerPayExpenseRequirement.cs
    - add ManagerPayExpenseRequirementHandler.cs

2. Startup.cs add ClaimPolicy

    ```aspx-csharp
    options.AddPolicy("HasExpenseCredit", policy => policy.AddRequirements(new ManagerPayExpenseRequirement()));
    ```

3. ManagerPayExpenseRequirementHandler Register DI

4. ManagerController add PayExpense action
    
    ```aspx-csharp
        public async Task<IActionResult> PayExpense(User inputModel)
        {
            var result = await _authorizationService.AuthorizeAsync(User, inputModel, "HasExpenseCredit");
            if(!result.Succeeded)
                return Forbid();
            return View();
        }
    ```

5. ManagerController Index.cshtml add PayExpense link
    
    ```aspx-csharp
    <div class="form-group">
        @Html.Label("Pay Expenses","PayExpense",new {@class="col-md-2 control-lable"})
            <div class="col-md-10">
                <a asp-control="Manager" 
                asp-action="PayExpense" 
                asp-route-Name=@Context.Session.GetString("userName")
                asp-route-HasExpenseCredit="true">Pay Expense</a>
            </div>    
        </div>
    ```

6. add PayExpense.cshtml
    
    ```aspx-csharp
    @using Microsoft.AspNetCore.Http
    @inject Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService
    @{
        ViewData["Title"] = "PayExpense";
    }
    <h2>Manager Portal</h2>
    <h3>You can PayExpenses - You are manager with Expense Credit</h1>
    <a asp-controller="Home" asp-action="Index">back to home</a>
    <a asp-controller="Security" asp-action="Logout">Logout</a>
    ```