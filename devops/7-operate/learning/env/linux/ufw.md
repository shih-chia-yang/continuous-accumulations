# ufw

- 安裝
```bash
sudo pacman -S ufw
```

- 狀態查詢，預設inactive
```bash
sudo ufw status verbose
```

- 開啟/關閉
```bash
sudo ufw enable
sudo ufw disable
```

- 重新設定
```bash
sudo ufw reset
```

- 查詢規則；numbered=>編號
```bash
sudo ufw status numbered
```

- 刪除規則
```bash
sudo ufw delete <number>
```

- 允許/封鎖 
```bash
sudo ufw allow
sudo ufw deny
```

- 預設規則
```bash
sudo ufw default allow 預設允許
sudo ufw default deny 預設封鎖
```

- ex: 封鎖所有連入，允許對外
```bash
sudo ufw default deny incoming
sudo ufw default allow outgoing
```

- 新增規則
```bash
sudo ufw allow ssh =>允許服務
sudo ufw allow in <port> 允許port連入
sudo ufw deny out <port> 封鎖對外port
sudo ufw deny in <port> 封鎖連入port
```

- 啟用日誌記錄，/var/log/ufw.log
```bash
sudo ufw logging on 
```

- 允許/拒絕 特定ip連線指定port
```bash
sudo ufw allow from <ip> to any port <number>
sudo ufw deny from <ip> to any port <number>
sudo ufw allow from 192.1.1.0/24 to any port <number>
```
- 指定網路設備允許連線
```bash
sudo ufw allow in on <network-interface-name> to any port <number>

sudo ufw allow in on eth0 to any port 80
```