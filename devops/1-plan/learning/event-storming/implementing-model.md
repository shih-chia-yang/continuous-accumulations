# implementing-model

## technical requirements

- linux(archlinux) [[linux]]
- c# 8.0以上 
- .net 5.0以上 [[netcore]] [[aspnet_core]]
- visual studio code [[vscode]]
- xunit [[xunit]] [[unittests]] [[tdd]]
- sql server 2019 [[sql-server-2019]] [[efcore]]
- git [[git]]

作業環境為linux，相關command line請自行轉換
修改紀錄以git log為主，md文件輔助，僅說明概念與架構
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

- 由money(valueobject)負責相等性與計算，price(domain primitive)物件處理格式邏輯

1. Money類別使用Value object，並實作2個money物件相加相減
2. 新增Price類別繼承Money物件，並檢查不可小於0 

```csharp
public class Money:ValueObject
{
    public decimal Amount { get;}

    public Money(decimal amount) => Amount = amount;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }

    public Money Add(Money sumand) => new Money(this.Amount + sumand.Amount);

    public Money Subtraction(Money subtrahend) => new Money(this.Amount - subtrahend.Amount);
    public static Money operator + (Money sumand1, Money sumand2) => sumand1.Add(sumand2);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtraction(subtrahend);
}
```

```csharp
public class Price : Money
{
    public Price(decimal amount):base(amount)
    {
        if(amount<0)
        {
            throw new ArgumentException("Price cannot be negative", nameof(amount));
        }
    }
}
```

ClassifiedAd中的_price物件型別替換為Price類別

```csharp
public class ClassifiedAd
{
    public Guid Id { get; private set; }
    private UserId _ownerId;
    private string _title;
    private string _text;
    private Price _price;
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

    public void UpdatePrice(Price price) => _price = price;
}
```


## Factories

將`SetTitle(string)`,`UpdateText(string)`,`UpdatePrice(decimal)`中的型別皆以domain primitive概念實作類別，並使用工廠方法實例化，建構子改為private不對放開放，僅能透過static FromString(string title)進行實例化。

why is the constructor `private`，因為使用工廠模式，假設title有多個來源可輸入時，工廠模式中的靜態函式可以撰寫相關邏輯，將商業邏輯顯性而不是隱藏在程式碼之中，如title也可以從html來源提供時，即可新增`FromHtml(string)`來表示title擁有2種不同來源，且不同靜態方法中可以特別實作不同的轉換格式提供給ClassifiedAdTitle

```csharp
public class ClassifiedAdTitle:ValueObject
{
    public static ClassifiedAdTitle FromString(string title)=>new  ClassifiedAdTitle(title);

    public static ClassifiedAdTitle FromHtml(string htmlTitle)
    {
        var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "*")
            .Replace("</b>", "*");
        return new ClassifiedAdTitle(Regex.Replace(supportedTagsReplaced, "<.*.?>", string.Empty));
    }

    private readonly string _value;

    private ClassifiedAdTitle(string value)
    {
        if(value.Length>100)
        {
            throw new ArgumentException("Title cannot be longer that 100 characters", nameof(value));
        }
        _value = value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return _value;
    }
}
```
- 同樣方式在Money物件中，可以提供decimal或字串實例化物件，檢查輸入值的格式是否有效

```csharp
public class Money:ValueObject
{
    public static Money Create(decimal amount)=>new Money(amount);

    public static Money Create(string amount)
    {
        if(!Regex.IsMatch(amount,@"/^\d*\.?\d*$/"))
        {
            throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
        }
        return new Money(Decimal.Parse(amount));
    }

    public decimal Amount { get;}

    protected Money(decimal amount) => Amount = amount;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }

    public Money Add(Money sumand) => new Money(this.Amount + sumand.Amount);
    public Money Subtraction(Money subtrahend) => new Money(this.Amount - subtrahend.Amount);
    public static Money operator + (Money sumand1, Money sumand2) => sumand1.Add(sumand2);
    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtraction(subtrahend);
}
```

-  檢查money格式，範例為檢查小數點2位數以內，但並非所有的貨幣都允許小數點2位，例如日圓就取整數，阿曼里亞爾貨幣可以小數點3位，請記永遠要針對所支援的市場地域做不同的檢驗，如貨幣、日期、時間格式、人名、銀行帳號、地址在世界各地上都有不同驚喜，確保你的應用程式規則可以符合當地情況。

- 替money增加貨幣屬性
    - 增加Currency, 預設值為TWD
    - 若貨幣單位不同者，無法進行加減計算
    - Create、建構子新增currency參數
    - Add與Subtract增加貨幣判斷單位，不同者拋出例外 

```csharp
public class Money:ValueObject
    {
        public static Money Create(decimal amount,string currency=DefaultCurrency)=>new Money(amount,currency);

        public static Money Create(string amount,string currency=DefaultCurrency)
        {
            if(!Regex.IsMatch(amount,@"/^\d*\.?\d*$/"))
            {
                throw new ArgumentException("invalid string cannot transfer to decimal", nameof(amount));
            }
            return new Money(Decimal.Parse(amount),currency);
        }

        private const string DefaultCurrency = "TWD";
        public string Currency { get; }
        public decimal Amount { get;}

        protected Money(decimal amount,string currency=DefaultCurrency)
        {
            if (Decimal.Round(amount,2)!=amount)
            {
                throw new ArgumentOutOfRangeException("Amount cannot have more than two decimal", nameof(amount));
            }
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }

        public Money Add(Money sumand)
        {
            if(this.Currency!=sumand.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            return new Money(this.Amount + sumand.Amount);
        }
        public Money Subtraction(Money subtrahend)
        {
            if(this.Currency!=subtrahend.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            return new Money(this.Amount - subtrahend.Amount);
        } 
            
        public static Money operator + (Money sumand1, Money sumand2) => sumand1.Add(sumand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtraction(subtrahend);
    }
```

- 但此種簡單的判斷會造成多國貨幣進行計算與換算的困難度，且開發人員可能會忘記在計算時需相同貨幣單位，造出拋出例外，而為了檢驗全部的貨幣換算問題，第一個選擇是money物件自行驗證與換算，但有任何異動，各國貨幣變動等與貨幣換算的問題都需要修改money物件，此種做法並不建議。

- domain service :  當domain model只專注於特定領域的規則時，當領域中的某個處理流程、執行涉及多個entity及value object不歸屬任何其中一個的職責時，且該執行流程是核心規則的體現，而非application services時，此時就可以獨立出個一個介面，實作domain service
    
    - what
        1. 執行一個業務處理流程    
        2. 對domain model進行轉換
        3. 以多個domain model(aggregate)作為輸入進行計算，產出value object
        4. 無狀態性
    
    - when
        1. 當涉及domain model的計算邏輯
        1. 有多條件式判斷領域概念

- application service : 較多為技術實作，與商業邏輯無關
    1. 消息驗證
    2. 錯誤處理
    3. 監控
    4. transaction
    5. 認證與授權 

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
[git]: ../git/git.md "git"
[dotnet-sln]: ../../../2-code/learning/tool/dotnet/dotnet-sln.md "Dotnet Sln"
[eventstorming]: eventstorming.md "eventstorming"
[domain-primitive]: domain-primitive.md "domain-primitive"
[//end]: # "Autogenerated link references"