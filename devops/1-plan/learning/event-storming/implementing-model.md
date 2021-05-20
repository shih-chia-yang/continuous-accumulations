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

## Domain Service

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

- 使用tdd方式，練習實作ExchangeService，主要過程以commit記錄呈現 [[money_spec]] [[coding_record]]

## entity invariants

[[classifiedad_publish_spec]]

- code position 
    1. ClassifiedAd.cs
    2. ClassifiedAdValidation.cs
    3. ClassifiedAd_Publish_Spec.cs
    4. InvalidEntityStateException.cs

因為domain primitive是可自行驗證的value object，使得entity中減少大量的檢查邏輯，可專注於真正的核心商業邏輯。
    - 簡化validation
    - 提供封裝
    - 允許型別安全

將command往左移動或是往右移動，試著去找出在執行後產生事件前時應避免什麼錯誤。

core rules: when seller want to Publish Ad

|<- |time flow | -> |
|--|--|--|--|
||title must not be empty| |
|Publish ad|text must not be empty|Ad requested to be published|
||price must not be specified| |

當classifiedAd被建立時，state為Inactive；當ad需要被發佈時狀態會被改變為PendingReview

1. 可使用contract programming, 將發佈事件前置檢查的條件稱為precondition，發佈事件後檢查稱為post condition，逐條獨立撰寫，並使用組合方式測試是否可完全覆蓋，當測試通過，代表合約正確

- [code contracts](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts)

