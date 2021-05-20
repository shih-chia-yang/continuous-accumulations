# classifiedad_publish_spec

發佈classifiedAd，title與text不可為null，且price必須大於0

this spec contains several tests for on operation(`publish`) with
different pre-conditions.

除了測試happy path之外，增加針對publish的反向測試，檢驗是否有遺漏的部份
案例應設計使用edge case，仍可成功完成publish，避免entity成為一個非法的物件。

## 測試案例

- test_publish_a_valid_ad_should_be_success
- test_when_publish_without_title_should_throw_exception
- test_publish_without_text_should_throw_exception
- test_publish_without_price_should_throw_exception
- test_publish_with_zero_price_should_throw_exception

