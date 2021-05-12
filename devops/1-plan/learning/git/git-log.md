# git-log

- 查詢個人程式碼數量
```bash
git log --author="<username>" --pretty=tformat: --numstats | awk '{ add +=$1; subs+=$2; loc +=$1 - $2} END {printf "added lines: %s,removed lines: %s,total lines:%s\n"}",add,subs,loc}' -
```

- 統計每個人增刪行數
```bash
git log --format='%aN' | sort -u | while read name; do echo -en "$name\t"; git log --author="$name" --pretty=tformat: --numstat | awk '{ add += $1; subs += $2; loc += $1 - $2 } END { printf "added lines: %s, removed lines: %s, total lines: %s\n", add, subs, loc }' -; done
```

- 查詢提交數量排前5
```bash
git log --pretty='%aN' | sort | uniq -c | sort -k1 -n -r | head -n 5
```

- 提交數統計
```bash
git log --oneline | wc -l
```

- 添加或修改的程式碼
```bash
git log --stat|perl -ne 'END { print $c } $c += $1 if /(\d+) insertions/'
```

## gitstats

[github](https://github.com/hoxu/gitstats)

## cloc

- archlinux

```bash
sudo pacman -S cloc
```