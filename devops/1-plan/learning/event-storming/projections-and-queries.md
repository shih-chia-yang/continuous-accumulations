# projections-and-queries

[[event-sourcing]]

## events and queries

CQRS主要流行的原因是server需要針對不同的操作進行優化，因此需要分開處理讀取與寫入，對於第三正規化的關聯資料庫，容易寫入，難以讀取，不好應對巨量的transaction loading
並同時保持複雜查詢的效能。
同樣的問題也適用於使用event做為資料庫記錄的系統，通過append的方式儲存業務事件，無法直接取得系統狀態，為了取得特定資料，需要讀取該物件所有的事件流，並滾動更新。假設需要
巨量的數據查詢時，代表著可能需要將整個系統讀取至記憶體，然後再進行包含多個物件的數據集合處理，而這樣的系統根本沒辦法運作。
這就是在event-sourced系統幾乎每個都實作CQRS的原因，有些系統人們稱之為事件源，它不斷更新替代數據庫中的系統狀態，例如，關係數據庫，與系統產生的事件同時發生。有時，這發生在記憶體中，或是在一個transaction中， 如果事件與系統狀態快照存儲在同一數據庫中。在這樣的系統中，您通常會發現事件實際上幾乎沒有用於任何事情。
應用服務不會從事件中加載對象，而是從快照數據庫中獲取最新的對象狀態。說event是這樣的系統中的source of truth有點牽強，本書不會研究這種作法。


## projection

when we were retrieving a subset of attributes from a larger dataset of the whole system state,to be used for queries.

command --->      Application service-|                  |-->   Projection    
                  ^                   |              |---|                |
   read from store|   commit to store |              |subscription        | update
                  |                   V              |                    V
aggregate events--|                       new event                  read model



## subscriptions

read model可能是來自不同的資料庫，使得所有的查詢會最終一致性。
需要了解目標是將event append to stream和read model更新之間的時間間隔最小化，在這2個操作之間的時間內，read model顯示的數據仍是舊的，但不代表不一致，只是不完全是最新的，
經過一段時間延遲，查詢最終會更新至最新數據。
為了盡量減少時間間隔，我們需要確保我們的projections即時接收new event。eventstore在這裡可以提供幫助，因為它有非常好的訂閱功能。有兩種類型的訂閱——catch-up和persistent訂閱，也稱為競爭消費者。主要區別在於檢查點所有權。

檢查點是流中的特定位置。由於投影處理了一個事件，它可以存儲檢查點，因此如果投影重新啟動，它將知道從哪裡開始處理，而不是從列表的開頭投影所有事件。
檢查點的概念在所有處理實時事件處理的系統中都是眾所周知的，例如 kafka 或 Azure 事件中心。
可以使用 sql server 表來存儲事件並使用自動遞增的主標識作為stream position。然後，您可以不斷輪詢此表以獲取新事件，這樣您就可以擁有一個有效的訂閱。

有2種方法可以儲存check point

1. client-based
    - 由client side維護與訂閱
2. server-based
    - event consumer競爭消費者，可進行多個projection

## catch-up subscriptions

## Cross-aggregate projections

當處理多個資料來源的數據，最明顯的方式是在edge 組合數據。最多人使用的技術是BFF(backend for frontend)，當前端需要獲取的些複合數據時，向後端單個API endpoint發送請求，API本身會調用不同數據來源的數據並合併返回。
                                            Read Model
                                          |---> PublicClassifiedAds
web app ----> Backend for frontend API ----
                                          |---> UserProfiles


1. 簡單: 對資料庫使用join，因為知道需要查詢的2個數據的鍵值
2. 進階: 針對2個不同微服務API的 remote calls，然後在記憶體中處理
    - 服務失效
    - 網路故障
    - 每次使用都必須重新連接

有幾種方法可以在read model獲取比在projection中接收到的事件獲取更多數據

- querying from a projection
- event up casting


## querying from a projection



ClassifiedAdCreated
    ClassifiedAdId  ---->Handle----> ClassifiedAdDetails Projection------->insert ClassifiedAdDetails Read model
    OwnerId                                    ^                                 ClassifiedAdId
                                               |                                 OwnerId
                                               |                                 OwnerDisplayName
                                               V
                                        UserDetails Read model
                                            UserId
                                            DisplayName


