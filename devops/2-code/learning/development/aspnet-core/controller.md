# controller

負責接收http request ，協調model與view，並將最終輸出回應給使用者

Convention:

- 位於Controllers資料夾中
- 繼承Microsoft.AspNetCore.Mvc.Controller

controller初始化，符合以下其中一個要件
- Controller類別名稱須以Controller結尾
- Controller繼承的類別是以Controller結尾
- 類別以`[Controller]`屬性裝飾


|屬性|說明|
|--|--|
|ControllerContext|取得或設定控制器內容|
|HttpContext|取得關於個別HTTP要求的HTTP特定資訊|
|MetadataProvider|取得或設定IModelMetadataProvider|
|ModelBinderFactory| 取得或設定IModelBinderFactory|
|ModelState|取得模型狀態字典物件，這個物件包含模型和模型繫結驗證的狀態|
|ObjectValidator|取得或設定IObjectModelValidator|
|ProblemDetailsFactory|產生ProblemDetails或ValidationProblemDetails之Factory|
|Request|取得目前HTTP要求的HttpRequestBase物件 |
|Response|取得目前HTTP回應的HttpResponseBase物件 |
|RouteDate|取得目前要求的路由資料 |
|TempData|取得或設定暫存資料的字典 |
|TempDataProvider|取得暫存資料提供者物件，這個物件用於儲存下一個要求的資料 |
|Url|取得URL Helper物件，使用路由來產生URL |
|ViewBag|取得動態檢視資料字典 |
|ViewData|取得或設定檢視資料的字典 |

## ViewComponent

## View

## ValidationProblem

## UnProcessableEntity

## UnAuthorized

## TryValidateModel

## TryUpdateModelAsync

## StatusCode

## SignOut

## SignIn

## RedirectToRoutePreserve

## RedirectToRoutePermanent

## RedirectToRoute

## RedirectToPagePreserve

## RedirectToPagePermanent

## RedirectToPage

## RedirectToActionPreserve

## RedirectToActionPermanent

## RedirectToAction

## RedirectPreserveMethod

## RedirectPermanentPreserve

## RedirectPermanent

## Redirect

## Accepted

## AcceptedAtAction

## AcceptedAtRoute

## BadRequest

## Challenge

## Conflict

## Content

## CreatedAtAction

## CreatedAtRoute

## Dispose

## File

## Forbid

## Json

## LocalRedirect

## LocalRedirectPreserve

## NoContent

## NoFound

## Ok

## OnActionExecuted

## OnActionExecuting

## OnActionExecutionAsync

## PartialView

## PhysicalFile

## Problem