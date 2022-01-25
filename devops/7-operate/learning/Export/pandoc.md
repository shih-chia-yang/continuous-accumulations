# Pandoc


## trouble shotting

- linux 系統 md 轉 pdf檔，出現select --pdf-engine
```bash
sudo pacman -S texlive-most
```

- md檔案含有中文，無法正常轉換
```bash
sudo pacman -S texlive-lang
```
尋找支援中文的字型

```bash
fc-list :lang=zh-tw
```

- 輸出pdf, 格式調整
```bash
* [ ] pandoc ~/project/continuous-accumulations/devops/2-code/learning/tool/SqlServer/mssql-cli.md --toc -o mssql-cli.pdf --pdf-engine=xelatex -V CJKmainfont="Source Han Serif TW" -V geometry:margin=0.5in  --variable urlcolor='[HTML]{9068be}' --variable fontsize=12pt
```

1. -V CJKmainfont : 設定中文字型
2. -V ge0metry:margin : 設定pdf邊界
3. --variable urlcolor : 超連結顏色
4. --variable fontsize

## 相關連結

[documents](https://pandoc.org/demos.html)

[markdown-with-chinese](https://jdhao.github.io/2017/12/10/pandoc-markdown-with-chinese/)

[texlive](https://wiki.archlinux.org/title/TeX_Live#tllocalmgr)

[margin-size](https://stackoverflow.com/questions/13515893/set-margin-size-when-converting-from-markdown-to-pdf-with-pandoc)