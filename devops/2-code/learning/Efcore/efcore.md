# EfCore

## Ef core

### Entity Framework Core工具 .net core cli

若需要使用code first 執行dotnet ef命令或add-migration,需安裝工具
確認Terminal是否位於正確專案路徑，執行以下命令
````text
dotnet tool install --global dotnet-ef
````

````text
dotnet add package Microsoft.EntityFrameworkCore.Design
````

驗證安裝

````text
dotnet ef
````

[Entity Framework Core 工具參考-.NET Core CLI](https://docs.microsoft.com/zh-tw/ef/core/cli/dotnet)

in addition , you might need to add the following NuGet packages to your project
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools

### 設計階段 DbContext建立

使用`IDesignTimeDbContextFactory<TContext>`，實作該介面類別，dotnet ef tool會略過其他建立`DbContext`的方法，並改為使用設計階段factory

````text
public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
{
    public BloggingContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
        optionsBuilder.UseSqlite("Data Source=blog.db");

        return new BloggingContext(optionsBuilder.Options);
    }
}
````

[設計階段 DbContext 建立](https://docs.microsoft.com/zh-tw/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli)

### dotnet ef Migrations
Entity Framework Core Migrations

新增 migrations
````text
dotnet ef migrations add your_migrations_name
````
資料庫更新
````text
dotnet ef database update your_migrations_name

--connection
dotnet ef database update your_migrations_name --connection your_connection_string
````

資料庫drop
````text
--force 不要確認
--dry-run 顯示要卸載的資料庫，但不要卸載它
dotnet ef database drop
````

[`learn Entity Framework Core`](https://www.learnentityframeworkcore.com/migrations)

### Entity Framework Core: private or protected navigation properties

two options, using type/string inside the model builder.
````text
modelBuilder.Entity<Model>(c =>
    c.HasMany(typeof(Model), nameof(Model.childs)
        .WithOne(nameof(Child.parent))
        .HasForeignKey("id");
);
````

or use backing field
````text
var elementMetadata = Entity<Model>().Metadata.FindNavigation(nameof(Model.childs));
    elementMetadata.SetField("_childs");
    elementMetadata.SetPropertyAccessMode(PropertyAccessMode.Field);
````
Alternatively try that with a property
````text
var elementMetadata = Entity<Model>().Metadata.FindNavigation(nameof(Model.childs));
    elementMetadata.SetPropertyAccessMode(PropertyAccessMode.Property);
````
Be aware, as of EF Core 1.1, there is a catch: The metadata modification must be done last, after all other .HasOne/.HasMany configuration, otherwise it will override the metadata.
[`Re-building relationships can cause annotations to be lost`](https://github.com/dotnet/efcore/issues/6674)

### InMemory 記憶體資料庫
````text
.net core cli
dotnet add package Microsoft.EntityFrameworkCore.InMemory

visual studio
Install-Package Microsoft.EntityFrameworkCore.InMemory
````