# dotnet

- 版本
```dotnetcli
dotnet --version
```

- 完整資訊
```dotnetcli
dotnet --info
```

- 命令列說明
```dotnetcli
dotnet -h
```

- 列出已安裝的sdk版本
```dotnetcli
dotnet --list-sdks
```

- dotnet core runtime版本
```dotnetcli
dotnet --list-runtimes
```

- 列出有關asp.net template
```dotnetcli
dotnet new asp.net -l
```
## 基本命令

- dotnet new : 初始化，指定專案
- dotnet restore : 還用應用程式相依性
- dotnet build : 建置.net core應用程式
- dotnet publish : 發行應用程式及其相依性到資料夾，部署至hosting
- dotnet run : 執行應用程式
- dotnet test : 使用測試執行器執行測試
- dotnet vstest : 執行vstest.console 應用程式，執行自動化測試
- dotnet pack : 建立程式碼的nutget套件
- dotnet migrate : 專案移轉版本
- dotnet clean : 清除組件
- dotnet sln : 在方案檔中新增、移除及列出專案的選項
- dotnet help : 顯示命令詳細說明文件
- dotnet store : 在執行階段套件存放區中的儲存組件
- dotnet msbuild : 提供對msbuild命令的存取


## dotnet tool 常用安裝

- dotnet EntityFramework
```dotnetcli
dotnet tool install -g dotnet-ef
```

- dotnet-aspnet-codegenerator (樣板產生器)
```dotnetcli
dotnet tool install -g dotnet-aspnet-codegenerator
```

## package 常用安裝
- 樣板產生器
```dotnetcli
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

- ef core相關
```dotnetcli
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

```

## 其他命令

|command|說明|
|--|--|
|dev-certs| 建立及管理開發憑證|
|ef | Entity Framework Core命令工具|
|sql-cache|sql server快取命令工具|
|user-secrets|管理開發使用者秘密|
|watch|啟動會在檔案變更時執行命令的檔案監看員|
|dotnet-install scripts | 執行script進行非管理.NET Core SDK和Shared runtime安裝|

## 專案常用命令
[[dotnet-project]]

## nuget管理命令列

[[dotnet-nuget]]

## libman
[[libman]]

## 線上文件
[dotnet command line](https://docs.microsoft.com/zh-tw/dotnet/core/tools/dotnet-new)

## dev-certs

[ca-linux](https://bbs.archlinux.org/viewtopic.php?id=251330)

[self-signed-certificates](https://docs.microsoft.com/zh-tw/dotnet/core/additional-tools/self-signed-certificates-guide)

[//begin]: # "Autogenerated link references for markdown compatibility"
[dotnet-project]: dotnet-project.md "dotnet-project"
[dotnet-nuget]: dotnet-nuget.md "dotnet-nuget"
[libman]: libman.md "libman"
[//end]: # "Autogenerated link references"