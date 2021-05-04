# three_tier_project

browser -> Business Layer -> DB

Api:

|Api|description|request body|Response body|
|--|--|--|--|
|Get /api/GetEmployees| get all Employee | None |array of Employee items|
|Get /api/Employee/{id} | get an item by ID | None | Employee item|
|Post /api/AddEmployee | add a new item | Employee item |String msg|
|Put /api/UpdateEmployee/{id} | update an existing item | Employee item |String msg|
|Delete /api/DeleteEmployee/{id} | delete an item | None | String msg|


- **Controller** : class object that handles HTTP request and creates an HTTP

- **DB** : EmployeeRepository which has three employees in it.

- **Web Client** :  Browser


1. Create `three_tier_project` Solution

```dotnetcli
dotnet new sln -o three_tier_project
```

2. Create Domain project `Employee.Domain`
```dotnetcli
dotnet new classlib -o Employee.Domain
```
- add Employee domain Model

3. Create Infrastructure project `Employee.Infrastructure`

    ```dotnetcli
    dotnet new classlib -o Employee.Infrastructure
    ```
    
    - add reference `Employee.Domain`
    
    ```dotnetcli
    dotnet add reference ../Employee.Domain/Employee.Domain.csproj
    ```

    - install package `Microsoft.EntityFrameworkCore`
    
    ```dotnetcli
    dotnet add package Microsoft.EntityFrameworkCore
    ```
    - install package `Microsoft.EntityFrameworkCore.SqlServer`
    
    ```dotnetcli
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    ```
    
    - add EmployeeContext.cs,add `DbSet<Employee>`
    
    - add DbInitializer.cs
        
        - Employee 資料初始化
        - 新增至 context
        - SaveChange()

4. create Employee.Api project
    
    ```dotnetcli
    dotnet new webapi -o Employee.Api
    ```
    
    - install package Microsoft.EntityFrameworkCore
    ```dotnetcli
    dotnet add package Microsoft.EntityFrameworkCore
    ```

    - install package Microsoft.EntityFrameworkCore.InMemory
    
    ```dotnetcli
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    ```

    - install package 
    
    ```dotnetcli
    Microsoft.VisualStudio.Web.CodeGeneration.Design
    ```
    
    - add reference Employee.Domain
    - add reference Employee.Infrastructure
    
    - add EmployeeController.cs 
    
    - libman install jquery & jquery-validation & jquery-validation-unobtrusive
    
    - register DbContext in `ConfigureServices`，使用記憶體資料庫，方便測試
    ```aspx-csharp
    services.AddDbContext<EmployeeContext>(options => options.UseInMemoryDatabase("TestEmployee"));
    ```

    - initial DbContext data in Program.cs，找出di容器中的DbContext物件，使用seed資料初始化，加入log機制紀錄是否初始化成功
    
    ```aspx-csharp
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EmployeeContext>();
                    DbInitializer.Initializer(context);
                }
                catch( Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database");
                }
            }

            host.Run();
    ``` 

    - back to EmployeeController.cs ，建構子增加Context參數取得di註冊物件
    
    ```aspx-csharp
        private EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
            DbInitializer.Initializer(_context);
        }
    ```

    - add CRUD action，書本範例中有關context操作部份寫在controller中，可將這部份獨立做Repository，註冊DI，於controller呼叫使用，將有關資料庫操作部份將controller 前端動作切割。
    
    - 前端javascript暫時跳過
    
    - 調整launchsetting.json中的launchUrl，可設定初始頁面