# unittests

[[tdd]]

[[pytest]]

[[pester]]


1. 自動化，可被重複執行 
2. 容易被實現 
3. 第二天還有存在意義 
4. 任何人都可以執行它 
5. 執行速度很快 
6. 執行結果一致 
7. 要能完全掌控被測試的單元 
8. 它應該是完全被隔離的 
9. 執行結果失敗，能簡單清楚地呈現我們的基望為何，發生問題的原因在那

## 定義

被測試程式所測試的對象，稱為「被測試系統」(system under test, SUT) 

1. 一段程式，通常是一個方法 
2. 一個工作單元 
3. 一個使用案例 

被測試的工作單元的範圍界定? 
如果受測單元粒度過小，那麼最後可能需要偽造一塊相關物件，而這些物件不是使用公開API的真實最終結果，只是工作單元處理過程中的一些中間狀態。 會造成不容易維護單元測試，過度指定(overspecification)。 

## good practice

- 可讀性：應具備任何都能理解目的與預期執行結果 

- 可維護性：若單元測試不具備容易維護，則會造成工作負擔，等於開發人員需同時維護2份程式 

- 可靠性：在不改變需求規格的前提，單元測試的執行結果應永遠一致。 

## 命名和位置的基本規則

|測試物件|測試專案所建立的物件|
|--|--|
|專案|新增一個名為[ProjectUnderTest].UnitTest的測試專案 |
|類別|對應被測試專案的一個類別，新增一名為[ClassName]Tests的測試類別 |
|工作單元(一個方法，或幾個方法組成的一個邏輯模組，或是幾個類別) |對每個工作單元，新增一個如下命名的測試方法： [UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]。 如果整個工作單元就是一個方法，工作單命名就可以很簡單，就是這個方法名稱。如果工作單元是一個包含多個方法或類別的情況，工作單元名稱就比較抽象，如UserLogin、RemoverUser或Startup。你可以從方法名稱開始，之後逐漸過到比較抽象的工作單元名稱。如果使用方法稱，要確保這些方法是公開的，否則他們不能真正代表一個工作單元的起點。|

 

- UnitOfWorkName：被測試的方法、一組方法或一組類別。 

- Scenario：測試進行的假設條件，例如「登入失敗」、「無效的使用者」、「密碼正確」。你可以用測試情境描述傳給公開方法的參數，或是單元測試執行時，系統的初始狀態，例如：「系統記憶體不足」、「無此使用者」、「該使用者已經存在」 

- ExpectedBehavior：在測試情境指定的假設條件下，你對被測試方法行為的預期。 

