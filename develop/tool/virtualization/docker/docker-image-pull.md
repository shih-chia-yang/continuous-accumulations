# Docker Image Pull

- 下載映像檔
```dockerfile
docker image pull [options] name[:tag|@digest]
```

## 個人練習

下載windows server時，container未切換至windows container，linux container無法pull windows server
```dockerfile
docker image pull mcr.microsoft.com/windows:20H2
```

```dotnetcli
docker image pull mcr.microsoft.com/powershell:latest
```