# format_response

可以使用特定格式或回應用戶端要求的格式來格式化回應資料
ASP.NET Core支援application/json、t

## content negotiation
指定accept標頭時，會發生content negotiation。ASP.NET Core預格格式為JSON。
1. 由ObjectResult實作
2. 從helper方法傳回的狀態碼特定動作結果。
```aspx-csharp

return NotFound();

or

return Ok(result);
```

- browser 與 content negotiation
網頁瀏覽器提供Accept header。Web瀏覽器指定許多格式，包括萬用字元。預設架構偵測要求來自瀏覽器時：
1. accept heder 會被忽略
2. 除非另有設定，否則內容會以json傳回

可在使用api時，跨瀏覽器提供更一致的體驗

>> [!TIP]
> 若要將應用程式設定為接受瀏覽器接受標頭，請設定`RespectBrowserAcceptHeader=true`
```aspx-csharp
services.AddControllers(options=>
{
    options.RespectBrowserAcceptHeader=true;
});
```

## configure formatter

- 新增支援XML格式
```aspx-csharp
services.AddControllers()
.AddXMLSerializerFormatters();
```

- 設定支援system.Text.Json formatter
```aspx-csharp
services.AddControllers()
    .AddJsonOptions(options=>
        options.JsonSerializerOptions.PropertyNamingPolicy=null    
);
```

- example ASP.NET Core，建立ProblemDetail回應
>ProblemDetail：一種機器可讀取的格式，根據`https://tools.ietf.org/html/rfc/rfc7807`來指定http api回應中的錯誤
```aspx-csharp
[HttpGet("error")]
public IActionResult GetError()
{
    return Problem("something went wrong")
}
```

- 設定支援Newtonsoft.Json格式
在ASP.NET Core 3.0前，預設使用json格式為Newtonsoft.json。在ASP.NET Core 3.0之後的版本，預設json格式是以System.Text.Json為基礎

```aspx-csharp
services.AddControllers()
.AddNewtonsoftJson();
```

## 指定格式
限制回應格式、請套用[Produces] FilterAttribute。可以在動作、控制器或全域範圍使用：

```aspx-csharp
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class WeatherForecastController:ControllerBase
{
}

```