受測方法的行為可能有三種，包含了回傳一個結果值(一個真實的值或是一個例外)、系統狀態的改變(例如在系統中新增了一個使用者，導致在下一次登入時系統的行為發生變化_、或是呼叫了外部第三方系統提供的服務(例如與一個外部的Web服務進行互動) 

## 測試行為

增加正向的測試：完全的測試案例涵蓋所有情況，以驗證方法是否能滿足預期功能需求 

1. 準備(Arrange)物件、建立物件、進行必要的設定 
2. 操作(Act)物件 
3. 驗證(Assert)某件事符合預期 

## 參數化測試

- Nunit

使用`[TestCase]`將測試中所寫死的值換成這個測試方法的參數 
把被替換的值放到TestCase括號中，如`[TestCase(param1,param2....)] `

```aspx-csharp
[TestCase("Heipuser","1234567")] 
Public void Login_ValidUserIsExist_ReturnTrue(string account,string password，bool isAuthiced) 
{ 

    UserService user = new UserService(); 

    Bool Result=user.Login(account,password); 

    Assert.equql(result,isAuthiced); 

} 
```

MsTest 

使用`[DataRow(param1,param2….)] `
參數為物件，集合，陣列; 方法時 [DynamicData(nameof(PropertyName or MethodName),DynamicDataSourceType)] 
DynamicDataSourceType.Property 
DynamicDataSourceType.Method 

```aspx-csharp
Public Static Ienumerable<object[]> GetListData 
{ 
    Get
    { 
        Return new[] 
        { 
            New object[]{ new List<string>{"02-77265882","05-4532486","04-4498513"}}， 
            New Object[]{new List<string>{}}， 
        } 
    }
} 

[TestMethod] 
[DynamicData(nameof(GetListData),DynamicDataSourceType.Property)] 
Public void ValidPhoneFormat_MatchWithTelecomEncoded_ReturnTrue(List<string> phoneList) 
{ 

} 
```

經上例實作經驗：因忽略書中範例程式的小細節，使用參數化測試後同一個被測試物件或方法，因需要正向、反向測試，導致同一方法會需3-4個測試方法，隨著被測物件増加，測試方法也幾何增加，造成維護困難，若寫在同一個測試方法，內容則會産生if之類的判斷式，增加測試方法的複雜度與不容易閱讀。 

初次實驗specification by example後，產生初版文件，再複習單元測試時，方了解書中的實際案例的目的。應為測試方法提供案例與預期結果，參數陣列應為:`[測試參數，預期值］`，預期值應於參數增加，不應在程式中有判斷式或多個測試方法，降低單元測試的複雜與增加維護性 

以下是使用Xunit Fix by SBE 

> [Theory(["Heipuser","1234567"],true)] 

## 練習心得

個人目前練習心得： 

可先針對會影響Public API最終真實呈現結果的核心工作單元，進行單元測試 

- 以Domain Driven Design模式 
- 輸入數據---->欄位驗證---->Domain Model---->Repository----->DataBase 
- 先針對Domain Model進行測試，再擴展至Application應用層 

即使已看過本書，但在實作過程中，才逐漸了解單元測試定義與容易維護。對於測試工作單元範圍界定，對於我來說，需多次經驗才能體會書中說提到的要點，目前已是試驗第三次，在每次試驗過再回頭看本書時，都有更深理解。建議閱讀本書的過程也需搭配真實專案環境的小部份實驗，並再次檢討應如何撰寫單元測試。

- 第一次經驗：釐清單元測試與整合測試的概念混淆 

- 第二次經驗：單元測試粒度過小過度指定，造成維護困難 

- (進行中)第三次經驗：從閱讀Specification by example到協作規範時，了解使用案例與實例對於單元測試的重要性 


在撰寫單元測試中遇到的困難在於思維的轉變與建構程式的流程改變，從實作程式完後再來撰寫單元測試，會給我一種感覺，究竟是在測試受測程式，還是測試單元測試。若從使用案例分析了解，並制定實例，藉由協作過程中不斷了解案例，從而先撰寫單元測試，並同時產生實例與預期結果，再進行實作商務邏輯程式，進行測試 

1. 若是修正程式邏輯，由失敗變化為成功，證明單元測試與程式按預期執行 

2. 若是修正單元測試，而從失敗變化為成功，是在測試單元測試本身，造成此問題目前遇到以下情況。 
    1. 可能性為不熟悉單元測試API，造成失敗。 
    2. 單元測試撰寫邏輯有問題，可能過於複雜，屬於失敗的單元測試。 
    3. 使用案例與實例不夠精確，有模糊地帶，需再次確認 

 

目前實驗Specification by example 與 TDD有相輔相成的效果，互相驗證 
Specification by example著重於 使用案例與實例的分析與協作 
TDD 著重於 將上述轉變為可執行規範 

## 虛設常式

- 外部依賴(external dependency)：是指在系統中的一個物綿，它與被測試系統之間產生互動，但你無法掌控這個物件。 

- 虛設常式(stub)：是在系統中產生一個可控的替代物件，來取代一個外部相依常件(或協作者)。你可以在測試程試中，透過虛設常式來避免必須直接相依物件所造成的問題。 

1. 找到被測試物件的外部相依(方法、物件；遵循職責單一原則) 
2. 如何讓測試變簡單 
    - 任何物件導向的問題，都可以透過增加一層中介(a layer of indirection)來解決；當然除了中介層過多的這個問題(作者摘錄自：https://en.wikipedia.org/wiki/abstraction_layer。 單元測試的藝術精華就在於如何找到一個合適的地方加入或使用一個中介層來測試你的程式碼。 有程式碼無法測試，就加入一個中介層來封裝對這段程式碼的呼叫行為，接著就可以在測試中模擬這個中介層的實作內容；或是讓這段無法測試的程式碼變得可抽換。 這藝術還包含了當中間層已經存在時要能夠發現它，而不是重新創造一個新的中間層取代。 
    - 找到導致被測試工作單元無法順利測試的介面 
    - 若被測試工作單元是直接相依這個介面，可以透過在代碼中加入一個中介層，來取代這個直接相依的行為，這樣程式碼就具備可測試性。 
    - 將這個相依介面的底層實作內容替換成你可以控制的程式碼 

- 測試模式名稱： Gerard Meszaros ：xUnit Test Patterns：Refactoring Test Code

> 是一本關於單元測試模式的經典參考書。這本書至少定義了五種在測試中進行模擬的模式。 
> 作者認為模式太多容易讓人困惑(該書對各種模式介紹地非常詳細)，在本書中，作者只使用了在測試中進行模擬物件的三種定義：假物件、虛設常式以及模擬物件。 簡化術語可以使讀者更容易理解這些模式，而且瞭解這三種模式就可以寫出品質良好的測試程式了。

## 重構
 
重構是在不改變程式碼功能的前提下，修改程式碼的動作，也就是說，程式碼在修改前後的工作是一致的，不多也不少，只是程式碼看起來跟原本不一樣了。 常見的重構例子例如重新命名一個方法，或是把一個較長的方法內容拆成幾個較短的方法。 

在進行重構之前，倘若沒在任何一種自動測試(整合測試或其他種類自動測試)保護下，就貿然開始重構程式碼，稍不留神可能就會導致你的職業生涯提早結束。 在開始修改現有程式碼之前，永遠記得要準備好整合測試作保護，或者至少要有一個能全身而退的計畫----在重構之前先備份你的程式，最好放在版本控制系統裡面，並加上一個醒目註解 

## 接縫(sean)

- Seam：是指在程式碼中可以抽換不同功能的地方，這些功能例如：使用虛設常式類別、增加一個建構函式(constructor)參數、增加一個可設定的公開屬性、把一個方法改成可供覆寫的虛擬方法，或是把一個委派拉出來變成一個參數或屬性供類別外部來決定內容。 

- Seam透過實作開放封閉原則(open-closed principle)來完成，類別的功能放擴充的彈性，但不允許直接修改功能內實作的原始程式碼(類別功能對擴充開放、對直接修改封閉)。 

- 遵循開放封閉原則，設計出的程式碼就會有Seam。 

重構： 

1. A型：將具象類別(concrete class)抽象或介面(interface)或委派(delegates) 

2. B型：重構程式碼，以便將委派或介面的偽實作注入至目標物件中 

### 擷取介面以便替換底層實作內容

1. 擷取出讀取檔案系統的類別，並呼別它的方法 

```aspx-csharp
Class FileExtensionManger 
{ 
    Public bool IsValid(string fileName) 
    { 
        …… 
    } 
} 
```

2. 從一個已知的類別擷取出介面 
Public class FileExtensionManager:IExtensionManger 
{ 
    …. 
} 

Public interface IExtensionManger 
{ 
    Bool IsValid (string fileName); 
} 

3. 一個總是回傳true的簡單虛設常式的程式碼 

```aspx-csharp
Public class AlwausValidFakeExtensionManger:IExtensionManger 
{ 
    Public bool IsValid(string fileName) 
    { 
        …… 
    } 
} 
```

- 留意類別相當特殊的命名，這相當重要。 這個類別不叫StubExtensionManger或MockExtensionManger，而是FakeExtensionManger。 在類別名稱中使用fake這個字眼，說明這個類別物件類似另一個物件，但它可能被當作模擬物件(mock)或虛設常式(stub)使用。 

- 透過這種命名方式來描述物件或變數是一個假物件，你就可以延遲決定這個用來當作替代品的物件究竟是模擬物件或是虛設常式，當人們看到「stub」或「mock」的字眼時，就會期待這個物件具有種特定的行為。如果在類別中避免直接使用「mock」和「stub」的命名，那麼這個物件就兼具兩種行為模式，以便在不同的測試中重複使用。 

- 這個假物件的實作總是回傳true，所以被命名為AlwaysValidFakeExtensionManger，這樣閱讀測試程式的人就不需要讀類別內的完整內容，光看類別名稱就能理解假物件的行為。 

### 在被測試類別中注入一個虛設常式的實作內容

- 在建構函式中得到一個介面的物件，前將其存到欄位(filed)中後供後續使用。 

- 在屬性的get或set方法中得到一個介面的物件，並將其存到欄位中供後續使用。 

- 透過下列其中一種方式，在被測試方法呼叫之前獲得一個介面的假物件： 
    - 方法的參數(參數注入) 
    - 工廠類別 
    - 區域工廠方法(local factory method) 
### 在建構函式注入一個假物件。 

使用這種方式依賴注入時，需在被測試物件增加一個新的建構函式(或是在已存在的構造函式新增一個新的參數)。 

```aspx-csharp
Public class ReportService 
{ 

    Private Irepository _repository; 

    Public ReportService(irepository repository) 
    { 
        _repository=repository; 
    } 

    Public Ilist<viewmodel> Find(string key) 
    { 
        List<viewmodel> dataList = _repository.where(x=>x.key==key); 
        Return dataList; 
    } 

} 

    

Public interface Irepository 
{ 
    Ilist<viewmodel> Where(string key); 
} 

    

Public class FakeRepository:irepository 
{ 
    Public Ilist<viewmodel> Where(string key) 
    { 
        Return new List<viewmodel> 
        { 
            New viewmodel(){}, 
            New viewmodel(){}, 
        }; 
    } 
} 
```

注意：透過建構函式中使用參數注入，實際上使得這些參數成為必要的依賴(假設這是被測試類別唯一的建構函式) 

1. 關於建構函式注入的警告 

- 透過建構函式注入假物件，可能會衍生某些問題，如果被測試類別需要更多個虛設常式才能在沒有直接依賴關係下正常運作，加入越來越多的建構函式就會越來越困難，甚至還會降低程式的可讀性和可維護性。 

2. 何時該使用建構函式注入 

- 除非使用ioc框架來協助初始化物件，否則使用建構函式參數來進行初始化物件時，會讓測試程式看起來更笨拙。但作者通常還是會選擇使用建構函式注入，因為在API的可讀性跟語意上，這方式於來的影響是最小的。 

- 此外這個方式能有效向API使用者表達：在呼叫這個API時，是必須相依這個物件才能正常運作的，因此需要在初始化時傳入該相依物件。 

- 如果你期望這些相依物件是非必要性的(optional)，可以透過獲取方法(getter)與賦值方法(setter)的屬性，來更彈性地注入相依物件，比起其他方法，例如為每個相依物件增加一個不同的建構函式，屬性注入的方式顯得靈活許多。 

### 從屬性的讀取或設定中注入一個假物件。 

這個方式是為一個相依的物件建立一個對應的get與set屬性，然後在測試過程中使用到這些相依物件。 

程式碼會與建構函式注入很類似，但更好讀，也更好撰寫。 

```aspx-csharp
 
Public class ReportService 
{ 
    Private Irepository _repository; 

    Public Irepository repository 
    { 
        get{return _repository;} 
        set{ _repository=value;} 
    } 

    Public ReportService() 
    { 

    }
 
    Public Ilist<viewmodel> Find(string key) 
    { 
        List<viewmodel> dataList = _repository.where(x=>x.key==key); 
        Return dataList; 
    } 
} 
```

1. 何時應該使用屬性注入 
如果你想表達出對被測試類別來說，這個相依物件並非是必要的，或是在測試過程中這個相依物件會被建立預設的物件執行個體，進而避免造成測試的問題，就可以使用屬性注入的方式。 
### 在方法被呼叫前注入一個假物件。 
 

[//begin]: # "Autogenerated link references for markdown compatibility"
[tdd]: ../../../2-code/learning/development/tdd/tdd.md "Tdd"
[pytest]: ../../../2-code/learning/language/python/pytest.md "pytest"
[pester]: ../../../2-code/learning/language/Powershell/testing/pester.md "Pester"
[//end]: # "Autogenerated link references"