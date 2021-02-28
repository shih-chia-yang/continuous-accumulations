# Xrandr

```bash
xrandr --help
```

```bash
xrandr -q |grep -w connected
```

```bash
xrandr --addmode <size>
```

```bash
xrandr --output <connected device> --mode <size>
```
問題：查詢device提供的解晰度後，設定1920*1080，不斷出現can't find `1920*1080`，經過試驗後發現 該欄位為mode name，不是運算式，設定mode要與device提供的相同，而設備為1920x1080，使用復製後即正常設定


[links-1](https://www.itread01.com/p/184275.html)

[links-2](https://gist.github.com/geek/3531111)