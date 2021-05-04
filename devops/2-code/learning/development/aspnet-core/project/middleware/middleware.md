# middleware

- chooses whether to pass the request to the next component in the pipeline.
- can perform work before and after the next component in the pipeline is invoked.

Request delegates are configure using `Run`、`Map` and `Use` extension methods.

1. the Run() method adds a middleware and ends the pipeline. doesn't call next middleware
> [!TIP]
> run 委派不會收到next參數，第一個run委派一律是terminal並終止pipeline
2. the Map() method adds middleware based on request path.
3. the Use() method adds a middleware , as a lambda or a dedicated class. the request delegate handler can be in-line as an anonymous method or a reusable class.

將多個request delegate 鏈結在一起的方法是使用`Use`，next參數代表pipeline 中的下一個delegate。可以不呼叫next來對pipeline執行最少運算
```aspx-csharp
app.Use(async(context,next)=>{
    //do work that don't write to the responses
    await next.Invoke();
    //do logging or other work that doesn't write to the responses
});


app.Run(async context=>{
    await context.Response.WriteAsync("hello from 2nd delegate");
});
```
## middleware sequence

Startup.Configure可定義在要求時叫用middleware的順便及回應的反向順序。對於安全性、效能和功能而言，順序非常重要。

一般建議以下列順序新增安全性相關的middleware

1. `UseExceptionHandler()`
2. `UseHSTS()`
3. `UseHttpsRedirection()`
4. `UseStaticFiles()`
5. `UseCookiePolicy()`
6. `UseRouting()`
7. `UseRequestLocalization()`
8. `UseCORS()` 
9. `UseAuthentication()`
10. `UseAuthorization()`
11. `UseSession()`
12. `UseResponseCompression()`
13. `UseResponseCaching()`
14. `UseEndPoint()` 
## UseMiddleware()

when setting up middleware as a class, UseMiddleware can be used to wire it up,providing custom middleware class as generic parameter.
- it is constructor accepts `RequestDelegate`. this will be called to forward the request to next middleware.
- it has a method `Invoke` accepting `HttpContext` and returning a `Task`. this is called by the framework when calling the middleware


## Built-in middleware

|middleware|description|order|
|--|--|--|
|Authentication|providing authentication support|Before HttpContext.UYser is needed. Terminal for OAuth callbacks.|
|Authorization|provides authentication support|immediately after the authentication middleware|
|Cookie Policy| Tracks consent from users for storing personal information and enforces minimum standards for cookie field,such as secure and SameSite| Before middleware that issues cookies .example:Authentication,session,Mvc(TempData)|
|CORS|Configure Cross-Origen Resource Sharing|Before components that use CORS|
|Diagnostics|configures Diagnostics|Before components that generate errors|
|ForwardedHeaders|forwards proxied headers onto the current request|Before components that consume the updated fields(example: schema,Host,Client IP,method)|
|Health Check|checks the health of an ASP.NET Core app and its dependencies, such as checking database availability|terminal if a request matches a health check endpoints|
|Header Propagation| Propagation HTTP headers from the incoming request to the outgoing HTTP client request|
|HTTP method override|Allows an incoming POst request to override the method| Before component that consume the updated methods.|
|HTTPS redirection|Redirect all HTTP request to HTTPS| before components that consume URL|
|HTTP Strict Transport Security(HSTS)| Security enhancement middleware that adds a special responses header| Before responses are sent and after components that modify requests. example: Forwarded Headers ,URL Rewrite|
|MVC|Processes request with MVC/Razor Pages|terminal if a request matches a route|
|OWIN|interop with OWIN-based apps, servers and middleware|terminal if the OWIN middleware fully processes the request|
|Response Caching|provides support for caching responses|Before components that require caching|
|Response Compression|provides support for compression responses|Before components that require compression|
|RequestLocalization|provides localization support|before localization sensitvie components|
|Endpoint Routing|Defines and constrains request routes|Terminal for matching routes|
|SPA| handles all requests from this point in the middleware chain by returning the default page for the Single Page Application|late in the chain,so that other middleware for serving static files,MVC actions,etc takes procedence|
|Session|provides support for managing user sessions|Before components that require Session|
|Static Files|provides support for serving static files and directory browsing|Terminal if a request matches a file.|
|URL Rewrite|provides support for rewriting URLs and redirecting requests.|
|WebSockets|Enable the WebSockets protocol|Before components that are require to accept WebSocket request|

[相關連結](https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0)