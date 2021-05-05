# modify_contentroot

一般情況使用預設值即可，若要對ContentRoot和WebRoot路徑調整，可在`CreateDefaultBuilder`
方法中，使用`UseContentRoot`和`UseWebRoot`指定參數

- Program.cs
```aspx-csharp
public static IHostBuilder CreateHostBuilder(string[] args)=>
    Host.CreateDefaultBuilder(args)
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureWebHostDefaults(webBuilder=>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseWebRoot(Directory.GetCurrentDirectory()+"/<custom path>");
    });

```

- 用middleware設定靜態檔目錄

在WebRoot之外有其他目錄存放靜態資源，希望和wwwroot共同服務，那麼可用middleware設定靜態檔目錄。

- Startup.cs

```aspx-csharp
app.UseStaticFiles(new StaticFileOptions{
    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"StaticFilesLibrary"
    ))
});
```

```aspx-csharp
app.UseStaticFiles(new StaticFileOptions{
    FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"StaticFilesLibrary")),
    RequestPath="/StaticFiles"
});
```

```html
//第一種沒有指定RequestPath，檔案 request path維持`~/...`
<img src="~/images/small.jpg" alt="jpg">
//第二種指定RequestPath，路徑須改成`~/StaticFiles/...`
<img src="~/StaticFiles/images/small.jpg" alg="jpg">
```