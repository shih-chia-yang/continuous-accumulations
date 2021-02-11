# Syntax

## 原文說明

> `https://github.com/DavidAnson/markdownlint/blob/v0.22.0/doc/Rules.md#md035`

## 標題

> 使用`#` 來表示 html 中`<h1>`，而`#`的數量分別對應`h2`、`h3`等此類推。

    # 等價 h1
    ## 等價 h2
    ### 等價 h3
    #### 等價 h4 
    ##### 等價 h5
    ###### 等價 h6 
>呈現結果如下
---

## test header2

### test header3

#### test header 4

##### test header 5

###### test header 6

---

## 引言

> 使用`>`做為標籤，引言內也可以包含`>`或其他強調語法，如`**`

    > 測試
    > 測試1>測試1-1
    > 測試1> 使用**xunit**
>呈現結果如下
---
> 測試專案
>
> > 測試版本
> > >測試工具:**xunit**

## 分隔線

>使用`-` 連續3個來表示分隔線

    ---
>呈現結果如下
---

