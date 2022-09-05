# Dockerfile

## 指令清單
- ADD
- CMD
    > 會在docker 啟動容器時自動執行該命令
- COPY
    > 將檔案或資料夾從本機複製到映像檔裡，語函為 <原始路徑> <目標路徑>，如以下範例為，將當前位置資料夾複製到image中的app資料夾下
    ````yaml
    COPY . /app
    ````
- ENV
- EXPOSE
- FROM
    > 用來載入暈像檔，每個映像檔都必須以其他映像檔作為基礎
- LABEL
- STOPSIGNAL
- USER
- VOLUME
- WORKDIR
    > 會在映像檔系統中建立一個目錄，並將其設為工作目錄
- ONBUILD


1. escape 逸位字等
```dockerfile
#escape=\
```
or
```dockerfile
#escape=`
```
