# Git Gpg
[[git]]
## git GPG 設定
GPG全名為`The GNU Privacy Guard`，又稱GunPG，是一套OpenPGP標準規格RFC4880的開源實作。
你可以用GnuPG來對訊息或檔案進行加密(encrypt)或簽章(sign)。GPG是一組CLI命令列工具，可以與
其他應用程式整合，例如Git或Outlook，也支援S/MIME與Secure Shell(ssh)等應用。
### 個人實作
- 不需下載gpg4win，git版，git version 2.30.0.windows.1本就有提供, 在`C:\Program Files\Git\usr\bin`中，若另外安裝Gpg4win需區分在powershell執行或是git bash執行，兩者有明顯不同，或是產生key時，先進行git config變更gpg預設執行路徑。

1. 執行git bash
2. 輸入`gpg --full-generate-key`
   
   - 依次出現加密方式
   - 加密長度
   - 有效期限
   - 使用者名稱與信箱
![generate](/assets/images/git/gpg/gpgkey-1.png)

3. 輸入`gpg --list-secret-keys --keyid-format long`
![secret-keys](/assets/images/git/gpg/gpgkey-2.png)

4. 設定git config 私密金鑰
   ````bash
   git config --global user.signingkey  516CDF6149829DA7
   ````
5. commit 預設加入簽章
````bash
git config --global commit.gpgsign true
````
6. 設定cache時間，不需每次都需輸入密碼
````bash
gpgconf default-cache-ttl 34560000
gpgconf max-cache-ttl 34560000
````
7. 重新啟動gpg.agent
````bash
gpgconf kill gpg.agent
````

#### export gpg key

```git
 gpg --export-secret-keys 
```

#### import gpg key

```bash
gpg --import <filename>.key
```

## reference

* [保哥] - https://blog.miniasp.com/post/2020/05/04/How-to-use-GPG-sign-git-commit-and-tag-object
* [github] - https://docs.github.com/en/free-pro-team@latest/github/authenticating-to-github/generating-a-new-gpg-key
* [Gpg4win] - https://www.gpg4win.org/index.html
* [how to enable auto-signing Git commits with GnuPG for programs that don't support it natively] - https://gist.github.com/BoGnY/f9b1be6393234537c3e247f33e74094a

[//begin]: # "Autogenerated link references for markdown compatibility"
[git]: ../learning/git/git.md "git"
[//end]: # "Autogenerated link references"