# middleware

http pipeline是由一系列的middleware組成，每個元件皆有不同功用，並以非同步方法在HttpContext上作業，前一個元件會叫用下一個元件，或是終止請求的執行

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
|Authentication |提供驗證支援|需要在HttpContext.User之前。 Terminal for OAuth callbacks.|
|Authorization|provides authentication support|immediately after the authentication middleware|
|Cookie Policy | 追踨使用使對用於儲存個人資訊的同意，並強制執行Cookie欄位的最低標準，例如secure和SameSite| 在發出Cookie的middleware之前。如驗證、session、Mvc(TempData)|
|CORS|設定跨原始來源資源共享|在使用CORS元件之前|
|Diagnostics|提供開發人員例外狀況頁面、例外處理、狀態字碼頁。以及新應用程式的預設網頁的middleware|在產生錯誤的元件之前。terminal的例外狀況，或為新的應用程式提供預設的網頁|
|ForwardedHeaders|將設為Proxy的標頭轉送到目前請求|在使用更新欄位元件前。(example: schema,Host,Client IP,method)|
|Health Check|檢查ASP.NET Core應用程式及其相依性的健康狀態，例如檢查資料庫可用性|在某項要求與健康狀態檢查端點相符則中止|
|Header Propagation| Propagation HTTP headers from the incoming request to the outgoing HTTP client request|
|HTTP method override|允許傳入的post request override| Before component that consume the updated methods.|
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