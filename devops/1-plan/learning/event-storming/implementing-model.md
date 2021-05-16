# implementing-model

## technical requirements

- linux(archlinux) [[linux]]
- c# 8.0以上 
- .net 5.0以上 [[netcore]] [[aspnet_core]]
- visual studio code [[vscode]]
- xunit [[xunit]] [[unittests]] [[tdd]]
- sql server 2019 [[sql-server-2019]] [[efcore]]

作業環境為linux，相關command line請自行轉換

## starting up the implementation

1. web application
2. api application
3. unittest 

project structure

    marketplace
    |
    |--src
    |    |--services
    |    |    |--marketplace.infrastructure
    |    |    |--marketplace.domain
    |    |    |--marketplace.api
    |    |--webs
    |         |-university.web
    |--tests
    |    |--marketplace.unittests
    |    |--marketplace.functionaltests
    |--README.md

[[dotnet-sln]]

- 依照結構建立專案
```bash
dotnet new sln -o maketplace ; cd $_ ;mkdir -p src/services src/web tests;touch README.md;cd src/services;dotnet new classlib -o marketplace.infrastructure;dotnet new classlib -o marketplace.domain ; dotnet new webapi -o marketplace.api ; cd ../web ;dotnet new mvc -o marketplace.web ; cd ../../tests ;dotnet new xunit -o marketplace.unittests;dotnet new xunit -o marketplace.functionaltests

```

- 加入方案與專案參考
```bash
dotnet sln add src/services/marketplace.api/marketplace.api.csproj src/services/marketplace.domain/marketplace.domain.csproj src/services/marketplace.infrastructure/marketplace.infrastructure.csproj  src/web/marketplace.web/marketplace.web.csproj tests/marketplace.unittests/marketplace.unittests.csproj tests/marketplace.functionaltests/marketplace.functionaltests.csproj 
```

- 專案由外向內參考，以marketplace.domain為中心
```bash

dotnet add src/services/marketplace.api/marketplace.api.csproj reference src/services/marketplace.domain/marketplace.domain.csproj src/services/marketplace.infrastructure/marketplace.infrastructure.csproj;dotnet add src/services/marketplace.infrastructure/marketplace.infrastructure.csproj reference src/services/marketplace.domain/marketplace.domain.csproj ;dotnet add tests/marketplace.unittests/marketplace.unittests.csproj reference src/services/marketplace.api/marketplace.api.csproj src/services/marketplace.domain/marketplace.domain.csproj src/services/marketplace.infrastructure/marketplace.infrastructure.csproj 
```

## marketplace.domain

[[eventstorming]]

將design-level eventstorming所得到的model轉換為程式碼。
可以從以下的表格發現所有command都是執行關於`Classified Ad`的作業，且有create,remove,publish行為改變classified ad的狀態，即便內容物相同也應視為不同廣告。

entity:一個物件即使同樣的值也視為不同實體，且擁有一系列狀態變化呈現其生命週期。

|actor|command|domain event|
|--|--|--|
|user|Create an ad|Ad Created|
|seller|Change the ad title|Ad title changed|
|seller|Update the ad text|Ad text updated|
|seller|Update the sell price|Ad sell price updated|
|seller|Add picture to ad |Picture added to ad|
|seller,admin|Remove picture from ad|Picture removed from ad|
|seller|Request to publish the ad|

1. 新增ClassifiedAd
    1. all entities need to have an ID
    2. using an object-oriented language, we shell try to encapsulate as much as we can, and keep our internal safe,and preferably invisible to the outside world.
```csharp
    public class ClassifiedAd
    {
        public Guid Id { get; private set; }
        public Guid _ownerId;
        public string _title;
        public string _text;
        public string _price;

    }
```

2. 新增建構子允許從外部傳入參數設定Id
    1. 只能從建構子設定id
    2. 當實例化ClassifiedAd時，一定要設定id，因為並沒有提供無參數建構子
    3. id需要驗證，當驗證失敗時會throw exception
```csharp
public ClassifiedAd(Guid id)
{
    if(id==default)
    {
        throw new ArgumentException("Identity must be specified", nameof(Id));
    }
    Id = id;
}
```

