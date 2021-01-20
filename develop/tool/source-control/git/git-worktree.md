# Git WorkTree

在一個儲存庫上建立多個工作目錄
````git
git worktree add <path> <branch>
````

|參數|說明|
|----|------|
|path|指定要建立工作目錄的目錄名稱|
|branch|在工作目錄上指定要checkout的分支。若省略此設定，則系會自動checkout與path同名的分支

----
- 顯示工作目錄一覽
````Git
git worktree list
````
- 刪除工作目錄的管理資料
````git
git worktree prune [-n] [--verbose]
````
|參數|說明|
|----|----|
|-n(--dry-run)|顯示要刪除的工作目錄一覽表，並非實際刪除資料|
| --verbose|顯示刪除結果|

----
建立工作目錄
````git
git branch
````
````git
git worktree list
````
確認目前分支是否其他工作目錄

移到要作業的工作目錄
````git
git worktree add ../repositoryname.{feat/bug/refactor}/資料夾名稱 分支名稱 
````
將分支指向新的資料夾路徑，這樣就可以同時對2個分支作業

- 刪除工作目錄

確認工作目錄是否存在，在執行刪除工作目錄前，要先刪除資料夾
````git
git worktree list
````
確認刪除的工作目錄是那一個，實際上並未執行

````git
git worktree prune --dry-run
````
刪除工作目錄

````git
git worktree prune --verbose
git worktree list
````

