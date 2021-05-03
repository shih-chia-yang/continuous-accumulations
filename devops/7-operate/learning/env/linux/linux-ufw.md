# linux-ufw

- 查詢狀態
```bash
sudo ufw status
```

- 查詢rules

```bash
sudo ufw status numbered
```

- 刪除rules
```bash
sudo ufw delete <num>
```

- 新增 規則
```bash
sudo ufw allow http
sudo ufw allow https
sudo ufw allow out 1433
```