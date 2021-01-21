# Coverlet

[github](https://github.com/coverlet-coverage/coverlet)

## VSTest integration
- **安裝**
````cli
dotnet add package coverlet.collector
````
> [!WARNING]
> 必須在測試專案安裝此package，如果使用的是xunit測試專案(`dotnet new xunit`)則會預設安裝，因為`Coverlet`已成為`.net core`和`.net5`專案的預設`coverage tool`，如果版本是`<.net5`，如果需要使用則必須升級

- **使用說明**

coverlet已整合至visual test platform ，取得測試覆蓋率可執行以下命令
```dotnetcli
dotnet test --collect:"XPlat Code Coverage"
```
執行完後，將會在`TestResults`資料夾中產生`coverage.cobertura.xml檔案

[詳細說明](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md)

## MSbuild integration

- **使用說明**
```dotnetcli
dotnet add package coverlet.msbuild
```
> [!WARNING]
> 必須在測試專案安裝package


- **用法**

Coverlet 已與建置系統整合，會在執行測試之後進行測試覆蓋率，計算測試覆蓋率只需設定`CollectCoverage` 為`true`

```dotnetcli
dotnet test /p:CollectCoverage=true
```
以上命令執行完以後將會在測試專案根目錄產生`coverage.json`檔案，同時也會在terminal呈現計算結果。

[詳細說明](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/MSBuildIntegration.md)

> [!NOTE]
> 個人初次實驗使用此方式，以下為命令範例
- CoverageOutputFormat：輸出格式
    - json(default)
    - lcov
    - opencover
    - covertura
    - teamcity
- CoverletOutput：輸出位置
```dotnetcli
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./TestResults/coverages/ universities.sln
```

## .NET Global tool

**安裝**
```dotnetcli
dotnet tool install --global coverlet.console
```

**使用說明**
coverlet tool透過指定含有單元測試的dll路徑執行計算，你還需要分別使用--target和--targetargs指定test runner與test runner所需的參數，使用提供參數的test runner不可以重新編譯專案，否則將無法計算覆蓋率。
```dotnetcli
coverlet /path/to/test-assembly.add --target "dotnet" --targetargs "test /path/to/test-project --no-build"
```

[詳細說明](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/GlobalTool.md)

## how it work

### Before Tests Run
- Locates the unit test assembly and selects all the referenced assemblies that have PDBs.
- Instruments the selected assemblies by inserting code to record sequence point hits to a temporary file.
### After Tests Run
- Restore the original non-instrumented assembly files.
- Read the recorded hits information from the temporary file.
- Generate the coverage result from the hits information and write it to a file.
