# launchsettings

> [!TIP]
> 僅供本機電腦使用，不用於部署

此檔案決定專案執行與行為，使用iis express或kestrel web server，是否啟動瀏覽器、環境變數或應用程式監聽的url網址

分為2大類、3個區塊

1. IIS設定
```json
///iis設定
"iisSettings":{

    "iisExpress":{
        ///HTTP port
        "applicationUrl":"http://localhost:5000",
        /// https port
        "sslPort":"5001"
    }
}
```
2. Profiles設定

```json
///profiles 設定
"profiles":{
    ///以IIS執行
    "IIS Express":{
    ///啟動瀏覽器
    "launchBrowser":true,
    "environmentVariables":{
        ///環境變數=>以development環境
        "ASPNETCORE_ENVIRONMENT":"Development"
        }
    },
    ///以dotnet run 執行
    "<projectname>":{
        "environmentVariables":{
            ///環境變數=>以development環境
            "ASPNETCORE_ENVIRONMENT":"Development"
            }
        },
        ///應用程式的https與http port
        "applicationUrl":"https://localhost:5001;http://localhost:5000"
    }
}
```