3. 新增行為

建立上方表格中所示的Change the ad title, Update the ad text,Update the sell price

需藉由行為改變被封裝的內部欄位，保證外部無法隨意變更類別內部重要的訊息。

```csharp

public void SetTitle(string title) => _title = title;

public void UpdateText(string text) => _text = text;

public void UpdatePrice(decimal price) => _price = price;
```

4. 對輸入值進行驗證

_ownerId允許空值，廣告不可能沒有廣告主，所以需要為該值進行限制，驗證輸入值，
以保證類別初始化時是正確的

```csharp
public ClassifiedAd(Guid id,Guid ownerId)
{
    if(id==default)
    {
        throw new ArgumentException("Identity must be specified", nameof(Id));
    }
    if(ownerId==default)
    {
        throw new ArgumentException("Owner id must be specified", nameof(ownerId));
    }
    Id = id;
    _ownerId = ownerId;
}
```

隨著建構子增加更多的參數，建構子函式的檢查邏輯程式碼也隨之增加，隨著時間增長建構子的認知複雜度逐漸提高，因為檢查邏輯與賦值的程式碼混雜在一起。
目前程式碼也尚未檢查涉及多個屬性的核心複雜規則

we can check the validity of such values,even before reaching the entity constructor,,using `value object`

>個人覺得此概念接近`Domain Primitive`，與之前了解的value object不同--[[domain-primitive]]

範例中將_ownerId提取出來建立UserId的dp class，並將檢查邏輯封裝於UserId類別中，但個人覺得除非OwnerId有其特殊格式且包含業務意義，若只是一般檢查值的邏輯統一使用`ValidationResult`之類的類別即可。

以ClassifiedAd的context來判斷，此處應是將UserId視為ClassifiedAd的Value object。

## Value object

- 將_ownerId提取出來建立ValueObject

```csharp
public class UserId
{
    private readonly Guid _value;

    public UserId(Guid value)
    {
        if(value==default)
        {
            throw new ArgumentException("Owner id must be specified", nameof(value));
        }
        _value = value;
    }
}
```

- 將ClassifiedAd中的_ownerId型別修改為UserId
1. 此處代表識別UserId相同者，即為同一個廣告主。
2. 書本將Id提取出來建立ClassifiedAdId，個人認為id為該Entity的識別碼不是value object，直接使用abstract entity class即可。本例不實作
```csharp
public class ClassifiedAd
{
    public Guid Id { get; private set; }
    private UserId _ownerId;
    private string _title;
    private string _text;
    private decimal _price;
    public ClassifiedAd(Guid id,UserId ownerId)
    {
        if(id==default)
        {
            throw new ArgumentException("Identity must be specified", nameof(Id));
        }
        Id = id;
        _ownerId=ownerId;
    }

    public void SetTitle(string title) => _title = title;

    public void UpdateText(string text) => _text = text;

    public void UpdatePrice(decimal price) => _price = price;
}
```

[//begin]: # "Autogenerated link references for markdown compatibility"
[linux]: ../../../7-operate/learning/env/linux/linux.md "Linux"
[netcore]: ../../../2-code/learning/tool/dotnet/netcore.md "netcore"
[aspnet_core]: ../../../2-code/learning/development/aspnet-core/aspnet_core.md "aspnet_core"
[vscode]: ../../../2-code/learning/tool/vscode/vscode.md "Vscode"
[xunit]: ../../../4-test/learning/tools/xunit/xunit.md "xunit"
[unittests]: ../../../2-code/learning/development/aspnet-core/project/unittests/unittests.md "unittests"
[tdd]: ../../../2-code/learning/development/tdd/tdd.md "Tdd"
[sql-server-2019]: ../../../2-code/learning/tool/SqlServer/sql-server-2019.md "Sql Server 2019"
[efcore]: ../../../2-code/learning/tool/Efcore/efcore.md "EfCore"
[dotnet-sln]: ../../../2-code/learning/tool/dotnet/dotnet-sln.md "Dotnet Sln"
[eventstorming]: eventstorming.md "eventstorming"
[domain-primitive]: domain-primitive.md "data-primitive"
[//end]: # "Autogenerated link references"