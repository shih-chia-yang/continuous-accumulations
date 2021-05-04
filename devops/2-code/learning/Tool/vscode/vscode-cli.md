# vscode-cli

在命令列中輸入`code --help`，可以快速連接到vs code的命令列介面，透過說明，可以詳細了解每個指令的介紹

|argument|example|description|
|--|--|--|
| -h/--help| code -h | 輸出說明資訊|
| -v/--version | code -v |輸出visual studio code的版本資訊|
| -n/--new-window| code -n | 開啟一個新的vs code視窗|
| -r/ --reuse-window| code -r|在已有的vs code視窗中開啟檔案或資料夾|
| -g/--goto | code --goto package.json:10:5 | 在此參數加上file:line[:character]參數，開啟檔案時可以定位到對應位置|
| -d/--diff | code --diff <file1> <fil2> | 進行檔案比較，參數後面加上2個檔案路徑 |
| -w /-wait | code -wait <file> | 等待檔案被關閉|
| --local <local> | code . --local zh-TW |設定顯示的語言|