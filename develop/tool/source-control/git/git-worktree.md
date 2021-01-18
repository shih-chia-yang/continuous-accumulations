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
- 
