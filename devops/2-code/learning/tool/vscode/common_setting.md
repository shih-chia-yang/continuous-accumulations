# common_setting

- 控制編輯器自動格式化貼上的內容
```json
"editor.formatOnPaste":true
```

- 儲存檔案後進行程式格式化
```json
"editor.formatOnSave":true
```

- 改變字型大小
```json
{
    "editor.fontSize":18,
    "terminal.integrated.fontSize":14,
    "[Log]":{
        "editor.fontSize":15
    }
}
```

- 調整視窗縮放等級
```json
"window.zoomLevel":5
```

- 設定連體字，需確保設定的字型是支援連體字。ex:fira code 是visual studio code團隊常用的字型之一
```json
"editor.fontFamily":"Fira Code",
"editor.fontLigatures":true
```
- 設定自動儲存模式
```json
"files.autoSave":"afterDelay",
"files.autoSaveDelay":1000
```

- 設定定位字元(Tab)等於的空格數
```json
"editor.tabSize":4
```
- 設定按Tab鍵時插入的空格還是定位字元(Tab)
```json
"editor.insertSpace":true
```

- 控制編輯器在空白字元上顯示等號的方式
```json
"editor.renderWhitespace":"all"
```

- 投定排除的檔案和八料夾的glob模式。檔案資源管理員將根據些設定決定要顯示或隱藏的檔案或資料夾
```json
"files.exclude":{
    "somefolder/":true,
    "somefile/":true
}
```

- 設定在搜尋中排除的檔案和資料夾的glob模式
```json
"search.exclude":{
    "somefolder/":true,
    "somefile/":true
}
```


