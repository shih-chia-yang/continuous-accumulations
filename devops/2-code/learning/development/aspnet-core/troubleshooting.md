# troubleshooting

## Failed to bind to address http://127.0.0.1:port: address already in use.

偵錯時出現part已被佔用，可能為前次偵錯未正常關閉導致仍在背景執行中
1.使用`lsof`指令列出被執行的檔案
2.使用`grep`找出為`dotnet`使用的執行緒
3.使用`awk`列出第二個PID欄位
4.使用`Kill`命令刪除執行緒

````bash
sudo kill -9 $(lsof -i TCP -s TCP:LISTEN | grep 'dotnet' |awk '{print $2}')
````

- 參考連結
  - [address-already-in-use](https://lifesaver.codes/answer/port-localhost-5000-not-released-causing-error-system-io-ioexception-failed-to-bind-to-address-http-127-0-0-1-5000-address-already-in-use-948)
  - [lsof](https://dotblogs.com.tw/ghoseliang/2015/01/21/148217)
  - [grep](https://ubuntuqa.com/zh-tw/article/9133.html)
  - [grep-regex](https://www.digitalocean.com/community/tutorials/using-grep-regular-expressions-to-search-for-text-patterns-in-linux)
  - [grep -P](https://blog.longwin.com.tw/2018/11/linux-grep-regex-match-keyword-2018/)
  - [kill process](https://www.cyberciti.biz/faq/kill-process-in-linux-or-terminate-a-process-in-unix-or-linux-systems/)