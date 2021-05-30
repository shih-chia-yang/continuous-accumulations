# aggregate-persistence

目前設計的模型並不是針對資料庫設計的，當使用關聯資料庫來儲存複雜物件模型時，可能會遇到問題，document database可使用機器可讀格式，如json格式儲存物件模型
，但在轉換與使用上仍然會有諸多限制，domain model需要對資料庫進行調整

- repository pattern
- impedance mismatch
- using a document database for persistence
- using a relational database for persistence

## technical requirements

- [[docker]]
    - docker ce [installation](https://docs.docker.com/install)
    - docker compose [installation](https://docs.docker.com/compose/install/)

- [[efcore]]

- [[postgreSql]]


## repository and units or work

[Patterns of Enterprise Application Architecture](https://martinfowler.com/eaacatalog/repository.html)

書中提到使用泛型的Repository pattern(`Repository<T>`)是一種反模式，而且很常見查詢方法允許使用lambda expression方法進行參數與條件限制
，而Repository將查詢透過ORM底層API進行轉換，使用此種方法讓開發人員認為Repository是ORM框架上不必要的的抽象化。

relational database，通過ORM使用LINQ query translate可能會有效能問題，不僅會嚴重影響應用程序效能，還會嚴重影響database server的效能。

專案經驗:
    - 目前資料公開專案就含有大量使用泛型的repository，建立時僅以為是作為資料庫與服務之間的橋樑，但後期維護即發現每實作一個新實體，都必須繼承Repository<T>, 的動作，但又無任何邏輯需撰寫任何程式碼而變成雞肋，且當有特殊資料需求時，仍然需透過ICustomRepository來定義方法實作, 否則將是變更泛型repository新增通用方法，但影響範圍更大。且使用linq expression當出現複雜查詢時，仍然有效能問題

eric evans 堅持需透過interface規範查詢動作且名稱必須以Ubiquitous Language表達此動作對於使用者獲取資料的意圖
，而不是透過發送任意queries syntax 來查詢資料庫，例如使用`IEnumerable<ClassifiedAd> GetAdsPendingReview()`或`IEnumerable<ClassifiedAd> Query (Specifications.GetAdsPendingReview)`勝過`IEnumerable<ClassifiedAd> Query(x=>x.State==ClassifiedAdState.PendingReview`
    - 透過方法命令表達更多有關業務的概念
    - 讓repository決定如何使用特定的查詢方法

## implementation document database

example : RavenDb
popularity : MongoDB

## implementation Entity framework core

- 新增IUnitOfWork.cs
```csharp
public interface IUnitOfWork
{
    Task Commit();
}
```

- 新增IRepository.cs
```csharp
public interface IRepository
{
    IUnitOfWork UnitOfWork{ get; }
}
```

- 新增ClassifiedAdEntityTypeConfiguration.cs，使用EntityTypeConfiguration設定資料表欄位與關聯
    - 暫時先新增Id欄位
```csharp
public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
{
    public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
```


- 新增ClassifiedAdContext.cs繼承IUnitOfWork介面，提供相同context事件可以在同一個transaction執行後Commit()

```csharp
public class ClassifiedAdContext:DbContext,IUnitOfWork
{
    private readonly ILoggerFactory _logger;

    public DbSet<ClassifiedAd> ClassifiedAds{ get; set; }

    public ClassifiedAdContext(DbContextOptions<ClassifiedAdContext> options):base(options)
    {

    }

    public ClassifiedAdContext(DbContextOptions<ClassifiedAdContext> options,ILoggerFactory logger):this(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
    }


    public Task Commit()
    {
        return base.SaveChangesAsync();
    }
}
```

- 修改IClassifiedAdRepository，繼承IRepository介面，將Save函式移除，新增Add與Exists方法

```csharp
public interface IClassifiedAdRepository:IRepository
{
    /// <summary>
    /// loads an entity by id
    /// </summary>
    /// <param name="id">entity id</param>
    /// <typeparam name="TEntity">entity type
    /// </typeparam>
    /// <returns></returns>
    Task<ClassifiedAd> Load(string id);


    /// <summary>
    /// check if entity with a given id already exist
    /// </summary>
    /// <param name="id">entity id</param>
    /// <typeparam name="TEntity">entity type</typeparam>
    /// <returns></returns>
    Task<bool> Exists (string id);

    Task Add(ClassifiedAd entity);

}
```

- 將ClassifiedAdContext注入ClassifiedAdRepository，實作資料庫相關操作
```csharp
public class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly ClassifiedAdContext _context;

    public IUnitOfWork UnitOfWork => _context;
    public ClassifiedAdRepository(ClassifiedAdContext context)
    {
        _context = context;
    }
    public async Task<bool> Exists (string id)
    {
        return await Load(id) != null;
    }

    public async Task<ClassifiedAd> Load (string id)
    {
        return await _context.ClassifiedAds.FindAsync(id);
    }

    public async Task Add(ClassifiedAd entity)
    {
        await _context.ClassifiedAds.AddAsync(entity);
    }
}
```

api專案

- 新增Registry folder andd add StartupExtensionMethods.cs，新增AddCustomDbContext method
```csharp
public static class StartupExtensionMethods
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services,IConfiguration configuration )
    {
        services.AddDbContext<ClassifiedAdContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
```

- 新增AppBuilderExtensions.cs
```csharp
public static class AppBuilderExtensions
{
    public static void EnsureDatabase(this IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetService(typeof(ClassifiedAdContext)) as ClassifiedAdContext;
        if(!context.Database.EnsureCreated())
            context.Database.Migrate();
    }
}
```

---

- 調整entity model中的屬性，提供ef-core EntityTypeConfiguration建立資料表
    - reference type property 需新增建構子public or protected
    - 且其中可揭露的property也同樣需設定為public {get; internal set;}

- 建立migrations
```dotnetcli
dotnet ef migrations add initial
```

- 建立資料庫
```dotnetcli
dotnet ef database update
```

- 使用 tool連線至資料庫，查看表格與欄位

以前經驗model中的型別都是使用value type，第一次練習使用reference type設計欄位，在使用domain primitive上減少domain model上檢查邏輯與提供型別安全
    - 若欄位無特定格式且無複雜邏輯是否仍需使用domain primitive，如邏輯主鍵，是否單純使用int即可，而業務主鍵則使用domain primitive撰寫

設定nest reference type property，Price屬性為例

```csharp
builder.OwnsOne(x => x.Price, price => 
    { 
        price.Property(x=>x.Amount).HasColumnType("decimal(18, 2");
        price.OwnsOne(c => c.Currency,currency=>
        {
            currency.Property(x => x.CurrencyCode).HasMaxLength(10);
            currency.Property(x => x.InUse).HasColumnType("bit");
            currency.Property(x => x.DecimalPlace).HasColumnType("decimal(2, 0)");
        });
    }
);
```

[//begin]: # "Autogenerated link references for markdown compatibility"
[docker]: ../../../7-operate/learning/docker/docker.md "Docker"
[efcore]: ../../../2-code/learning/tool/Efcore/efcore.md "EfCore"
[postgreSql]: ../../../2-code/learning/tool/PostgreSQL/postgreSql.md "postgreSql"
[//end]: # "Autogenerated link references"
