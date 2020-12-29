# Route-to-Json

asp.net core支援多種方法來建立json web api：
- asp.net core web api提供完整的架來建立api。藉繼承來建立服務ControllerBase。架構所提供的部份功能，包括model binding、model validation、content negotiation、openapi、input and output formatting。
- route-to-code 是asp.net core web api的非架構替代方案。
    route-to-code將asp.net core route直接連接到你的程式碼。
    你的程式碼會從要求中讀取並寫入回應。
    route-to-code沒有web api的advanced功能, 你不需要進行任何設定，就能使用。


