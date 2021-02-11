# Docker Ps

> [!NOTE]
> 不清楚為何與`docker container ls`功能重覆，但此指令有完整範例說明，`docker container ls`則沒有。

- 列出container 清單
```powershell
docker ps [options]
```

- Options

|參數|預設值|說明|
|--|--|--|
| --all,-a||呈現全部的container(預設只呈現運行中的)|
| --quiet,-q||只呈現container id|
| --filter,-f||設定篩選條件|
| --no-trunc||不要截短呈現結果|


- filtering

|條件|說明|
|--|--|
|id|container's id|
|name|container's name|
|exited|container exited code。需搭配 --all|
|ancestor|篩選image；`<image-name[:<tag>]>`、`<image id>`、`<image@digest>`|
## 練習

> [!TIP]
> image的查詢條件為ancestor

```powershell
docker ps --no-trunc -a
```
:::image type="content" source="../../../../assets/images/docker-ps/docker-ps--no-trunc-a.png" alt-text="dockerpsnotrunc":::

呈現所有container
```powershell
docker ps -a 
```

呈現所有container佔用空間
```powershell
docker ps -s 
```

### filter 

1. exited
```powershell
docker ps -a -f 'exited=0'
```
2. status
created,restarting,running,removing,paused,exited,dead
```powershell
docker ps -f status=exited
```
3. ancestor
- image
- image:tag
- image:tag@digest
- short-id
- full-id

```powershell
docker ps -a -f ancestor=helllowrld:1.0.0
```