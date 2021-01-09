# Git Config

- 搭配gpg，設定全域環境設定個人簽章
````bash
git config --global user.signingkey <your secret short/long key-id>
````

- 搭配gpg，設定全域環境設定，預設commit使用gpg 
````bash
git config --global commit.gpgsign true
````

- 搭配gpg，設定全域環境詘定，預設push使用gpg
````bash
git config --global push.gpgsign true
````

- 設定ui status顏色，狀態改變:黃；未加入變更索引:紅；已加入變更索引:綠
````bash
git config --global color.ui true
git config --global color.status.changed yellow
git config --global color.status.untracked red
git config --global color.status.added green
````
