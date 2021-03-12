# python-fileoperation

|方法|說明|
|--|--|
|read(n)|由目前位置讀取n個字元，並將目前位置往後移n個字元。若n省略則讀取全部內容。|
|readline()|由目前位置讀取一行文字(包含行尾的`\n`)，並將目前位置移到下一行開頭|
|readlines()|讀取所有的行並依序加入到串列中傳回，串列中每個元素即為一行資料（包含行尾的`\n`)|
|write(str)|將str寫入到檔案中|
|close()|關閉檔案|