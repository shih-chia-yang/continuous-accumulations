# libman

- 安裝libman

```dotnetcli
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```
## 使用方式

```dotnetcli
libman --version
```

```dotnetcli
libman --help
```
- 初始化專案中的libman

```dotnetcli
dotnet init -d -p  --verbosity
```

- `-d | --default-destination <PATH>` : 相對於目前資料夾的路徑
- `-p | --default-provider <PROVIDER>` : 如果未針對指定的程式庫定義提供者，則為要使用的提供者。
    - cdnjs
    - filesystem
    - jsdelivr
    - unpkg

- `-h | --help` :說明資訊

- `--verbosity <LEVEL>` : 設定輸出的詳細資訊
    - quiet
    - normal
    - detailed

## example

- 至專案根目錄
- 執行`libman init`
- 輸入預設提供者
    - cdnjs
    - filesystem
    - jsdelivr
    - unpkg


## 相關連結
[搭配ASP.NET Core使用Libman CLI](https://docs.microsoft.com/zh-tw/aspnet/core/client-side/libman/libman-cli?view=aspnetcore-5.0)