# swagger

1.使用ch3 的todoSolution
2.NutGet Package: install Swashbuckle.AspNetCore
3.使用postman 測試api

## install Swashbuckle

```dotnetcli

dotnet add todo.mvc package Swashbuckle.AspNetCore 
```

## setting swagger middleware

1. add AddSwaggerGen to IServiceCollection in Startup.ConfigureServices method 

```aspx-csharp

public void ConfigureServices(IServiceCollection services)
{

    services.AddSwaggerGen();
}

```

在`Startup.Configure`，加入swagger ui 與json文件
```aspx-csharp

public void Configure(IApplicationBuilder app)
{
    app.UseSwagger();

    app.UseSwaggerUI(c=>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json","My API v1);
    });
}

```

> [!TIP]
> 要在應用程式的root`(http://localhost:<port>/)`提供swagger ui，請將RoutePrefix設為空字串
```aspx-csharp
app.UseSwaggerUI(c=>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json","My API v1);
        c.Routeprefix=string.Empty;
    });
```
1. 若使用目錄搭配IIS或反向Proxy，請將Swagger端點設定為使用./前置詞的相對路徑。
ex:`./swagger/v1/swagger.json`。
2. 使用`/swagger/v1/swagger.json`指示應用程式在URL的真實根目錄(若已使用，請加上`route prefix`)。ex:`http://localhost:<port>/<route_prefix>/swagger/v1/swagger.json`

## API資訊與描述
傳遞至`AddSwaggerGen`方法的組態動作會新增作、授權和描述等資訊
在`startup`類別中，匯入下列namespace `OpenApiInfo`

```aspx-csharp
services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1",new OpenApiInfo{
        Version="v1",
        Title="Todo API",
        Description="a simple example ASP.NET Core Web API",
        TermsOfService=new Uri("https://example.com.terms"),
        Contact= new OpenApiContact{
            Name="test name",
            Email=string.Empty,
            Url= new Uri("https://twitter.com/spboyer"),
        },
        License= new OpenApiLicense
        {
            Name="Use under LICS",
            Url= new Uri("https://example.com/license"),
        }
    });   
});

```

## XML 註解

啟用xml註解
```aspx-csharp
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

```aspx-csharp

services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1",new OpenApiInfo{
        Version="v1",
        Title="Todo API",
        Description="a simple example ASP.NET Core Web API",
        TermsOfService=new Uri("https://example.com.terms"),
        Contact= new OpenApiContact{
            Name="test name",
            Email=string.Empty,
            Url= new Uri("https://twitter.com/spboyer"),
        },
        License= new OpenApiLicense
        {
            Name="Use under LICS",
            Url= new Uri("https://example.com/license"),
        }
    });

    var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath=Path.Combine(AppContext.BaseDirectory,xmlFile);
    c.IncludeXmlComments(xmlPath);   
});

```

將`<remark>`加入至Create動作，它會補充`<summary>`項目中指定的資訊，提供更友善的Swagger UI。`<remark>` 項目內容可以包含文字、json或xml

```xml
/// <remarks>
/// Sample request:
///
///     POST /Todo
///     {
///        "id": 1,
///        "name": "Item1",
///        "isComplete": true
///     }
///
/// </remarks>
```

## 資料註解

使用`ComponentModel.DataAnnotation`，協助Swagger UI呈現
```aspx-csharp

    public class TodoItem
    {
        [Required]
        public string Name {get;set;}
        
        [DefaultValue(false)]
        public bool IsComplete{get;set;}
    }

```


## 描述回應類型
使用Web Api 的開發人員最關心的是傳回的內容-特別是回應類型和錯誤碼。回應類型和錯誤碼會在xml註解及資料註解中表示。

```aspx-csharp

[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public IActionResult Create(TodoItem item)
{

}
```

使用`<response>`加入xml說明中，會補充`[ProducesResponseType]`詳細描述
```xml
<response code="201">item create successfully</response>
```


## 相關連結
[swashbuckle 與asp.net core使用者入門](https://docs.microsoft.com/zh-tw/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio)