# acting with commands

[[implementing-model]]

- application layer
- calling the domain model from the web api
- persisting the domain model changes

## technical requirements

- docker, using containers to run database [[docker]]

## api

[[web_api]]

到目前為止我們保持domain model與基礎設施、持象化機制、執行、通訊等是完全隔離的，使domain model不依賴以上機制與專注在業務領域上。而現在將以domain model為中心開始向外擴展服務, 利用web api
向user提供操作domain model

## public API contracts

Contracts 是由 DTO(data-transfer objects)與 POCOs(plain-old C# objects)組成，
他們不需要任何邏輯，只需要基本型別與不需要任何處理即可進行序列化與反序列化。
實作上，欄位的型別可以來自其他contract的dto, 但注意使用complex type會有兼容性問題，當你更改此類別時，所有使用該類別的contract也會受到影響且是隱式的，由於該contract在內部沒有使用，僅提供給http request，所以你無法得知client端是否會受到此影響，此類資訊只能通過測試得知。

example:
```csharp
public class UpdateCustomerAddressDetails
{
    public string BullingStreet{get;set;}
    public string BillingCity{get;set;}
    public string BillingPostalCode{get;set;}
    public string BillingCountry{get;set;}
    public string DeliverStreet{get;set;}
    public string DeliveryCity {get;set;}
    public string DeliveryPostalCode {get;set;}
    public string DeliveryCountry {get;set;}
}
```

```csharp
public class Address
{
    public string Street{get;set;}
    public string City {get;set;}
    public string PostalCode {get;set;}
    public string Country {get;set;}
}

public class UpdateCustomerAddressDetails
{
    public Address BillingAddress{get;set;}
    public Address DeliveryAddress{get;set;}
}
```
public Api任何有權限的人都可以使用，意味著你無法控制使用API的人，任何改變都可能會造成外部系統發生錯誤，所以在修改public contract時必須僅慎處理，會有breaking change與non-breaking change2種情形發生。

POCO 型別的某些變更是屬於non-breaking change

- 更改屬性的型別，使任何值都能通過序列化轉換為新的型別，例如可以將屬性改為integer改為string
- 新增屬性也被視為non-breaking change，因為當我們反序列化該屬性的xml或json物件時，因為使用人尚未更新contract，而大多數的序列化工具在沒有提供值的情況下會使用默認值。

隨著新技術出現，不可能永遠都是non-breaking changes，意味著我們應該對breaking changes準備解決方案。
api在變更時應考量兼容性，讓使用old api的使用者在一段時間內仍可正常運作，不會馬上出現異常([[wen_api_version]])

- 為contract進行版本控制, 使用巢狀靜態類別取代namespace，使我們可以在同一個檔案中可以有更多版本，且在呼叫靜態方法時使程式碼較簡潔

```csharp
public static class ClassifiedAds
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
            }
        }
    }
```

commands 允許使用者或其他系統執行domain model的動作， 當command成功執行，domain model的狀態變更並產生一個domain event，我們已經將command完成了，現在需要讓command可以透過http request傳送。

## http endpoint 

[[aspnet_core]]

[[controller]]

[[routing]]

- api專案安裝 Microsoft.VisualStudio.Web.CodeGeneration.Design

```dotnetcli
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

- 新增`ClassifiedAdController.cs`

```dotnetcli
dotnet aspnet-codegenerator controller -name ClassifiedAd -api -async -outDir Controllers 
```

```csharp
[Route("api/ad")]
[ApiController]
public class ClassifiedAdController : ControllerBase
{
    public ClassifiedAdController()
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
    {
        return Ok();
    }
}
```
以上程式碼尚未完成，僅為示例。

透過建立web api讓服務可以從外界接收command，使用http infrastructure做為中介，而這個部份在onion arch中稱為 `the edge`，，因為它所屬的層是屬於最外圍的部份。

- application startup [[modify_contentroot]] [[program]] [[appsettings]]
    1. build the configuration 
    2. configure the web host
    3. execute the web host 

- 安裝`Swashbuckle.AspNetCore` [swagger](https://swagger.io/)

```dotnetcli
dotnet add package Swashbuckle.AspNetCore
```

在net5.0下環境使用預設即可，書中範例為netcoreapp2.1
執行api project並使用swagger api，測試try it out等其他功能

選擇新增的/api/ad的預設動作，回傳http status code 200

```dotnetcli
dotnet watch run or dotnet run
```

## application layer

以上呈現一個簡單的web api，從外界接收參數。主要任務是接收來自json、xml、來自rabbitMQ的訊息，或其他通訊管道的序列化資訊，將它轉換成command並確保正確執行它。

edge component可以直接使用domain model,這代表我們只接受一種edge type，而且edge component通常都嚴重依賴通訊基礎架構，雖然對整合測試有很好的幫助，但在建立單元測試會遇到挑戰。為了將通訊基礎架構與request execute隔離，通常會引入application layer，在這一層中會建立一個component，該組件接收來自edge component的命令，並使用domain model來處理這些命令，而這種component被稱為`application service`

| > |user| < | |
|--|--|--|--|
|v||^||
|deserialize| |return 200 ok |edge |
|v||^||
|load an entity by id=>|execute operation via the entity model =>|persist the modified entity|application service|
|^| ^ v| ^ |
| ^|entity|^|domain model|
| ^| | ^|
| < | entity storage| > |persistence|

建立一個ClassifiedAdsApplicationService，以組合的概念將domain model與http request (edge type)、持久化機制等其他component在此進行互動。

```csharp
public class ClassifiedAdsApplicationService
{
    public void Handle(Contract.ClassifiedAds.V1.Create command)
    {
            
    }
}
```

加入DI註冊,使用AddSingleton
```csharp
services.AddSingleton(new ClassifiedAdsApplicationService());
```

在controller中使用di註冊的application service，並在endpoint中呼叫，傳入Http request接收到的強型別

```csharp
public class ClassifiedAdController : ControllerBase
{
    private readonly ClassifiedAdCreatedCommand _created;

    public ClassifiedAdController(ClassifiedAdCreatedCommand created)
    {
        _created = created;
    }

    [HttpPost]
    public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
    {
        _created.Handle(request);
        return Ok();
    }
}
```

## the command handler pattern

- 新增介面 ICommandHandler
```csharp
public interface ICommandHandler<in TRequest>
where TRequest:class
{
    Task Handle (TRequest command);
}
```

- ClassifiedAdCreatedCommand繼承ICommandHandler

```csharp
public class ClassifiedAdCreatedCommand:ICommandHandler< ClassifiedAds.V1.Create>
{
    private readonly IClassifiedAdRepository _repo;
    public ClassifiedAdCreatedCommand(IClassifiedAdRepository repo)
    {
        _repo = repo;
    }
    public Task Handle (ClassifiedAds.V1.Create command)
    {
        var classifiedAd = new ClassifiedAd(Guid.NewGuid(), new UserId(Guid.NewGuid()));
        return _repo.Save(classifiedAd);
    }
}
```

- 修改startup.cs di註冊

```csharp
services.AddScoped<ICommandHandler<ClassifiedAds.V1.Create>,ClassifiedAdCreatedCommand>();
```

- 修改controller

```csharp
public class ClassifiedAdController : ControllerBase
{
    private readonly ICommandHandler<ClassifiedAds.V1.Create> _created;

    public ClassifiedAdController(ICommandHandler<ClassifiedAds.V1.Create> created)
    {
        _created = created;
    }

    [HttpPost]
    public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
    {
        await _created.Handle(request);
        return Ok();
    }
}
```


- RetryCommandHandler

```csharp
public class RetryingCommandHandler<T> : ICommandHandler<T>
{
    static RetryPolicy _policy = Policy.Handle<InvalidOperationException>().Retry();

    private ICommandHandler<T> _next;

    public RetryingCommandHandler(ICommandHandler<T> next)
    {
        _next = next;
    }

    public Task Handle(T command)
    {
        await _policy.ExecteAsync(() =>_next.Handle(command));
    }
}
```

- startup.cs

```csharp
services.AddScoped<ICommandHandler<V1.Create>>(c=> 
    new RetryCommandHandler<V1.Create>(
        new ClassifiedAdCreatedCommand(c.GetService<ClassifiedAdRepository>())
    ));
```
command handler pattern遵循 單一職責原則(Single Responsibility Principle)，每個http的方法都需要分注入依賴命令處理程序。當方法數量很多時，情況就會變得不一樣，透過event storming來看ClassifiedAd的討論結果，就會至少超過10個方法，代表著相當多數量的command handler。

此書作者只是展示command handler pattern，但不使用它，取而代之的是使用application service實作command handling
z
## application service

類似一堆command handler變成application service的行為，一般application service呈現的多參數方法如下所示，但該種方法在pipeline中會造成維護困難，想要將這個service加入到logging、retry process非常不容易，使用pipeline的所有的處理程序在參數上需要兼容性，pipeline的所有程序都要有相同的參數集合，這造成如果某一個處理程序必須增加一個參數，就必須對pipeline中所有程序進行大量更改。

```csharp
public interface IPaymentApplicationService
{
    Guid Authorize(
    string creditCardNumber,
    int expiryYear,
    int  expiryMonth,
    int cvcCode,
    int camount);
    
    void Capture(Guid authorizationId);
}
```

或是application service 實作擁有大量方法的IHandle<T>的介面，但同樣的每個command都需要分別使用DI註冊,如下
```csharp
services.AddScope<IHandleCommand<V1.Create>>(c=>
    new RetryingCommandHandler<V1.Create>(
        new CreateClassifiedAdHandler<c.GetService<RavenDbEntity>>()));
services.AddScope<IHandleCommand<V1.Create>>(c=>
    new RetryingCommandHandler<V1.Rename>(new RenameClassifiedAdHandler(c.GetService(RavenDbEntityStore()))));
```

作者使用 實作Task Handle(object command)的介面，內部使用switch case將所有command集中處理，也是有提到違反SRP原則，但application service得到很好的內聚力。
但個人感想是，同樣會遇到大量command handler方法集中一個函式中，經過長久維護依然是要面對大量的程式碼
    
1. 將switch case中的每個case拆分為private方法，handle僅是command與事件的篩選器
2. 因人而異，端看開發者喜歡如何分類，是要將所有方法集中在一個類別或是拆分成command handler後集中在同一個資料夾內
3. 當檔案中的程式碼內容量大時，認知複雜度隨之增加。
4. 關於pipeline的方法，應可以使用custom middleware的方法來實現

- 根據event storming的資訊，建立各種command的contract

```csharp
public static class ClassifiedAds
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
            }

            public class SetTitle
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
            }

            public class UpdateText
            {
                public Guid Id { get; set; }
                public string Text { get; set; }
            }

            public class UpdatePrice
            {
                public Guid Id { get; set; }

                public decimal Price { get; set; }
                public string Currency { get; set; }
            }

            public class RequestToPublish
            {
                public Guid Id { get; set; }
            }
        }
    }