1. 建構子新增一個delegate function
2. 在ClassifiedAdCreated中SellerDisplayName=delegate function
3. 在DI建立ClassifiedAdDetailsViewModel，利用委派函數將DisplayName傳入建構子

在projections中使用查詢時需要注意很多方面，主要是為了確保可靠性。此類工作的主要目標是確保projections永遠不會失敗。當您需要查詢的數據與您正在更新的讀取模型位於同一個儲存機制中時，查詢的處理速度和可靠性應該處於可接受的水平。您可能仍希望對整個projections應用重試策略，以減輕瞬時網絡故障和類似情況的問題。

## upcasting events

將更多數據放入讀取模型的最複雜方法是使用事件向上轉換。
需要建立一個單獨的事件存儲訂閱，用於接收slim event，從其他地方獲取額外數據，生成一個包含更多數據的新事件，並將其發佈到特殊流。這個流永遠不可能是聚合流，因為新事件只需要構建讀取模型。我們可以為流選擇一個特殊的名稱，例如 ClassifiedAd-Upcast。由於讀取模型投影會偵聽 $all 流，因此它也會接收和處理這些事件。
此方法僅在不同讀取模型需要額外數據時有用，因此我們可以使用一個enrich event更新所有這些數據，因此我們只需要查詢一次額外數據。


新增一個更新數據專用的事件
```csharp
public static class ClassifiedAdUpcastedEvents
{
    public static class V1
    {
        public class ClassifiedAdPublished
        {
            public Guid Id { get; set; }

            public Guid OwnerId { get; set; }

            public string SellerPhotoUrl { get; set; }

            public Guid ApprovedBy { get; set; }
        }
    }

}
```

將AppendEvent方法修改為擴充方法
```csharp
public static class EventStoreExtensions
    {
        public static Task AppendEvents(this IEventStoreConnection connection,
        string streamName,long version,
        params object[] events)
        {
            if(events==null || !events.Any()) 
                return Task.CompletedTask;
            var preparedEvents = events
            .Select( @event=>new EventData(
                eventId:Guid.NewGuid(),
                type:@event.GetType().Name,
                isJson:true,
                data: Serialize(@event),
                metadata:Serialize(new EventMetadata{
                    CLRType=@event.GetType().AssemblyQualifiedName
                })
            )).ToArray();
            return connection.AppendToStreamAsync(streamName, version, preparedEvents);
        }

        private static byte[] Serialize(object data)
            => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
```

實作專門整合數據專用的訂閱事件處理，訂閱domain event ClassifiedAdPublished，接收到資訊後，取的userItems最新photourl數據，再儲存為新事件
發送至EventStore

```csharp
public class ClassifiedAdUpcasters : IProjection
{
    private readonly IEventStoreConnection _connection;
    private readonly Func<Guid, string> _getUserPhoto;
    private const string StreamName="UpcastedClassifiedAdEvent";

    public ClassifiedAdUpcasters(
        IEventStoreConnection connection,
        Func<Guid,string> getUserPhoto)
    {
        _connection = connection;
        _getUserPhoto = getUserPhoto;
    }
    public async Task Project(object @event)
    {
        switch(@event)
        {
            case ClassifiedAdPublished e:
                var photoUrl = _getUserPhoto(e.OwnerId);
                var newEvent = new ClassifiedAdUpcastedEvents.V1.ClassifiedAdPublished
                {
                    Id = e.Id,
                    OwnerId=e.OwnerId, 
                    SellerPhotoUrl=photoUrl,
                    ApprovedBy=e.ApprovedBy};
        await _connection.AppendEvents(StreamName, ExpectedVersion.Any, newEvent);
                break;
        }
    }
}
```

ClassifiedAdProjection，新增訂閱數據更新事件，進行photourl資訊更新
```csharp
//在switch新增此事件
case V1.ClassifiedAdPublished e:
    UpdateItem(e.Id, ad => ad.SellersPhotoUrl = e.SellerPhotoUrl);
    break;
```

該實作方法同樣於eShopContainer範例中的IntegrationEvents，訂閱來自其他服務發送的事件，進行處理後再轉發事件通知訂閱方執行或數據更新


## persistent storage



[//begin]: # "Autogenerated link references for markdown compatibility"
[event-sourcing]: event-sourcing.md "event-sourcing"
[//end]: # "Autogenerated link references"
