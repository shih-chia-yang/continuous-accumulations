# aspnet_mvc_versioning

- Query String-based Versoning
- URL Path-based Versioning
- Http Header Based Versioning
- Convention Based Versioning
- Action Method Based Versioning
- Deprecating Versioning

## setup

建立webapi專案，安裝package `Microsoft.AspNetCore.Mvc.Versioning`,Configure the `services.AddApiVersioning` middleware

1. `dotnet new sln -o aspnet_mvc_version`
2. `dotnet new webapi -o api_version`
3. `dotnet sln add api_version/api_version.csproj`
4. `cd api_version`
5. `dotnet dev-certs https --trust`
6. `dotnet add package Microsoft.AspNetCore.Mvc.Versioning`
7. `dotnet add package Microsoft.Asp`
8. `dotnet aspnet-codegenerator controller -name Testv1 -async -api -outDir Controllers`
9. `dotnet aspnet-codegenerator controller -name Testv2 -async -api -outDir Controllers`
10. add `AddApiVersioning` in `ConfigureService`.

```aspx-csharp
services.AddApiVersioning(options=>{
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=true;
                options.DefaultApiVersion= new ApiVersion(1,0);
                options.Conventions.Controller<Testv1Controller>().HasApiVersion(new ApiVersion(1,0));
                options.Conventions.Controller<Testv2Controller>().HasApiVersion(new ApiVersion(2,0));
                });
```

- ReportApiVersions: Adds a response header api-supported-versions.
- AssumeDefaultVersionWhenUnspecified: Client that don't specify version, default will be used ,otherwise they will get an error
- DefaultApiVersion: specify default version number using `ApiVersion`
- ApiVersionReader: specify the location from where version v is read.the default is `query string` however, to use HTTP header,you need to specify this
- Convention: instead of using `[ApiVersion]` attribute,you could specify versions of various controllers here

```aspx-csharp
[ApiVersion("1.0")]
    [Route("api/Test")]
    [ApiController]
    public class TestControllerv1 : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()=>Content("Version 1");
    }
```

> [!TIP]
> read the current version of request by using `GetRequestApiVersion` method on HttpContext,which returns `ApiVersion` type

## Query String-based Versioning

- using `https://localhost:5001/api/Test?api-version`, return default api version `Version 1`
- `https://localhost:5001/api/Test?api-version=2` , return `Version 2`

|url|return|
|http://localhost:5001/api/Test|Version 1|
|http://localhost:5001/api/Test?api-version=1|Version 1|
|http://localhost:5001/api/Test?api-version=2|Version 2|

## URL-Path API Versioning

use a route and `api-version` constraint to specify version number in the URL

1. add `[Route]` and add url parameter`{ver:apiVersion}`
2. test `https://localhost:5001/api/v1/Test`, return `Version 1`
3. test `https://localhost:5001/api/v2/Test`, return `Version 2`

|url|return|
|--|--|
|http://localhost:5001/api/v1/Test|Version 1|
|http://localhost:5001/api/v2/Test|Version 2|

## Http Header Based Versioning

To use HTTP Header to pass version number , we need to first configure the version reader, tells the middleware how to read the version

> [!TIP]
> 當`ApiversoinReader` 設定為HeaderApiVersionReader ,`query string-base`則不會生效

1. create controller and name as HeaderController

2. enable header API versioning in startup.cs

    ```aspx-csharp
    options.ApiVersionReader=new HeaderApiVersionReader("api-version")
    ```

3. add default action `Get()`

4. `Getv2()` action add `[MapToApiVersion("2.0")]`

5. use postman test api , then header add `api-version` with value send request check the return value

|url|heaers|return|
|--|--|--|
|`https://localhost:5001/api/Header`||Version 1|
|`https://localhost:5001/api/Header`|api-version:1.0|Version 1|
|`http://localhost:5001/api/Header`|api-version:2.0|Version 2|

## Convention based Versioning

to use convention, we need to first configure the version number on the controller by using Convention property

1. create `SaleController`
2. enable header API versioning
    - `options.ApiVersionReader=new HeaderApiVersionReader("api-version");`
3. enable convention on the Api versioning options
    - `options.Convention.Controller<SaleControllerV1>().HasApiVersion(new ApiVersion(1,0));`
    - `options.Convention.Controller<SaleControllerV2>().HasApiVersion(new ApiVersion(2,0));`

|url|headers|return|
|--|--|--|
|http://localhost:5001/api/sale||Version 1|
|http://localhost:5001/api/sale|api-version:1.0|Version 1|
|http://localhost:5001/api/sale|api-version:2.0|Version 2|

## Action methods based Versioning

version individual action methods within a controller using `[MapToApiVersion]`

1. create controller `paymentController`
2. enable Api version `options.ApiVersionReder=new HeaderApiVersionReader("api-version")`
3. add `[MapToApiVersion]` on actions
4. use postman test api

## Deprecating a Version
version of API is deprecated by setting the `Deprecated` property on the `[ApiVersion]` attribute

1. add `[ApiVersion("1.0",Deprecated=true)]`
2. header will show `api-deprecated-version:1.0` & `api-supported-version:2.0`

## Troubleshooting and Solution

當建立不同版本Api時，request可正常執行，但swagger api會出現`Conflicting method/path combination "Get api/xxx/xxxx" for actions .Actions require a unique method/path combination for Swagger/OpenAPI 3.0`

1. making the endpoints with the information on which version to map to. decorate every endpoing with the `[MapToApiVersion]`

    ```aspx-csharp
    
    [MapToApiVersion("2.0")]
    public string Alive()
    {
        return "Alive Version 2";
    }
    ```

2. add package `Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer`

    ```dotnetcli
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
    ```

3. add the `ApiExplorer` service to the collection

    ```aspx-csharp
    services.AddVersionApiExplorer(options=>{
        options.GroupNameFormat="'v'VVV";
        options.SubstituteApiVersionInUrl=true;
    });
    ```

4. inject the `ApiExplorer services` into the Configure method and with the metadata it has collected into the SwaggerUI middleware

    ```aspx-csharp
    public void Configure(IApplicationBuilder app,IWebHostEnvironment env,IApiVersionDescriptionProvide provider)
    {
        app.UseSwaggerUI(options=>{    
            foreach(var description in provider.ApiVersionDescription)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",description.GroupName.ToUpperInvariant()
                );
            }
        });
    }
    ```

- `https://localhost:50001/swagger/v1/swagger.json` 可正常執行並呈現，但Swagger V2 verion會出現 not found error。

need to ensure that Swagger creates documentation for all the available version provided by the ApiExplorer

1. add swagger document for every API version discovered

    ```aspx-csharp
    foreach ( var description in provider.ApiVersionDescriptions )
            {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo()
                {
                    Title = $"Sample API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description=description.IsDeprecated?"This Api version has been deprecated":string.Empty
                }
                );
            }
    ```

## 相關連結

[SwaggerDoc](https://github.com/microsoft/aspnet-api-versioning/wiki/API-Documentation#aspnet-core)
[Integrating ASP.NET Core Api Versions with Swagger UI](https://referbruv.com/blog/posts/integrating-aspnet-core-api-versions-with-swagger-ui)