# selenium

[官方網址](https://www.selenium.dev/)


 ## quick tour

- 配合python使用，安裝package

```bash
pip3 install selenium
```

- 確認是否安裝成功
```bash
pip3 show selenium
```

- 各瀏覽器需安裝不同driver
    1. firefox說明 geckodriver [firefox](https://firefox-source-docs.mozilla.org/testing/geckodriver/Support.html)

    2. github 下載[geckodriver](https://github.com/mozilla/geckodriver/releases)
    3. download geckodriver-v0.29.0-linux64.tar.gz

- 至Downloads 路徑
```bash
    mv -vi geckodriver-v0.29.0-linux64.tar.gz ~/opt/
```

- 解壓縮
```bash
tar xzvf geckodriver-v0.29.0-linux64.tar.gz
```

- 移至 /usr/local/bin
```bash
mv geckodriver /usr/local/bin/
```

- 新增test_website.py進行測試
```python
from selenium import webdriver
fireFoxOptions=webdriver.FirefoxOptions()
fireFoxOptions.set_headless()
browser=webdriver.Firefox(executable_path="/usr/local/bin/geckodriver",firefox_options=fireFoxOptions)
browser.get("https://udb.moe.edu.tw")
print(browser.page_source)
browser.close()
```

- 測試
```bash
python3 test_website.py
```