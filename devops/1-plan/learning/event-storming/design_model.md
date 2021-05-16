# design_model

[[eventstorming]]

## what dose the model represent

models represent real-life objects on a different scale and also demonstrate a substantially different level of detail.


## domain model 
scale of model
an object model of the domain that incorporates both behavior and data

the **state** is the data that describes what our system look like at a particular moment in time.
every behavior of the model changes the state. 
the state is that thing we persist to the database and that we can recover at any time before applying a new behavior.

- ex:以下的例子簡單說明domain behavior發生時使狀態改變，稱為state transition.

account balance $500 =>deposit in ATM $200 => account debited for $200 => account balance $700

基本上，communication protocols,user input validation and persistence implementation不是domain model,這些屬於基礎架構。
一個好的domain model在測試時是不需要任何基礎架構的，且不需要使用mock與test harnesses(先簡單理解類似整合測試)

[test harnesses](https://www.softwaretestinghelp.com/what-is-test-harness/)

[wiki](https://en.wikipedia.org/wiki/Test_harness)

## Anemic domain model 

models only express the system state and know nothing of how this state changes and what operations are done in the system .there is no place for behavior in conceptual classes.

why anemic models became popular:

1. Visualize domain models in UML suggest that things that are envisioned are conceptual classes.
2. UML models became the only domain models, where domain behavior was considered insignificant.

anemic models傾向完全透過資料庫實作來實現系統功能，不同的model關聯組成一個類似relational models，這是資料庫最常使用的持久化對象，使得anemic domain model與資料庫關聯緊密結合，以致於無法區分彼此。如果你在一個將anemic model做為domain model的系統，你無法單獨透過程式碼去了解整個系統，在還未了解系統中的資料庫sql語法、sp、trigger並且執行它之前，

## functional languages

the key here is in the combination of data and behavior, and definitely ,when using a rich type system combined with clearly defined functions that express intent,such a model is indeed not anemic.

## design considerations

如果注意onion architecture(洋蔥架構), the hexagonal architecture(六角架構),and clean architecture(簡潔架構)的設計原則時
，會發現一個共通點所有的服務中心點都是domain。所以有application services與基礎設施、ui都保持在外圍，圍繞著系統形成各層，
而domain是所有東西的中心點，所有東西都向內依賴。

if look at the onion architecture, the hexagonal architecture, and clean architecture principles, you will find that they have one thing in common.

The center of any application is the **domain**

Application services and infrastructure are kept outside and form layer around this one of the system. 

domain is the center of everything , and everything depends  on it.
## CQRS

the term originated from command-query separation(CQRS), formulated by bertrand meyer,which states that object methods are separated into two categories. these categories are as follows:
    
- Commands ,which mutate the system(most often the object) state and return void

- Queries, which return part of the system state and do not change the state of the system. this makes queries side effect free(expect things such as logging) and idempotent so that they can be executed many times and get the same result.

command-query responsibility segregation(CQRS) takes this principle outside of an object.

separating commands and queries on the system level means that any state transition for the system can be expressed by a command, and such a command should be handled efficiently , optimized to perform the state transition.

queries,on the other hand, return data derived from the system state,which means that queries can be executed differentlyand can be optimized for reading the state or any derivative of the state if such a derivative exists.

## getting deeper knowledge

- what processes does the business run ?
- what kind of objects participate in these processes ?
- what facts can we record about the system behavior ?
- who does what ?
- what essential terms do we need to learn and use ?

discussions about these points produced a diagram with a lot of orange(also have some pink) sticky notes 
representing facts of life, which we call **domain events**.

##  preparation for the workshop **Design-level eventstorming**

- paper roll or any other type of unlimited modeling space
- sticky notes of different colors,we'll get to the notation later on
- enough permanent markers


it is essential to choose to explore,and finding such a space is often a non-trivial task

limiting the scope and limiting the number of people, allow us to discuss the design in a much higher level of detail

- 注意事項

因為event storming與工具語言、技術無關，所以我們無法實作出類別、欄位、方法、函式等，我們需要更通用的設計方法，如同上方所提到的CQRS，透過design pattern我們可以將domain model中的行為表達為`執行命令`，而不是一堆方法清單。

1. command用來表達使用者的意圖，domain model會取得狀態轉換，產生新的event，記錄這個行為與狀態變換

1. queries用來表達使用者想要看到什麼，來決定下一步或執行其他命令。

## commands

使用**藍色**來代表。
透過design-level eventstorming來標示command, 表達使用者與系統之間的互動

ex: the commands triggers the state change

activate classified AD       classified Ad Activated

以上流程表達當我們要求系統當operation被執行時做某些事，系統變換狀態並且產生一個新的event。
避免在modeling space使用箭頭，因為當初創造這些sticky notes，已經明確標示了時間線，以此降低modeling dynamics 和experimentation

## read models

使用**綠色**來代表。

the read model is something that our users look at before asking the system to do something.
informational elements and form elements are something that our users look at before deciding what to do.

- the information shown as text and images
- form elements such as input boxes,check boxes ,and radio buttons
- action buttons
- navigation



example:

綠色便利貼呈現classified ad的read model，使用者透過read model可以做特殊的動作，如publish or remove. 
執行命令後，domain model將會發佈一個事件。

commands: 
1. `Activate Classified Ad`,`Classified Ad Activated`
2. `Remove CLassified Ad`,`Classified Ad Removed`

Classified Ad:
- Title
- Text
- Price
- Image

## User


when designing the model, we often need to understand who is running which command, just because not all commands are allowed to be executed by everyone.

識別出具有執行命令的使用者或角色

- administrator

- manager

- reviewer

使用便利貼畫上人型圖案並標示角色，貼在他具備的職責或權限的commands旁

## policies

when we publish events , we also let other elements of our domain model, that were previously unaware of the command being executed,know that something happened.this is very useful in order to not execute all work linked to a certain at once.

policies subscribe to domain events, and when a policy receives some domain events it is interested in, it will check the event content and potentially send another command to the system to complement the work.

應該將處理命令的工作量最小化。有些操作需要在交易機制內完成，但有些則不需要一定要在狀態轉換後立即完成，而這些操作不應強迫使用者等待所有工作完成。

there might be numerous policies reacting to the same event type,doing all kinds of post-processing in an asynchronous fashion,while the user gets control back after the original command has been executed.


ex:當owner將classified ad標記為sold時，system 應該停用這個classified ad

Mark Classified Ad as Sold, `Classified Ad Marked as Sold`

`Deactivate when Marked as Sold` Deactivate Classified Ad

a policy can react to domain events and issue commands,based on certain conditions. such behavior is classed `reactive behavior`,and systems that actively use this pattern can be referred to as `reactive system`

## all together now

![eventstorming-color](/assets/images/eventstorming/eventstorming-color.png)



|-|-|-|-|-|-|
|--|--|--|--|--|--|
|  |  | <= |<span style="color:purple">policy</span>| <=|  |
|  |  | v |      |  ^|  |
|  | => |<span style="color:blue">Command</span>| =>|<span style="color:orange">domain event</span>|=> |
|  | ^ |        |   |v |v|
|=>  |<span style="color:yellow">user(actor)</span>| |    | v|<span style="color:pink">external system</span>|
|  | v| ||v| |
|  | =>|<span style="color:green">read model</span>|<=|<=| |

使用者從系統得到一個以read model方式呈現的訊息，並根據他從外界得到資訊，向系統送出操作要求(`commands`)，其執行結果造成狀態改變從而產生一個`domain events`，
而domain events間接觸發`policies`，從而可能基於`event`與`state change`的資訊又產生新的`commands`。而external system也會產生`domain events`。
而系統狀態改變的執行結果藉由`read model`回饋給user，使用者則在這個循環中不斷的接收訊息與送出要求。

*separation of concerns(SOC)*

## 相關閱讀

[introducing eventstorming](https://leanpub.com/introducing_eventstorming)

[microsoft inductive user interface guidelines](https://msdn.microsoft.com/en-us/library/ms997506.aspx)

[task-based ui](https://cqrs.wordpress.com/documents/task-based-ui/)

[//begin]: # "Autogenerated link references for markdown compatibility"
[eventstorming]: eventstorming.md "eventstorming"
[//end]: # "Autogenerated link references"
