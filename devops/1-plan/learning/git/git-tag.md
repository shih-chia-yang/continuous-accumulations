# git tag

[[git]]

## 說明

可以對某一時間點的commit加上標籤，例如釋出某種軟體版本時，主要分為以上2種類型；一般都建議使用annotated tag，以利保留相關資料，
但若只是臨時性標籤或不需要額外資訊時，可以直接使用lightweight
- lightweight
    > 不會變化的分支，實際為指向特定commit的參考
- annotated
    > 為儲存在repo中的獨立物件，有自身的檢驗和資訊，包含標籤名字、電子郵件、日期、說明，本身支援gpg簽署或驗證
    

## how to annotated

- 建立annotated，使用 `-a <tag name>`, -m 提供標籤說明

````bash
git tag -a v1.0 -m 'initial version 1.0'
````

- 使用gpg進行數位簽章，-a替換為-s
````bash
git tag -s v1.0 -m 'initial version 1.0 signed by me'
````
## how to lightweight

- 直接設定 tag name即可

````bash
git tag v1.0.0
````

## 分享標籤

in default git push won't upload tag to remote server, only thought command shared tag to remote repo.

````bash
git push origin v1.0.0
````

push all local tag at one time,you can use `--tags` argument

````bash
git push origin --tags
````

## pull and delete

**pull**

`git clone` remote repo first, and then `git checkout <tag name>`, this action will take matched version source code，
but now your branch was in `detached HEAD` status，because tag just a snapshot,if you want to modify base on this tag, 
you have to create a branch `git checkout -b <branch name> <tag name>`


**delete tag**
- if tag already pushed to remote repo,you have to delete local first and then delete remote again

````bash
git tag -d v1.0.0
````

````bash
git push origin :refs/tags/v1.0.0
````

## 參考連結
[git tag 標籤用法(如何更好的管理你的分支)](https://www.itread01.com/content/1546264443.html)


[//begin]: # "Autogenerated link references for markdown compatibility"
[git]: git.md "git"
[//end]: # "Autogenerated link references"