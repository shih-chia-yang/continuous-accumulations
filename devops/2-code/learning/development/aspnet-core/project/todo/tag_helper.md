# tag_helper

AnchorTagHelper協助產生 html tag上的formaction屬性，formaction屬性可讓您控制表單提交其資料的位置。
AnchorTagHelper允許使用多個`asp-`屬性來控制相元素產生那個formaction連結

- AnchorTagHelper屬性控制formaction

|屬性|描述|
|--|--|
|asp-controller|控制器的名稱|
|asp-action|動作方法的名稱|
|asp-area|區域的名稱|
|asp-page|頁面的名稱Razor|
|asp-page-handler|Razor頁面處理常式的名稱|
|asp-route|路由的名稱|
|asp-route-{value}|單一URL路由值。例如：asp-route-id="1234"|
|asp-all-route-data|所有路由值|
|asp-fragment|URL片段|


[相關參考](https://docs.microsoft.com/zh-tw/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-5.0#the-form-action-tag-helper)