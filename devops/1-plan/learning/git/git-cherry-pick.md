# Git Cherry Pick

# 查詢指定提交訊息sha碼
````bash
git log --oneline
````
# 使cherry-pick 指令，指定一個commit
1. 切換至要取得commit的branch
````bash
git checkout <branchname>
````
2. 使用cherry-pick指定sha1碼
   - -e (--edit)編輯訊息
   - -n (--no-commit)將cherry-pick執行結果加入工作目錄和索引，但不執行commit
````bash
git cherry-pick 6a4df91 -e -n
````