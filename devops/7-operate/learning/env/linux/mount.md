# mount

- 查詢usb
```bash
lsblk
```
or 

```bash
fdisk -l
```

- 建立mount 資料夾
```bash
mkdir /mnt/usb
```

- 掛載
```bash
mount /dev/sda /mnt/usb
```

- 卸載
```bash
unmount /mnt/usb
```