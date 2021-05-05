# options

直接存取組態值會造成相依問題為，為避免此問題，ASP.NET Core使用options pattern抽象化組態系統，將組態資料繫結至`class`，應用程式再存取該class，使用應用程式不直接相依組態系統。

options class有2個要求

1. 必須是非抽象類別
2. 建構函式必須為public且無參數

建立options pattern 步驟
1. 建立組態資料，並載入組態系統中
2. 建立Options類別
3. 在DI Container中以services.Configure<T>註冊Options class
4. 在controller /view /Services中存取Options類別

## example

1. 新增組態資料至appsettings.json
```json
"Developer":{
    "Name":"123",
    "Mail":"123@123",
    "Phone":"1231243425"
    
}
```

2. 建立Options class，屬性名稱須與組態key名稱對映
```aspx-csharp
public class DeveloperOptions
{
    public string Name {get;set;}
    public string Mail {get;set;}
    public string Phone {get;set;}

}
```

3. 在Startup.cs的`ConfigureServices`註冊DeveloperOptions，繫結對應的組態設定
```aspx-csharp
services.Configure<DeveloperOptions>(options=>Configuration.GetSection("Developer").Bind(options));
```

4. 在Controller中注入DeveloperOptions
```aspx-csharp
public Class UserController:Controller
{
    private readonly IOptionMonitor<DeveloperOptions> _options
    public UserController(IOptionMonitor<DeveloperOptions> options)
    {
        _options=options;
    }

    public IActionResult Index()
    {
        string name=_options.Name;
        return View(name);
    }
}
```