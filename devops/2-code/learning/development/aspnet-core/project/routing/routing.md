# routing

- Routing Middlware
- Basic Routing Example
- MapControllerRoute
- Convention-based and template-based Routing
- attribute-based Routing
- multiple routes
- routing constrains
- regex constrains
- types of route constrains


## routing mechanism

the main functionality of routing operation is to map any incoming request to a routing handler.

incoming request are mapped to the controllers and their actions.

configure in application startup
1. app.UseRouting() 
2. app.UseEndPoint()

- add a web project
1. open startup.cs
 - app.UseRouting() adds the routing middleware to be used. 
 - app.UseEndPoint() adds a endpoint middleware which handles routing HTTP requests. It takes in RouteHandler to process the request
 - MapControllerRoute is an extension method to add a route to IEndpointRouteBuilder which has a specific name and pattern

```aspx-csharp
pattern:"{controller=Hi}/{action=Index}/{id?}
```

使用{}做區隔，在3個{}區塊中分別代表 controller,action options id。在以HiController的Index action做為預設執行
，options 參數會加上?表示

*URL* 會以下幾種方式觸發
1. /Hi/Index/1 =>所有值都提供
2. /Hi/Index =>選擇性參數省略
3. /Hi =>預設使用Index Action
4. / => 預設執行HiController的Index action

## Routing types

1. Convention-based
2. Template-based
3. Attribute-based

## Convention route 會搭配controller與view使用

```aspx-csharp
endpoint.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}");
)
```
1. 第一個區段，`{controller=Home}`對應到控制器名稱
2. 第二個區段，`{action=Index}`對應到動作名稱
3. 第三個區段，`{id?}`對應到option argument。`?`讓`id`成為選擇性的。

- 只以controller和action 名稱為基礎
- 不是以命名空間、來源檔案位置或方法參數為基礎

## REST api Attribute-based
REST Api應該使用`Attribute route`，將應用程式的功能模型為一組資源，其中的作業會以HTTP動詞表示。

Attribute route是使用 `[Route]`，將動作直接對應至`template-based route`。
下列範例一般適用於REST API

```aspx-csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
}

public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
{
    if(env.IsDevelopment()){
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    
    app.UseRouting();
    
    app.UseAuthorization();

    app.UseEndPoints(endpoints=>
        {
            endpoints.MapControllers();
        }
    )
}
```

```aspx-csharp
public class HomeController : Controller
{
    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    [Route("Home/Index/{id?}")]
    public IActionResult Index(int? id)
    {    
        return ControllerContext.MyDisplayRouteInfo(id);
    }
}
```

此範例強調`Attribute-based`與`convention-based`的設計差異。屬性路由需要更多輸入才能指定route。
`convention-based`更簡潔的處理route。`Attribute-based`允許精確地控制每個動作的template route

## 保留的route name

使用controller或 view時，下列關鍵字是Razor保留關鍵字

- action
- area
- controller
- handler
- page

## HTTP Verb Template

- [HttpGet]
- [HttpPost]
- [HttpPut]
- [HttpDelete]
- [HttpHead]
- [HttpPatch]


## Route Template
- 所有的HTTP Verb Template都是 route telplates
- route

## routing with HTTP Verb Attribute

```aspx-csharp

[Route("api/[controller]")]
[ApiController]
public class Test2Controller:ControllerBase
{
    ///URL = Get api/Test2    
    [HttpGet]
    public IActionResult ListProducts()
    {
        return Content("ProductList");
    }
    
    ///URL Get api/Test2/123
    [HttpGet("{id}")]
    public IActionResult GetProduct(string id)
    {
        return Content($"product id:{id}");
    }

    [HttpGet("{int/{id:int}}")]
    public IActionResult GetIntProduct(int id)
    {
        return COntent($"product int id:{id}");
    }

}

``` 

## route name

可以使用route name根據特定route 來產生URL，route name：
1. 不會影響route的URL比對行為
2. 僅用於產生URL

在整個應用程式中route name必須是唯一的

```aspx-csharp

[HttpGet("/product/{id}",name="ProductList")]
public IActionResult GetProduct(int id)
{
    return Content("{id}");
}
```

## multi route
`Attribute-based`支援定義多個route來達到相同的動作，最常見的用法是模擬`Convention-based`的方法
```aspx-csharp

[Route("example")]
[Route("[controller]")]
public class Test1Controller:ControllerBase
{

    [Route("")]  ///Test1/ or example/
    [Route("Index")] ///Test1/Index or example/Index
    public IActionResult Index()
    {
        return Content("Index");
    }
}

```

## 相關連結
[ASP.NET Core 中的路由至控制器動作](https://docs.microsoft.com/zh-tw/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#routing-token-replacement-templates-ref-label)

[ASP.NET Core 中的路由](https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/routing?view=aspnetcore-5.0)