```
- extend edge ，controller新增可以接受command的http request 
- 實作application service方法並封裝處理程序，edge case 僅需呼叫同一個Handle方法
- edge 不需要參考domain model，僅需處理有關http request與合約的部份

```csharp
[Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly ICommandHandler<ClassifiedAds.V1.Create> _created;

        private readonly IAppService _service;

        public ClassifiedAdController(
            ICommandHandler<ClassifiedAds.V1.Create> created,
            IAppService service)
        {
            _created = created;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
        {
            await _service.Handle(request);
            // await _created.Handle(request);
            return Ok();
        }

        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> SetTitle(V1.SetTitle request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("text")]
        [HttpPut]
        public async Task<IActionResult> UpdateText(V1.UpdateText request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("price")]
        [HttpPut]
        public async Task<IActionResult> UpdatePrice(V1.UpdatePrice request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> RequestToPublish(V1.RequestToPublish request)
        {
            await _service.Handle(request);
            return Ok();
        }
    }
```

```csharp
public class ClassifiedAdAppService : IAppService
    {
        private readonly IClassifiedAdRepository _repo;
        public ClassifiedAdAppService(IClassifiedAdRepository repo)
        {
            _repo = repo;
        }
        public Task Handle(object command) =>
        command switch
        {
            V1.Create cmd => Created(cmd),
            V1.SetTitle cmd => SetTileAsync(cmd),
            V1.UpdateText cmd => UpdateTextAsync(cmd),
            V1.UpdatePrice cmd => UpdatePriceAsync(cmd),
            V1.RequestToPublish cmd => RequestToPublishAsync(cmd),
            _ => Task.CompletedTask
        };

        private async Task RequestToPublishAsync(V1.RequestToPublish cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.RequestToPublish();
            await _repo.Save(entity);
        }

        private async Task UpdatePriceAsync(V1.UpdatePrice cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.UpdatePrice(Price.Create(cmd.Price, Currency.Create(cmd.Currency, 2)));
            await _repo.Save(entity);
        }

        private async Task<ClassifiedAd> FindAsync(string id)
        {
            var entity = await _repo.Load(id);
            if(entity==null)
                throw new InvalidOperationException($"Entity with id {id} cannot be found");
            return entity;
        }

        private async Task UpdateTextAsync(V1.UpdateText cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.UpdateText(ClassifiedAdText.FromString(cmd.Text));
            await _repo.Save(entity);
        }


        private async Task SetTileAsync(V1.SetTitle cmd)
        {
            var entity = await FindAsync(cmd.Id.ToString());
            entity.SetTitle(ClassifiedAdTitle.FromString(cmd.Title));
            await _repo.Save(entity);
        }

        private async Task Created(V1.Create cmd)
        {
            if(await _repo.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exist");
            var classifiedAd = new ClassifiedAd(cmd.Id, new UserId(cmd.OwnerId));
            await _repo.Save(classifiedAd);
        }
    }
```

[//begin]: # "Autogenerated link references for markdown compatibility"
[implementing-model]: implementing-model.md "implementing-model"
[docker]: ../../../7-operate/learning/docker/docker.md "Docker"
[web_api]: ../../../2-code/learning/development/aspnet-core/project/webapi/web_api.md "web_api"
[wen_api_version]: ../../../2-code/learning/development/aspnet-core/wen_api_version.md "web_api_version"
[aspnet_core]: ../../../2-code/learning/development/aspnet-core/aspnet_core.md "aspnet_core"
[controller]: ../../../2-code/learning/development/aspnet-core/controller.md "controller"
[routing]: ../../../2-code/learning/development/aspnet-core/project/routing/routing.md "routing"
[modify_contentroot]: ../../../2-code/learning/development/aspnet-core/modify_contentroot.md "modify_contentroot"
[appsettings]: ../../../2-code/learning/development/aspnet-core/appsettings.md "appsettings"
[//end]: # "Autogenerated link references"