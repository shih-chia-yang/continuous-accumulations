# logging

Logging provider會將log記錄輸出或寫入到不同目的端，例如Console provider輸出log記錄到Console中，window event log寫入至事件檢視器。 

- 加入其他logging provider,可在program的CreateHostBuilder()用ConfigureLogging方法加入。

```aspx-csharp
public static IHostBuilder CreateHostBuilder(string[] args)=>
    Host.CreateDefaultBuilder(args)
    .ConfigureLogging(loggingBuilder=>{
        loggingBuilder.ClearProvider();
        //系統預設提供者
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
        loggingBuilder.AddEventSourceLogger();
        //加入其他提供者
        loggingBuilder.AddTraceSource(new SourceSwitch("loggingSwitch","Verbose"),new TextWriterTraceListener("LoggingService.txt"));
        loggingBuilder.AddAzureWebAppDiagnostics();
        loggingBuilder.AddApplicationInsights();
});
```

加入多重logging provider，logs記錄能發送到多重目的寫入

## 在controller使用logging記錄資訊

於輸出來源「偵錯」可看到log記錄輸出

```aspx-csharp
public class HomeController:Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger-logger;
    }
}

public IActionResult Index()
{
    EventId eventId=new EventId(1234,"test log");
    _logger.LogWarning(eventId,"logging write by Home/Index")
}
```

## Log Level記錄層級

|等級|代碼|說明|
|--|--|--|
|None|6|不寫入log訊息。指定logging類別不應寫入任何訊息|
|Critical|5|記錄不可復原的應用程式，系統崩潰或災難性故障，需要立即關注|
|Error|4|記錄當前執行流程因失敗而停止，這個失敗是指當前執行洖動，但不是整個應用程式範圍的失敗|
|Warning| 3|記錄應用程式中的異常或非預期事件外，但這些異常不導致應用程式停止|
|Information| 2|用於記錄應用程式一般流程，log應該有一些長期價值|
|Debug| 1|在開發期間使用的互動式調查log記錄，log主要是包含debugging偵錯資訊，沒有長期保存的價值|
|Trace| 0 |此等級的log包含大多數詳細資訊，這些訊息可能包含敏感的應用程式資料，預設是disable，在production絕對不要開啟|

## appsettings.json 設定

預設值為debug情況下，必須使用至少跟debug同等級或以上的方法，log才會寫入至logging provider。ex: `LogDebug`、`LogInformation`、`LogWarning`、`LogError`、`LogCritical`
```json
{
    "Logging":{
        "LogLevel":{
            "Default":"Debug",
            "System":"Information",
            "Microsoft":"Information"
        }
    }

}
```