# tdd

[[test-automation]]

[[unittest]]

## 測試驅動開發

- 實施得當，TDD可以極大化提昇程式的品質、減少BUG數量、提昇你對程式的信心、縮短發現BUG的時間、優化程式設計 

- 若實施不當，可能導致專案時程延宕、注費時間、打擊士氣、甚至降低程式的品質 

## 開發過程

1. 撰寫一個會失敗的測試，證明產品中程式或功能的缺失 
2. 撰寫符合測試預期的產品程式碼，以通過測試 
3. 重構程式碼 

撰寫測試=>執行所有測試 =>撰寫production code通過測試=>執行測試=>若無重構，撰寫下一個測試

重構production code =>執行測試 =>失敗，修正問題，直到測試通過

[//begin]: # "Autogenerated link references for markdown compatibility"
[test-automation]: test-automation.md "test-automation"
[//end]: # "Autogenerated link references"