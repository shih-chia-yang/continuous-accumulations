# git clean

[[git]]

- delete untracked local records
    > 刪除工作目錄中沒有tracked的檔案，常與`git reset --hard`一起使用
    > `reset`只會影響加入track的檔案，所有需要`clean`刪除沒有加入track的檔案
    > 結合2個指令可以將工作目前完全回到指定的commit狀態

- 模擬clean，提醒那些檔案會被刪除，但不會進行刪除的動作

````bash
git clean -n
````

- 刪除目前資料夾下所有沒有加入track的檔案，不會刪除.gitignore設定指定的路徑與檔案

````bash
git clean -f

git clean -f <path>
# 刪除指定路徑下沒有加入track的檔案

git clean -df
# 刪除目前路徑下沒有加入track的檔案和資料夾

git clean -xf
# 刪除目前路徑下沒有加入track的檔案，無論是否為.gitignore中所設定的檔案與資料夾
````

- example
    > 假設你已經加入stash，而且新增功能，但需要刪除所有的修改，包括新增的檔案
    > 執行以下命令後，可以將work directory與tracking changes復原至最新一次的commit狀態
````bash
git reset -hard
git clean -df
````

## 參考連結
[git clean清除untracked的本地記錄](https://www.itread01.com/content/1547528799.html)

[//begin]: # "Autogenerated link references for markdown compatibility"
[git]: git.md "git"
[//end]: # "Autogenerated link references"