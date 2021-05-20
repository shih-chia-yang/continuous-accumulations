# coding_record

 - [x] 新增ICurrencyExpression介面，使money物件繼承
    - [x] 新增ExchangeService，使用ICurrencyExpression來處理money物件
    - [x] 新增IOperationExpression，定義計算與換算方法的介面，提供ExchangeService繼承
    - [x] IOperationExpression 新增ICurrencyExpression Sum(params ICurrencyExpression[] added);提供實作加法
        - [x] 測試案例新增test_sum_of_money_gives_full_amount()、test_sum_of_different_currency_should_be_throw_exception()測試加法
    - [x] IOperationExpression 新增ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend);提供實作減法
        - [x] 測試案例新增 test_subtraction_of_money_gives_correct_amount() 測試減法
    - [x] IOperationExpression 新增 ICurrencyExpression ExchangeTo(string to);提供實作匯率轉換
        - [x] 測試案例新增test_currency_exchange_to_another_currency()
    - [x] 新增匯率轉換概念，實值幣別轉換，不是單純修改currency
        - [x] test_currency_exchange_to_another_currency增加貨幣數值比對
        - [x] 新增Pair物件提供幣別轉換物件 
        - [x] ExchangeService 新增HashTable建立匯率轉換表
        - [x] IExchangeService新增 AddRate(Pair pair, decimal rate)，提供建立各種貨幣匯率轉換清單。
        - [x] IExchangeService新增GetRate(Pair targetPair)，可取得指定貨幣轉換匯率
    - [x] 計算方法加入匯率轉換概念，將所有貨幣轉換同一種再進行計算
        - [x] 實作不同幣別進行加總計算
        - [x] 實作不同幣別進行減法計算
        - [x] 實作乘法計算
    - [x] 新增Currency domain primitive物件，將Money物件中的string currency換為Currency型別，
    - [x] 新增CurrencyCollection類別，提供各種幣別，小數點進位清單
        - [x] 提供AddCurrency方法=>rename to Add
        - [x] 提供FindCurrency方法=>rename to Find




