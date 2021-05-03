# linux-mount

- 確認nas server ip，與連結位置
    - ex: servername:/mount_point

- 確認防火牆 port開啟： 111,2049,892

- 確認安裝 nfs-utils

```bash
sudo pacman -Q nfs-utils

sudo pacman -S nfs0utils
```

- mount

```bash
sudo mount -t nfs servername:/<mount_point> <client_mount_point>
```

## 相關連結

[NFS](https://wiki.archlinux.org/title/NFS)