# REST

Representational State Transfer (REST)

- 每一個URL都可視為一個可識別的資源 , like https://localhost:5001/api/products/23
- 獲取資源通常使用http method, like `GET`,`POST`,`PUT`,`DELETE`。 [http-methods](https://developer.mozilla.org/en-us/docs/web/http/methods/options)


- `[HTTPGet]`、`[HTTPHead]` : 回應HTTP GET或HEAD請求以檢索資源並返回資源及response headers或僅headers
- `[HTTPPost]` : 回應HTTP POST request並建立一個新的資源
- `[HTTPPut]`、`[HTTPPatch]`:  回應HTTP PUT/PATCH request，並更新或替代現有resource，或是更新/替代resource 中的某些屬性
- `[HTTPDelete]` :  回應HTTP DELETE request並移除資源
- `[HTTPOptions]` : 回應HTTP OPTIONS request

- REST is Representational state Transfer and it is a network of web pages where the client progresses through an application by selecting links.
- REST is an architectural style that uses existing standards such as HTTP.
- REST is light wait uses HTTP GET,PUT,POST,DELETE,etc.
- REST is solely exist on existing HTTP standards, it is extremely interoperable across all platforms and capable of making HTTP request.
- as long as a device can make an HTTP request to a URL,it can do REST

## 相關資源

[media-type](http://en.wikipedia.org/wiki/Media_type)