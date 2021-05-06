# taghelper

- `<partial>` partial view

```aspx-csharp
<partial name="_PartialName"> //呼叫partial view
<partial name="_PartialName" view-data="ViewData"> //使用view-data屬性傳遞ViewData
<partial name="_PartialName" model="model"> //使用model屬性傳遞model物件
<partial name="_PartialName" for="model"> // 使用for屬性傳遞model物件
```

- `<img>`影像標籤

image tag helper用來替代<img>加上版本號碼，並具唯一性。若server靜態圖檔資源變更或修改，則會重新產生一組version number，強迫瀏覽器重新下載。

```razor
<img src="~/...." asp-append-version="true" >
```

- `<a>`

|屬性|說明|
|--|--|
|asp-action | 動作方法名稱|
|asp-controller | 控制器名稱|
|asp-area | 區域名稱|
|asp-page | razor頁面名稱|
|asp-page-handler | razor頁面處理常式名稱|
|asp-route | 路由名稱|
|asp-route-{value} | 單一路由值,ex: asp-route-id="1234"|
|asp-all-route-data | 所有路由值|
|asp-fragment | url片段|
|asp-protocol | url的通訊協定，ex:http/https|
|asp-host | host主機名稱|

- `<form>`

|屬性|說明|
|--|--|
| asp-controller| 控制器名稱 |
| asp-action | 動作名稱 |
| asp-area | 區域名稱 |
| asp-page | razor頁面名稱 |
|asp-page-handler | razor頁面處理常式名稱 |
|asp-route | 路由名稱 |
|asp-route-{value} | 單一路由值,ex: asp-route-id="1234" |
|asp-all-route-data | 所有路由值 |
|asp-fragment | url片段 |

- `<label>`

產生<label>元素與標題的for屬性

```razor
<label asp-for="Email"></label>
```

- `<input>`

產生`<input>`元素與id、name、type等屬性

```razor
<input asp-for="<Expression Name>" //指表達式名稱 />
```

Expression name=>`Model.<PropertyName>`,直接指定model中的屬性

```aspx-csharp
public class ViewModel
{
    public string Email {get;set;}
    public string Password {get;set;}
    public string ConfirmPassword {get;set;}
}
```

- `<select>` 

根據model 屬性來產生`<select>`及`<option>`。是`Html.DropDownListFor`與`Html.ListBoxFor`的替代

```razor
<select asp-for="Country" asp-items="Model.Countries"></select
>
```