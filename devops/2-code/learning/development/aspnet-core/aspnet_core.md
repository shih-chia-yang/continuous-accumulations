# aspnet_core

[intro](#intro)

[ShareFramework](#shareframework)

[Web Root](#web-root)

[Content Root](#content-root)

[Make HTTP Requests](#make-http-requests)

[Error Handling](#error-handling)

[EndPoint Routing](#endpoint-routing)

[Logging](#logging)

[Host](#host)

[Startup Class](#startup-class)

[Configuration](#configuration)

[Options](#options)

[Dependency Injection](#dependency-injection)

[Middleware](#middleware)

[Server](#server)

[Environment](#environment)

[web api](#web-api)

[trouble shooting](#trouble-shooting)

## intro

[[system]]

[[mvc-pattern]]

[[project-type]]

[[dotnet]]

the asp.net core mvc framework is a lightweight , open source, highly testable presentation framework optimized for use with ASP.NET Core.

- Routing
- Web APIs
- Model binding
- Model validation
- Dependency injection
- Filters
- Areas
- Testability
- Razor view engine
- Strongly typed views
- View Components
- Tag Helpers

## 選擇.net core 與 .net framework 的開發需求

1. **使用.net core**

   - 有跨平台需求
   - 微服務架構
   - 使用 container 技術
   - 需要高效能與易於擴展的服務
   - 需要不同.net 版本並行

2. **使用.net framework**
   - 目前系統是.net framework
   - 第三方組件、nuget package 無法相容.net core 版本
   - 系統使用了.net core 無法支援的功能
   - 目前使用的平台無法使用.net core

- 掌控 ASP.NET Core App 系統運作
- 提供 Hosting 和 Web Server 組態設定
- 提供各種環境變數與組態設定
- 提供多重環境組態設定：Development、Staging、Production
- 提供 DI 及 Middle 設定
- 提供效能調校、logging

3. **ASP.NET Core 載入過程**

   1. dotnet run : dotnet command cli tool
   2. launchSetting.json : 本機環境組態設定
   3. Programs() : 建立 Generic Host
   4. appsettings.json : 載入 logging 設定、資料庫設定等其他
   5. Startup.cs
      1. ConfigrureServices() : DI Container/Option Pattern 註冊的地方
      2. Configure() : 載入設定的 middleware
   6. Host 建立/執行 :建立 host 主機，kestrel 開始監聽 http request and send response

> launchSettings 說明 : [[launchsettings]]

> configure : [[appsettings]]

> Program.cs [[Program]]

> Startup.cs [[startup]]

## ShareFramework

共享的框架組件

- Microsoft.AspNetCore.App
- Microsoft.NETCore.App

## Web Root

web 根目錄，專案對外公開靜態資產的目錄，路徑為`{ContentRoot}/wwwroot`

包含 images、css、js、json 和 xml 等靜態檔

`GetCurrentDirectory`加上/wwwroot 就是 web root 根目錄路徑

## Content Root

內容根目錄，代表專案目前所在的基底路徑

調用`IWebHostEnvironment`取得 content root

```aspx-csharp
private readonly IWebHostEnvironment _env;
public TestController(IWebHostEnvironment env)
{
    _env=env;
    string contentRoot=env.ContentRootPath;
}
```

- 調整路徑:[[modify_contentroot]]

## Make HTTP Requests

是 IHttpClientFactory 實作，用於建立 HttpClient 實例

## Error Handling

負責錯誤處理的機制

## EndPoint Routing

自 ASP.NET 3.0 開始採用 Endpoint route，負責匹配與派送 HTTP request 到應用程式執行端點

- Convention Routing
- Attribute Routing

[[routing]]

[[controller]]

[[view]]

## Logging

資訊或事件的記錄機制

- Console
- Debug
- Event Tracing Windows
- Windows Event Log
- TraceSource
- Azure App Service (需參考 Microsoft.Extensions.Logging.AzureAppServices 的 nuget 套件)
- Azure Application Insights (需參考 Microsoft.Extensions.Logging.ApplicationInsights 的 nuget 套件)

[[logging]]

## Host

裝載與執行.NET Core 應用程式的主機環境，它封裝了所有 App 資源，如 Server、Middleware、DI 和 Configuration，並實作 IHostedService

- Generic Host
- Web Host

## Startup Class

- ConfigureServices()
- Configure()

## Configuration

ASP.NET Core 的組態框架，提供 Host 和 App 所需的組態存取系統

- Host Configuration
- App Configuration

[[appsettings]]

[[Program]]

## Options

指 Option pattern，用類別來表示一組設定，.NET Core 中大量使用 Option pattern 設定 config

[[options]]

## Dependency Injection

相依性注入，亦稱 DI Container

[read more](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/denpendency-injection)

## Middleware

在處理 HTTP Request 的 pipeline, 包含一系 Middleware 中介軟體元件

[[middleware]]

## Server

指 HTTP Server 或 Web Server 伺服器、用於監聽 HTTP Request 與 Response 的網頁伺服器

- Kestrel
- IIS Http Server
  - In Process
  - Out Process
- HTTP.sys

## Environment

[[launchsettings]]

環境變數與機制，內建 Development、Staging、Production3 種環境
執行時會讀取 ASPNETCORE_ENVIRONMENT 環境變數

- Development
- Staging
- Production

> [!TIP] > `IsDevelopment()`、`IsStaging()`或`IsProduction()`提供判斷目前環境，若成立則回傳`true`

可調用`IWebHostEnvironment`取得環境變數，可在`Configure`判斷環境

1. 在 Controller 建構子宣告
2. 在 view 使用`@inject` 與 Environment TagHelper <environment include="Development"></environment>

## selecting authentication type

- no authentication:this option will not add any authentication packages to the application

- individual user accounts: individual user account template design for connecting to an azure active directory b2c application will provide us with all the authentication and authorization data.

- work or school accounts: this is for the enterprises, organizations and schools that authenticate users with office 365, active directory,or azure directory services can also use this option.

- windows authentication: windows authentication mostly be used applications within the internet environment

## crud todo project

[[crud_todo]]

## web api

[[web_api]]

[[web_api_version]]

[[aspnet_mvc_versioning]]

## swagger

[[swagger]]

## security

[[security]]

[[Authorization]]

[[jwt]]

[[require_https]]

## cors

[[cors]]

## 綜合

[[three_tier_project]]

[[webApplication]]

## trouble shooting

**unable to start kesterl Interop+Crypto+OpenSslCryptographicException: error:2006D080:BIO routines:BIO_new_file:no such file**

原因為憑證問題，需設置環境變數
ASPNETCORE_Kestrel__Certificates__Default__Password
ASPNETCORE_Kestrel__Certificates__Default__Path

[unable to start kesterl](https://stackoverflow.com/questions/56824859/how-to-run-dockered-asp-net-core-app-generated-by-visual-studio-2019-on-linux-p)

[github-issue-25760](https://github.com/dotnet/aspnetcore/issues/25760)

[github-issue-36333](https://github.com/dotnet/runtime/issues/36333)

---
**dev-certs**

```bash
dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --trust --format Pem
```

```bash
sudo vim /usr/lib/firefox/distribution/policies.json
```

```json
{
    "policies": {
        "Certificates": {
            "ImportEnterpriseRoots":true,
            "Install": [
                "/usr/local/share/ca-certificates/aspnet/https.crt"
            ]
        }
    }
}
```

[SEC_ERROR_INADEQUATE_KEY_USAGE](https://docs.microsoft.com/zh-tw/aspnet/core/security/enforcing-ssl?view=aspnetcore-5.0&tabs=visual-studio#trust-ff)


[//begin]: # "Autogenerated link references for markdown compatibility"
[system]: system.md "system"
[mvc-pattern]: mvc-pattern.md "mvc-pattern"
[project-type]: project-type.md "project-type"
[dotnet]: ../../tool/dotnet/dotnet.md "dotnet"
[launchsettings]: launchsettings.md "launchsettings"
[appsettings]: appsettings.md "appsettings"
[Program]: Program.md "program"
[startup]: startup.md "startup"
[modify_contentroot]: modify_contentroot.md "modify_contentroot"
[routing]: project/routing/routing.md "routing"
[controller]: controller.md "controller"
[view]: view.md "view"
[logging]: logging.md "logging"
[options]: options.md "options"
[middleware]: project/middleware/middleware.md "middleware"
[crud_todo]: project/todo/crud_todo.md "todo"
[web_api]: project/webapi/web_api.md "web_api"
[web_api_version]: project/webapi/web_api_version.md "web_api_version"
[aspnet_mvc_versioning]: project/webapi/aspnet_mvc_versioning.md "aspnet_mvc_versioning"
[swagger]: project/swagger/swagger.md "swagger"
[security]: project/seciurty/security.md "security"
[Authorization]: project/seciurty/Authorization.md "Authentication"
[jwt]: project/seciurty/jwt.md "jwt"
[require_https]: project/seciurty/require_https.md "require_https"
[cors]: project/cors/cors.md "cors"
[three_tier_project]: project/three_tier_project/three_tier_project.md "three_tier_project"
[webApplication]: project/webapplication/webApplication.md "webApplication"
[//end]: # "Autogenerated link references"
