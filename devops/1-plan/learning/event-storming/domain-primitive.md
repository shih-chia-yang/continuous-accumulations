# domain-primitive

## introduce

Domain Primitive的概念和命名來自於Dan Bergh Johnsson & Daniel Deogun的書 Secure by Design.

在Evans的 DDD藍皮書中,Value Object代表著Entity的值物件
在Vernon的IDDD紅皮書中,作者更多的關注在Value Object的Immutability、Equals方法、Factory方法等。

Domain Primitive是Value Object的進階版,在原始VO的基礎上要求每個DP擁有概念的整體,而不僅僅是值物件.在VO的Immutable基礎上增加了Validity和行為.同樣要求無副作用（side-effect free）.簡單的說Domain Primitive是一個在特定領域中擁有精準定義，可自我驗證，擁有行為的value object

## principal

- 將業務基本知識概念顯性，而不是隱藏在檢查邏輯中
- 封裝多個物件行為

## how to use

- 有格式限制的string, 如name,phoneNumber,OrderNumber,ZipCode,Address
- 有限制的integer,加OrderId,Percentage,Quantity
- 可列舉的status(因有序例化問題，一般不使用enum)
- double或Decimal，含有業務意義的數字，如Temperature,Money,Amount,ExchangeRate,Rating
- 複雜數據結構，如map或樹，盡量把複雜操作包裝不暴露非必要行為

## what is different with DTO(data transfer object)

||DTO|DP|
|--|--|--|
|功能|數據傳輸|業務領域中的概念|
|關聯|只是command所需要用的數據，不一定有關聯|牽涉entity所使用的資料|
|行為|無行為|無副作用的簡單行為與自我驗證的邏輯|

## 相關資源

- [domain primitive](https://alibaba-cloud.medium.com/an-alibaba-cloud-technical-experts-insight-into-domain-driven-design-domain-primitive-c569986cebcd)

- [secure-by-design](https://livebook.manning.com/book/secure-by-design/chapter-5/1)
