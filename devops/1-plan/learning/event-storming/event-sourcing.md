# event-sourcing

## technical requirement

[[docker]]

[[docker-compose]]

[[eventstore]]

1. 按照docker官網步驟安裝docker-compose
2. 前往eventstore官網，查閱docker部署相關環境變數
3. 建立docker-compose.yml
4. 執行docker-compose up -d
5. 測試sql連線與eventstore

## issues with state persistence

[domain-driven design is linguistic](http://verraes.net/2014/01/domain-driven-design-is-linguistic)


目前所完成的部份，提到`Domain Event`很多次，在設計階段時使用橘色便利貼來表示，將domain event實作為class，這些類別將系統所發生的事轉換為機器可讀的形式。
domain model的每個動作中，以aggregate中的方法表示並改變系統的狀態，透過事件來描述這些變化。當改變發生時，透過程式碼在儲存至資料庫前先改變aggregate state。

events(Ad created)--->Aggregate--->State(Id)--->events(Ad Rename)--->Aggregate--->State(Id,Title)--->依此類推

aggregate state透過該aggregate repository commit至資料庫中，而每次執行aggregate的操作時，透過repository中的Get方法從資料庫中還原aggregate state。
當狀態改變時，都會覆寫前一次的狀態，資料庫儲存著系統的快照，盡管是由很多次變化，才使系統狀態變化成最後的狀態。

events(Ad created)--->Aggregate--->State(Id)--->events(Ad Rename)--->Aggregate--->State(Id,Title)--->依此類推--->Commit()--->database

aggregate中的每個動作執行:

database ---> Get--->(Id,Title,Description,Price)--->Apply(Ad sell Price updated, new price:price)--->When(Id,Title,Description,Price)--->Commit--->database

以上流程僅適用假設我們只需要系統最新的狀態，我們可以知道ClassifiedAd的最新售價，然而Product Owner如果表示需要呈現售價的改變歷史，現階段無法做到。
而對開發者而言，時常需要解決系統狀態出現非預期或無效的值，通常可以透過log file來觀察到底如何發生，但如果無法做到的話，通過都是詢問使用者是如何操作的，但使用者永遠會說他們沒有做錯，或是沒做任何事，它就這樣發生了。
而有些時候這些問題會存在數月、甚至數年，開發人員無法確定問題的原因，因為無法得知事件發生的順序進而導致目前的問題。收集改變歷史的資料，可以透過日誌來解決，但日誌所記錄的資料不完全都與event processing有關聯，可能會有些event無法被紀錄到。


- Events are recorded for each operation, so an object state can be reconstructed by reading all those events.
- Event cannot be changed or removed,because such an operation would undermine the whole concept of the audit log and make it invalid.

## event streams

1. 取得單個aggregate的事件，最好是一次讀取，如果有巨量事件，則需要將事件拆分成多個批次
    - 事件需加入可識別aggregate唯一性的metadata

2. 事件必須與寫入資料庫是相同順序進行讀取，當將數據寫入資料庫時，這些事件需要按照發送到資料庫的正確順序寫入

以特定順序進入系統的事件形成event stream。最好的方式是資料庫允許每個aggregate都有自己的stream。我們會讀取已寫入的stream，stream name是以{aggregate name}-{identity id}格式命名。aggregate stream包含aggregate生命週期中所有發生的事件。當決定不需要該aggregate時，可以刪除stream或發送一個刪除的事件，如:ClassifiedAdRemove

用來做為持久化事件的資料庫，除了獨立分開的aggregate stream之外，還有一個包含所有系統事件的
stream，透過控制identity metadata來識別每個aggregate stream，以防資料庫不支援獨立分開的stream。該流統稱為$all stream。


ClassifiedAd-111 : ----[]---- ---> Ad Created ---> Ad Renamed ---> ----[]---- --->Ad Removed

ClassifiedAd-123 : Ad Created ---> ----[]---- ---> ----[]---- ---> Ad Description update

$all             : Ad Created ---> Ad Created ---> Ad Renamed ---> Ad Description update ---> Ad Removed

## event stores

database:
1. store events and metadata
2. put indexes on the metadata
3. cannot put any indexes on events , because there is not single denominator for event objects; they are all different. 
4. metadata is structured in a known way.for example, the stream name must be present in the metadata for all events

|database|how to store events | how to read a single stream|
|--|--|--|
|RDBMS(SQL Server,PostgreSql,等)| Use a single table; add one column for the stream name and one column for the event payload. one row is one event | select all rows where the stream name is what we want|
|Document database(MongoDB,Azure Cosmos DB,RavenDb) | Use a document collection. each document should have a metadata object and a filed to store the payload. one document is one event|
query all documents where the stream name(part of the metadata) is what we need |
|Partitioned tables(Azure Table Storage,AWS DynamoDB)| use a single table; add one field for the stream name(or ID) to be used as the partition key and another filed as the row key(Azure) or sort key(DynamoDB). the third filed will contain the event payload. one record is one event|
query all records where the partition key is the name of the stream we are reading|
|Specialized database(Event store)|Native support for streams|read all events from a single stream|


tool

1. [marten framework](http://jasperfx.github.op/marten/), used the native PostgreSql feature to store unstructured data in JSONB-type column
2. [SQL Stream Store](https://github.com/SQLStreamStore/SQL)

## Event-oriented persistence

新增interface

```csharp
public interface IAggregateStore
{
    Task<bool> Exists<T,TId>(TId aggregateId);
    Task Save<T,TId>(T aggregate) where T:AggregateRoot;
    Task<T> Load<T,TId>(TId aggregateId)where T:AggregateRoot;
}
```

add package
```dotnetcli
dotnet add <project path> package Newtonsoft.Json
dotnet add <project path> package EventStore.Client
```

aggregate-per-stream:

1. 寫入一個stream就是一個事件，一個stream和一個aggregate boundary成為transaction boundary。
2. stream name = {aggregate name}-{aggregate.Id}

```dotnetcli
dotnet add <project path> package Newtonsoft.Json
dotnet add <proejct path> package EventStore.Client="5.0.9" 
```

## reading from event store

1. find out the stream name for an aggregate
2. read all of the events from the aggregate stream
3. loop through all of the events,and call the When handler for each of them

Load() method

1. ensures that the aggregate ID parameter is not ull
2. Gets the stream name for a given aggregate type
3. Creates a new instance of the aggregate type by using reflections
4. Reads events from the stream as a collection of ResolvedEvent objects
5. Deserializes those raw events to a collection of domain events
6. calls the Load method of the empty aggregate instance to recover the aggregate state

詳細請閱讀marketplace專案git log

## trouble shutting

EventStore版本差異:最新版無法使用無SSL，強制使用https，若為開發測試用可先下載使用5.0.9版本

## 相關參考資源

[a decade of DDD,CQRS,Event Sourcing](https://www.youtube.com/watch?v=LDW0QWie21s)

[Event Sourcing](https://www.youtube.com/watch?v=8JKjvY4etTY)

[versioning in an event sourced system](https://leanpub.com/esverioning)

[//begin]: # "Autogenerated link references for markdown compatibility"
[docker]: ../../../7-operate/learning/docker/docker.md "Docker"
[docker-compose]: ../../../7-operate/learning/docker-compose/docker-compose.md "docker-compose"
[eventstore]: ../../../2-code/learning/tool/eventstore/eventstore.md "eventstore"
[//end]: # "Autogenerated link references"
