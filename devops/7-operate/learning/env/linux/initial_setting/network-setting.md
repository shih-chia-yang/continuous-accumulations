# network-setting

- [links](https://www.server-world.info/en/note?os=Ubuntu_20.04&p=initial_conf&f=3)

- netplan 設定\*.yaml

- 確認介面卡 id 與是否啟用

```bash
ip link
```

- 編輯\*yaml 檔

```bash
sudo vi /etc/netplan/*.yaml
```

- 內容

```yml
# create new

network:
  ethernets:
    # interface name
    enp1s0:
      dhcp4: no
      # IP address/subnet mask
      addresses: [10.0.0.30/24]
      # default gateway
      gateway4: 10.0.0.1
      nameservers:
        # name server to bind
        addresses: [10.0.0.10]
      dhcp6: no
  version: 2
```

- 啟用

```bash
netplan apply
```

- 確認 ip

```bash
ip addr
```

## 關閉 ipv6 設定

```bash
sudo vi /etc/sysctl.conf
```

- 新增 `net.ipv6.conf.all.disable_ipv6 = 1`後執行

```bash
sysctl -p
```

> [!TIP]
> -p Load in sysctl settings from the file specified or /etc/sysctl.conf if none given.

- 確認介面卡設定

```bash
ip addr
```
