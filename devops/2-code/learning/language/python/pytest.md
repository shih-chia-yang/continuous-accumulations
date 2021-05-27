# pytest


## trouble shotting

1. pytest command not found。依照waring path message 於`.bashrc`設定`PATH`環境變數，如下範例

- 於`bashrc`新增
```bash
export PATH="$PATH:/home/<you>/.local/bin"
```
- 執行source .bashrc
```bash
source .bashrc
```

2. python pytest import module error

```python
import sys, os
myPath = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, myPath + '/../')

```

[link](https://www.itread01.com/content/1545927610.html)

[pytest cannot import module while python can](https://stackoverflow.com/questions/41748464/pytest-cannot-import-module-while-python-can)

[PATH issue with pytest 'ImportError: No module named YadaYadaYada'](https://stackoverflow.com/questions/10253826/path-issue-with-pytest-importerror-no-module-named-yadayadayada)

## 相關連結

[python-test-discovery](https://docs.pytest.org/en/stable/goodpractices.html#python-test-discovery)

[Changing standard (Python) test discovery](https://docs.pytest.org/en/stable/example/pythoncollection.html)

[install pip](https://linuxhint.com/install_pip_archlinux/)