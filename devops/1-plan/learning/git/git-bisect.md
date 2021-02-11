# Git Bisect

## git bisect 查詢commit提交出現錯誤

原理是將代碼的提交歷史，按照2分法不斷縮小定位。所以謂2分法就是將代碼歷史一分為二，確定問題出在前半部份，還是後半部份，不斷執行這個過程，直到範圍縮小到某一次代碼提交。
- clone 專案
- 執行建置
- 執行git log
````bash
git log --pretty=oneline
````
![log](/assets/images/git/gitbisect/gitlog.png)

- 總共有13個提交，最近一次是76d6750。
- 使用git bisect start 指令執行查找，格式如下
````bash
git bisect start [終點] [起點]
````
上面的指令，終點是最近的提交，起點是更久以前的提交。它們之間的這段歷史，就是可能出錯的範圍
範例起點設為
````bash
git bisect start HEAD 94354a3
````
- 執行以上命令，repo就是切換至指定的commit，執行建置測試，若功能正常則使用`git bisect good`定義此commit，執行後，git會自動切換是後段的中間commit，再執行建置測試，若錯誤則執行`git bisect bad`，不斷重覆此過程，直到git定位至最初產生錯誤的commit。便可開始著手修復。

- 輸入` git bisect reset`，退出偵錯。