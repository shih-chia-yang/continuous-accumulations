# cors

- enabling Cross-Origin Requests(CORS)
- simple Authorization
- Enabling CORS with middleware
- app.UseCore("`<Policy name>`")
- builder.WithOrigins
- Enabling CORS in MVC
- services.AddCors
- Enabling CORS per action
- `[EnableCors("<Policy name>")]`
- Enabling CORS per controller
- Enabling CORS globally
- Disable CORS
- `[DisableCors]`

to allow clients from a different origin to access your ASP.NET Core Web API. you need to allow Cross-Origin Requests

[[launch_json]]

## same origin source

如果2個url具有相同的配置、主機、port，其來源相同

**example**

Url來源相同
- https://example.com/foo.html
- https://example.com/bar.html

Url來源不同
- https://example.net : 不同網域
- https://www.example.com/foo.html ：不同子域
- http://example.com/foo.html : 不同配置
- https://example.com:9000/foo.html : 不同的port


## 啟用CORS

- 在middleware中使用 AddPolicy或 DefaultPolicy
- 使用endpoint routing
- `使用[EnableCors]` attribute


## Enabling Cors with middleware

```aspx-csharp
public void ConfigureService(IServiceCollection services)
{
    services.AddCors();
}

public void ConfigureService(IApplicationBuilder app,IWebHostEnvironment env)
{
    app.UseCors(
    builder=>builder
        .WithOrigins("<target website>")
        .SetIsOriginAllowWeb((host)=>true);
    
    );
}
```
**上述原則會預設Cors原則套用至所有控制器端點**
**若使用[DisableCors()] attribute 可以為個別action或controller禁止Cors**

- AddPolicy

```aspx-csharp
services.AddCors(options=>
{
    options.AddPolicy(cors_middleware, 
                    builder=>
                    {
                        builder.WithOrigins("https://localhost:5001")
                        .SetIsOriginAllowed((host)=>true)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
    
});

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors(cors_middleware);
}
```
**使用命名原則時，UseCors指定policy name，為全部控制器端點允許使用指定原則的跨原始要求**
**若使用UseCors()，不指定原則情況下，則需至需要Cors的端點動作加上[EnableCors({Policy Name})]**


## enabling Cors with EndPoint route

**使用endpoint routing時 Cors middleware必須設定在UseRouting與UseEndPoint之間**

**使用每個endpoint來啟用cors不支援自動預檢要求**

使用endpoint routing時，可以使用擴充方法，根據每個endpoint來啟用Cors

```aspx-csharp

app.UseCors();


app.UseEndpoint(endpoints=>
{
    endpoint.MapGet("/echo",context=>context.Response.WriteAsync("echo")).RequireCors(<Policy Name>);

    endpoint.MapControllers().RequireCors(<Policy Name>);

    endpoint.MapGet("/echo2",context=>context.Response.WriteAsync("echo2"));

});

```

**[DisableCors]不會停用endpoint routing所啟用的Cors**

## 使用 Cors Attribute 啟用Cors

`[EnableCors]`是提供全域套用CORS的替代方案。
使用`[EnableCors]`啟用CORS, 將命名原則套用至需要的cors端點，可提供最精確的控制
`[EnableCors]`針對選取的endpoint啟用CORS，而不是所有endpoint

- `[EnableCors]` 指定預設原則
- `[EnableCors("{Policy Name}")]`指定命名原則

## [DisableCors]

`[DisableCors]` 不會停用endpoint routing 已啟用的CORS

## 自動預檢要求

如果下列所有條件都成立，瀏覽器可以略過預檢要求

1. 要求方法為GET、HEAD或POST
2. 應用程式不會設定`Accept`，`Accept-Language`, `Content-Language`，`Content-Type`或以外的 request header `Last-Event-ID`
3. `Content-Type`如果設定，標頭會有下列其中一個值：
    1. `application/x-www-form-urlencoded`
    2. `multipart/form-data`
    3. `text/plain`

## Troubleshooting and Solution

the ssl connection could not be established, see inner exception.

[Trusting a self signed certificate](https://bbs.archlinux.org/viewtopic.php?id=251330)

[ trust self-signed SSL certificate](https://bbs.archlinux.org/viewtopic.php?pid=1776753#p1776753)
## 相關資源

[跨資源共享](https://developer.mozilla.org/zh-TW/docs/Web/HTTP/CORS)

[在 ASPNET Core 中啟用 CORS 的跨原始來源要求](https://docs.microsoft.com/zh-tw/aspnet/core/security/cors?view=aspnetcore-5.0)

[ASP.NET Core API - Allow CORS requests from any origin and with credentials](https://jasonwatmore.com/post/2020/05/20/aspnet-core-api-allow-cors-requests-from-any-origin-and-with-credentials)

[//begin]: # "Autogenerated link references for markdown compatibility"
[launch_json]: launch_json.md "launch_json"
[//end]: # "Autogenerated link references"