- [null-references](https://www.infoq.com/presentations/Null-references-The-Billion-Dollar-Mistake-Tony-Hoare)
- [c# nullable](https://github.com/dotnet/csharplang/blob/master/proposals/nullable-reference-types.md)

## domain events

making domain events first-class citizens in the domain model has excellent benefits.there are two primary use cases for domain events that are implemented explicitly as part of the domain model

- 允許系統中的一部份將狀態改變以訊息方式通知其他部份。將系統拆分多個區塊，這些區塊需要通過訂閱事件與執行操作才能運作良好，如果建構方式是以各區塊針對彼此的狀態變更而做出反應，這樣的系統被稱為`reactive system`
    
- 將domain events藉由持久化機制儲存，可獲取domain model內部狀態變化的完整歷史訊息，然後可以通過這些事件並重新建構任何實體的狀態，這種模式被稱為`event sourcing`(事件溯源)

1.  domain events as objects

domain events 需要在系統各部份之間傳遞，所以需能夠支援序列化，因struct無法序列化，事件將會使用class實作，代表事件的類別需要清楚的描述事件(what happened)，包括解釋系統狀態如何變化的必要訊息，事件是針對命令執行的反應。

盡量將事件保持簡單，內容含有複雜的value object時，需要額外處理flattered。

- Classified entity
    - Create a new classified ad
    - set the ad title
    - update text
    - update price
    - publish the ad(send to review)

|operation|event|
|--|--|
|create an Ad | Ad created|
|change the Ad title| Ad title changed|
|update the Ad text | Ad text updated|
|update Ad sell price|Ad sell price updated|
|request to publish the Ad|Ad requested to be published|

- example
```csharp
public class ClassifiedAdSentToReview
{
    public Guid Id { get; set; }
}
```


2. Raising events

新增abstract entity class
- 提供集合收集事件
- 提供清除事件
- 提供取得事件清單

```csharp
public abstract class Entity
    {
        private readonly List<object> _events;

        protected Entity() => _events = new List<object>();

        protected void Raise(object @event) => _events.Add(@event);

        public IEnumerable<object> GetChanges() => _events.AsEnumerable();

        public void ClearChanges()=>_events.Clear();
    }
```

ClassifiedAd繼承entity，在上述表格中發生行為的函式中 將事件加入集合中
example：
```csharp
public void UpdatePrice(Price price)
{
    Price = price;
    Raise(new ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.CurrencyCode));
}
```

當完成事件實例化，可以藉由類似下方程式碼(非正式用)，展示如何藉由domain event來整合系統不同部份。
當發送事件到event bus後，其他服務有訂閱該事件的話，他們就會接到事件並開始針對事件執行作業，改變domain model的狀態，或執行特定作業，如寄送email，文字訊息，實時提醒等。

```csharp
public async Task Handle (RequestToPublish command)
{
    var entity=await _repository.Load<ClassifiedAd>(command.id);
    entity.RequestToPublish();
    await _repository.Save(entity);
    foreach( var @event in entity.GetChanges)
    {
        await _bus.Publish(@event);
    }
}
```
可以使用`implicit`將value直接指向value object，實例化事件賦值時可以直接使用primitive type
```csharp
public static implicit operator Guid(UserId self) => self._value;
```

## event change state

events代表發生狀態變化的事實，代表如果不與domain event進行互動的話，無法改變系統狀態。
目前的程式碼中更改系統狀態與觸發domain event是完全分開的，仍需要做一些變動。將`Raise`更名為`Apply`，且不再只是將事件加入集合中，透過`When`將每個事件的內容應用在entity state，且需在每個entity中進行實作，同時也會呼叫`EnsureValidState`

```csharp
public abstract class Entity
    {
        private readonly List<object> _events;

        protected Entity() => _events = new List<object>();
        public IEnumerable<object> GetChanges() => _events.AsEnumerable();
        public void ClearChanges()=>_events.Clear();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        protected abstract void When(object @event);

        protected abstract void EnsureValidState();
    }
```

下一步將apply domain event與狀態變化全部移到`When` method
1. 將所有公開方法修改為將entity state information 傳送給 domain event, 移除狀態變化與驗證。驗證的部份已由entity base class中的`Apply`呼叫。
2. 實作When方法，並使用c#7.1提供的switch pattern matching的功能識別事件與需要更改的狀態

```csharp
public void UpdateText(ClassifiedAdText text)=>
            Apply(new ClassifiedAdTextUpdated(Id, text.Value));
```

```csharp
protected override void When(object @event)
{
    switch(@event)
    {
        case ClassifiedAdCreated e:
            Id = e.Id;
            OwnerId = new UserId(e.OwnerId);
            State = ClassifiedState.Inactive;
            break;
        case ClassifiedAdTitleChanged e:
            Title = ClassifiedAdTitle.FromString(e.Title);
            break;
        case ClassifiedAdTextUpdated e:
            Text = ClassifiedAdText.FromString(e.Text);
            break;
        case ClassifiedAdPriceUpdated e:
            Price = Price.Create(e.Price,Currency.Create(e.CurrencyCode,2));
            break;
        case ClassifiedAdSentToReview e:
            State = ClassifiedState.PendingReview;
            break;
    }
}
```

- 個人心得：
    - 疑問:若entity的更改資料部份僅限於內部狀態變更無需通知系統其他entity，是否可使用command即可，而需要與其他物件互動的command則需要domain event配合，如本書範例，是否僅publish命令需要實作domain event，其他更改資料部份，若無牽涉其他模組或自身無需達成acid，則直接使用command
    
    - 步驟：將動作的參數傳遞給事件，由底層加入事件，並由事件來觸發command與驗證，該範圍為每個動作皆會呼叫`apply`，理應可以只加入事件，再一併觸發，而不用單獨一個一個觸發。
    - when的部份，應該可以修改藉由di註冊，由事件型別找出繼承該事件的command，再將物件注入command，執行handle，而不用全部寫在when，可拆分為事件與命令物件分別撰寫
        - 書中範例的方法較易理解，但當狀態變化很多與大量操作方法時會增加when的複雜度
        - 拆分command與事件，由di控制與觸發，在複雜情形下可減少各狀態變與操作的相互依賴，但較不易理解全貌。

一般使用ddd的general event與domain event不一定要使用event sourcing。本書較著重`event sourcing`，所以在此部份就出現利用事件來更改物件狀態的方法。

- complex value object : 透過單一職責原則，將驗證獨立方法出來，並將建構子設為internal封裝，實例化僅能透過靜態工廠方法來建立，而在靜態工廠方法的處理流程加入驗證，即可保證物件被正確的建立，並使用implicit覆寫value至型別，簡化賦值事件的作業。
- 
```csharp
public class ClassifiedAdTitle:ValueObject
{
    public static ClassifiedAdTitle FromString(string title)
    {
        Validate(title);
        return new ClassifiedAdTitle(title);
    }

    public static ClassifiedAdTitle FromHtml(string htmlTitle)
    {
        var supportedTagsReplaced = htmlTitle
            .Replace("<i>", "*")
            .Replace("</i>", "*")
            .Replace("<b>", "*")
            .Replace("</b>", "*");
        var value = Regex.Replace(supportedTagsReplaced, "<.*.?>", string.Empty);
        Validate(value);
        return new ClassifiedAdTitle(value);
    }

    public string Value { get; }

    private ClassifiedAdTitle(string value)=>Value = value;

    private static void Validate(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException("Title cannot be null or empty", nameof(value));
        }
        if(value.Length>100)
        {
            throw new ArgumentException("Title cannot be longer that 100 characters", nameof(value));
        }
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string (ClassifiedAdTitle title) => title.Value;
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
[git]: ../git/git.md "git"
[dotnet-sln]: ../../../2-code/learning/tool/dotnet/dotnet-sln.md "Dotnet Sln"
[eventstorming]: eventstorming.md "eventstorming"
[domain-primitive]: domain-primitive.md "domain-primitive"
[coding_record]: ../../../2-code/practices/maketplace/docs/coding_record.md "coding_record"
[classifiedad_publish_spec]: ../../../2-code/practices/maketplace/docs/classifiedad_publish_spec.md "classifiedad_publish_spec"
[//end]: # "Autogenerated link references"