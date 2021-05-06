# viewcomponent

將網頁某一區塊做成可重複使用的ui元件，例如排行榜、標籤雲端、登入面版、購物車、最新文章或促銷資料，讓view invoke

特性
- 轉譯區塊，而不是整個Response回應
- 設計上可維持關注點分離和可測試性優點
- 可傳遞參數資料給檢視元件的view
- 適合設計較為複雜的邏輯，甚至是允許對資料庫作存取
- 檢視元件不參與controller生命週期的過程，因此不能使用Filter，同時亦沒有model binding模型繫結。

過程
- 建立檢視元件類別
- 建立檢視元件之view檢視
- 將檢視元件註冊為Tag Helper
- 在view或Controller 叫用檢視元件


1. 建立檢視元件類別名稱結尾須加上ViewComponent，例如IscedList檢視元件名稱須為IscedListViewComponent
2. 須繼承ViewComponent類別
3. 若檢視元件需要資料庫存取，使用相依性注入ef core的DbContext
4. 檢視元件必須實作InvokeAsync非同步或Invoke同步方法
5. 檢視元件最終會呼叫一個檢視，用以顯示畫面，且檢視可以接受參數 

```aspx-csharp
    public class IscedListViewComponent:ViewComponent
    {
        public IscedListViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(string iscedId)
        {
            var viewmodel = new List<IscedViewModel>()
            {
                new IscedViewModel(){IscedId="0001",IscedName="綜合教育"},
                new IscedViewModel(){IscedId="0002",IscedName="成人教育"},
                new IscedViewModel(){IscedId="0003",IscedName="特殊教育"},
                new IscedViewModel(){IscedId="0001",IscedName="教育行政"},
                new IscedViewModel(){IscedId="0001",IscedName="課程與教學"},
                new IscedViewModel(){IscedId="0001",IscedName="教育科技"},
                new IscedViewModel(){IscedId="0001",IscedName="教育測驗評量"},
                new IscedViewModel(){IscedId="0001",IscedName="技職教育"}
            };

            return View(viewmodel);
        }
    }
```

於`/Views/Shared/Components/IscedList/`路徑下新增`Default.cshtml`

```razor
@using code.web.ViewComponents

@model IEnumerable<IscedViewModel>

@{
    ViewData["Title"]="IscedList";
}
<div class="row row-cols-2 row-cols-md-3 pt-5">
    @foreach (var item in Model)
    {
        <div class="col-md-4">
            <div class="card">
                <div class="card-header">
                    <h5 class="card-title">@item.IscedName</h5>
                </div>
                <div class="card-body">
                    @foreach (var subItem in item.DetailedList)
                    {
                        <p class="card-text">@subItem.IscedName</p>
                    }
                    <div class="d-flex justify-content-between align-items-center">
                        <div class ="btn-group">
                            <a asp-action="Detailed" class="btn btn-sm btn-outline-secondary">統計一覽</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    }
</div>
```

在view中以`Components.InvokeAsync()`叫用`IscedList` view component

```razor
@await Component.InvokeAsync("IscedList",5)
```

- 類別套用`[ViewComponent]`屬性，可不必ViewComponent結尾

```aspx-csharp
[ViewComponent(Name="HeroList")]
public class Heros:ViewComponents
{
    public IViewComponentResult Invoke(List<Card> data)
    {
    }
}
```

- 將檢視元件註冊為tag helper

在`_ViewImports.cshtml`中新增一行`@addTagHelper *, <專案組件名稱>`

名稱必頁全部改為小寫，且中間須用dash隔開(kebab case命名原則)
```razor
<vc:isced-list ised-id="5"></vc:isced-list>
```