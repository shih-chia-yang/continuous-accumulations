# Docker Login

- 登入docker registry

```powershell
docker login [options] [server]
```

- 登入個人registry

```dockerfile
docker login localhost:8080
```

## options
|參數|說明|
|--|---|
| --password,-p|密碼|
| --password-stdin|密碼來自串流|
| --username,-u|使用者名稱|