# Docker Container rm

移除container 

```powershell
docker container rm [options] container [container...]
```

- options
|參數|說明|
|--|--|
| --force,-f|強制刪除執行中的container|
| --link, -l||
| --volumes,-v||

## 練習

移除 container status=exited
```powershell
 docker container rm $(docker container ls -a -q -f status=exited)
```

```powershell
 docker container rm $(docker ps -a -q -f status=exited)
```