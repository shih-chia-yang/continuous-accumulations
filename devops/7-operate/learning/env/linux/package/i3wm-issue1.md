# i3wm-issue1

安裝i3wm後，開啟vscode，切換terminal`ctrl+grave`衝突，不斷跳出提醒視窗，經查詢後得知是dunst package的shortcut,另外i3wm的keybinding 會overwrite dunst的keybinding
    -  修改此文件~/.config/dunst/dunstrc
    - bindsym Control+grave 註解
    - 若無此檔案，則至/usr/share/dunst尋找，本機為dunstrc.gz檔案，unpacking後，cp /usr/share/dunstrc ~/.config/dunst/dunstrc
    [qa](https://www.reddit.com/r/i3wm/comments/bl1uk5/pass_keyboard_command_through_to_focused/)
    [dunst](http://manpages.ubuntu.com/manpages/focal/man1/dunst.1.html)
