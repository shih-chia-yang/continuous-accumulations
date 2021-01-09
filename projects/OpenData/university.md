# University
## 學校基本資料

### 屬性
````text
        /// <summary>
        /// 唯一識別碼
        /// </summary>
        /// <value></value>
        public string UniversityKey { get; set; }
        /// <summary>
        /// 設立別
        /// </summary>
        /// <value></value>
        public Category Category { get; private set; }
        /// <summary>
        /// 體系
        /// </summary>
        /// <value></value>
        public EducationSystem EducationSystem { get; private set; }
        /// <summary>
        /// 父節點識別碼
        /// </summary>
        /// <value>併校、重組時，該識別碼會寫入新學校識別碼</value>
        public string ParentId { get; private set; }
        /// <summary>
        /// 校務資料庫學校識別碼
        /// </summary>
        /// <value></value>
        public string ExternalId { get; private set; }
        /// <summary>
        /// 統計處識別碼
        /// </summary>
        /// <value></value>
        public string StatId { get; private set; }
        /// <summary>
        /// 學校中文名稱
        /// </summary>
        /// <value></value>
        public string ChtName { get; private set; }
        /// <summary>
        /// 學校英文名稱
        /// </summary>
        /// <value></value>

        public string EngName { get; private set; }
        /// <summary>
        /// 經營狀態
        /// </summary>
        /// <value></value>
        public UniversityState State { get; private set; }
````

### 建構子
* [ ] 確認必填欄位, 目前為設立別、體系、校務資料庫學校識別碼、統計處學校代碼、學校中文名稱、經營狀態
````text
    public University(Category category, EducationSystem educationSystem
        , string externalId, string statId
        ,string chtName,UniversityState state)
        {
            Category = category;
            EducationSystem = educationSystem;
            ExternalId = externalId;
            StatId = statId;
            ChtName = chtName;
            State = state;
        }
````

### 行為
* [ ]確認是否已具備所有邏輯
- 併校
1. 當前學校會將UniversityKey 寫入 oldUniversities集合各個的ParentId
2. oldUniversities集合每個經營狀態將改變成UniversityState.Mergered
3. 資料庫新增當前學校，同時修改集合oldUniversities屬性
````text
public IEnumerable<University> Merge(University[] oldUniversities)

````

- 重組、改制
1. 當前學校ParentId寫入newUniversity.UniversityKey
2. 當前學校UniversityState改變為UniversityState.Reorganized
3. 資料庫新增newUniversity，同時修改當前學校屬性
````text
public University Reorganize(University newUniversity)
````

- 設定ParentId
````text
public void SetParentId(string id)
````

- 變更學校狀態
````text
public void ChangeState(UniversityState state)
````