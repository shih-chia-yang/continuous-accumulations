# ssh

## how to settings

[configuring windows server 2019 open ssh server to support key based authentication](https://youtu.be/Cs3wBl_mMH0)

[ssh防火牆與安裝說明](https://docs.microsoft.com/zh-tw/windows-server/administration/openssh/openssh_install_firstuse)


> 影片操作範例為ssh遠端部份為windows server core，win10仍適用，部份指令可跳過

- Client Side:
    1. open powershell
    2. whoam i ==>查看目前使用者 username@hostname
    
    ```powershell
    ///安裝ssh client
    Add-WindowsCapability -online -name OpenSSH.Client~~~~0.0.1.0
    ```
- Server Side:
    - open powershell
    ```powershell
    Add-WindowsCapability -online -name OpenSSH.Server~~~~0.0.1.0
    Add-WindowsCapability -online -name OpenSSH.Client~~~~0.0.1.0
    ```
    下載open source nano/vim editor，修改sshd_config時使用

    ```powershell
    set-executionpolicy Bypass
    iwr https://chocolatey.org/install.ps1 -UseBasicParsing | iex
    choco install -y nano or choco install -y vim 
    set-service -name ssh-agent -startuptype 'automatic'
    set-service -name sshd -startuptype 'automatic'
    start-service ssh-agent
    start-service sshd
    ```

- Client Side
    - open powershell
    ```powershell
    ssh <username>@<hostname>
    ```
    - then system will asking you sure want to trust it when first time login
    - keypress : [yes]
    - asking for password : [xxxxxx]
    - login successfully

    - 以下為金錀驗證設定
    ```powershell
    cd ~\.ssh
    ssh-keygen => example cmd : ssh-keygen -t -rsa 4096 -f "$HOME\.ssh\id_rsa_remote_ssh"
    ```
    generate completed，在資料夾中可以發現id_rsa`(private key)`，id_rsa.pub`(public key)`
    
    ```powershell
    set-service ssh-agent -startuptype 'automatic'
    start-service ssh-agent
    ssh-add .\id_rsa ///將private key加入ssh-agent中
    ssh <username>@<hostname> mkdir c:\users\<username>\.ssh
    將client端的public key加至server端的.ssh資料夾下的authorized_keys
    scp c:\users\<client user name>\.ssh\id_rsa.pub <remoteuser>@<hostname>:c:\users\<remoteuser>\.ssh\authorized_keys
    ```

- Server side:
    - 使用ssh登入遠端主機，檢查資料夾是否新增authorized_keys
    - 確認檔案權限問題。
        - [link1](https://github.com/PowerShell/Win32-OpenSSH/issues/1313)
        - [link2](https://github.com/PowerShell/Win32-OpenSSH/wiki/Security-protection-of-various-files-in-Win32-OpenSSH)
    - edit sshd_config
    ```powershell
    nano/vim c:\programdata\ssh\sshd_config
    ```
    - 移至最後一行，將match group administrator註記，存檔
    - 有關sshd config設定可以參考下方有關安全性、連線說明

- Client side:
    - 測試是否無需輸入密碼即可登入Server
    ```powershell
    ssh <remoteuser>@<remotehost>
    ```
## config

- 認證代理 : 當本機上的同一把金錀曾在多台server上註冊並啟用此設定，則可於多台server上轉發，也就是可以使用本機的金錀登入第二層、第三層的server
```json
ForwardAgent yes
```

- 壓縮頻寬 : 先藉由cpu壓縮後再進行傳輸，適用於效能較好的機器
```json
Compression yes
```

- 減少重複連線的時間
```json
ControlMaster auto
ControlPath /tmp/ssh-%r@%h:@p
```

- 延長連線時間
```json
ControlPersist 1h
```

- 取消連線加密 : 區網、可信賴的網路環境及大量檔案傳輸之情境使用此參數可有效減少cpu負載以提升傳輸速度。
```json
Ciphers arcfour
```

## security

1. strong username and passwords
2. Configure idle timeout interval

    ```json
    ClientAliveInterval 360
    ClientAliveCountMax 0
    ```
3. disable empty passwords

    ```json
    PermitEmptyPasswords no
    ```

4. limit users ssh access : you should limit your ssh logins to only certain users who need remote access.
    
    ```json
    AllowUsers user1 user2
    ```
    - linux
    ```bash
    /etc/init.d/sshd restart or service sshd restart
    ```
    - windows
    ```powershell
    restart-service sshd
    ```

5. disable root logins

    ```json
    PermitRootLogin no
    ```
6. Only Use SSh Protocol 2 : ssh has two protocols that it can use. 
    1. protocol 1 is older and is less secure. 
    2. protocol 2 is what you should be using to harden your security.

    ```json
    
    #Protocol 2,1
    /// remote protocol1 ,then uncomment it (remove the #)
    ///result
    Protocol 2
    ```

7. Use Another Port : using a non-standard port is to avoid being seen by casual scans. 

    ```json
    #run ssh on a non standard Port
    Port 2025 
    ```

8. allow only specific clients : 原則禁止，例外開放，防火牆設定特定ip特定port允許連入

for linux
```bash
iptables -A INPUT -p tcp -s <YourIP> --dport 22 -j ACCEPT
iptables -A INPUT -p tcp --dport 22 -m state --state NEW -m recent --set --name ssh --resource
iptables -A INPUT -p tcp --dport 22 -m state --state NEW -m recent ! --rcheck --seconds 90 --hitcount 3 --name ssh --rsource -j ACCEPT
```

9. enable two-factor authentication

10. use public/private keys for authentication

## 相關連結

[Remote Development using SSH](https://code.visualstudio.com/docs/remote/ssh#_getting-started)

[產生SSHKey並且透過KEY進行免密碼登入](https://xenby.com/b/220-%E6%95%99%E5%AD%B8-%E7%94%A2%E7%94%9Fssh-key%E4%B8%A6%E4%B8%94%E9%80%8F%E9%81%8Ekey%E9%80%B2%E8%A1%8C%E5%85%8D%E5%AF%86%E7%A2%BC%E7%99%BB%E5%85%A5)
[如何使用 SSH 控制連線 Windows 伺服器](https://www.gushiciku.cn/pl/2GFQ/zh-tw)

[openssh security](https://www.cyberciti.biz/tips/linux-unix-bsd-openssh-server-best-practices.html)

[10 Steps to Secure Open SSH](https://blog.devolutions.net/2017/4/10-steps-to-secure-open-ssh)