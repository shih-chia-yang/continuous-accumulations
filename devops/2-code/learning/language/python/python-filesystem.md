# python-filesystem

|函式|說明|
|--|--|
|os.getcwd(),Path.cwd()|取得工作目錄|
|os.name|取得作業系統名稱|
|sys.platform|取得詳細的作業系統名稱|
|os.environ|取得環境變數及其值|
|os.listdir(path)|列出目錄中所有子目錄與檔案|
|os.scandir(path)|取得目錄內所有項目完整資訊|
|os.chdir(path)|改變目前工作目錄|
|os.path.join(elements),Path.joinpath(elements),Path / element / element|將多個參數合併組合為路徑名稱|
|os.path.spilt(path)|將basename與路徑的其餘部分拆開|
|Path.parts|將路徑中所有資料夾或檔案拆開|
|os.path.splitext(path),Path.suffix|取得路徑中的副檔名|
|os.path.basename(path),Path.name|取得路徑的basename|
|os.path.commonprefix(list_of_paths)|找出所有路徑共同的前面部份|
|os.path.expanduser(path)|將路徑中的～或～user轉換為使用者個人資料夾|
|os.path.expandvars(path)|展開路徑中的shell環境變數|
|os.path.exists(path),Path.exists()|檢查路徑是否存在|
|os.path.isdir(path),Path.is_dir()|檢查路徑是否為目錄|
|os.path.isfile(path),Path,is_file()|檢查路徑是否為檔案|
|os.path.islink(path),Path.is_symlink()|檢查路徑是否為symbols links(linux 適用)|
|os.path.ismount(path)|檢查路徑是否為掛載點|
|os.path.isabs(path),Path.is_absolute()|檢查路徑是否為絕對路徑|
|os.path.samefile(path1,path2),Path,smaefile(Path2)|檢查兩個路徑是否指向同一個檔案|
|os.path.getsize(path)|取得大小|
|os.path.getmtime(path)|取得上次修改時間|
|os.path.getatime(path)|取得上次存取時間|
|os.rename(old_path,new_path),Path.rename(new_path)|重新命名或搬移檔案或目錄|
|os.mkdir(path)|建立新目錄|
|Path.mkdir(parents=True)|建立新目錄，如果parents=True，則會自動建立多層目錄|
|os.rmdir(path),Path.rmdir()|刪除一個空目錄|
|glob.glob(pattern),Path.glob(pattern)|取得目前工作目錄下與萬用字元樣式相符合的項目|
|os.walk(path)|遞迴取得所有子目錄中的所有檔案|