# format-usb-drive

- locate USB drive
````bash
df
````

- umount and format USB drive

````bash
sudo umount /dev/sdb1
````

- FAT32 file system

````bash
sudo mkfs.vfat /dev/sdb1
````

- NTFS file system

````bash
sudo mkfs.ntfs /dev/sdb1
````

- exFAT file system

````bash
sudo mkfs.exfat /dev/sdb1
````

- verify USB drive formating

````bash
sudo fsck /dev/sdb1
````