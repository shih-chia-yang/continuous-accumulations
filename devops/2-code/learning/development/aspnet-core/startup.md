# startup

[[middleware]]

主要負責

1. Service相依性註冊 =>ConfigurationServices()


```aspx-csharp
public void ConfigureServices(IServiceCollection services)
{

            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentQueries, StudentQueries>();
            services.AddTransient<IRequestHandler<CreateStudentCommand, bool>, CreateStudentCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateStudentCommand, bool>, UpdateStudentCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteStudentCommand, bool>, DeleteStudentCommandHandler>();
            
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<IInstructorQueries, InstructorQueries>();
            services.AddTransient<IRequestHandler<CreateInstructorCommand, bool>, CreateInstructorCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteInstructorCommand, bool>, DeleteInstructorCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateInstructorCommand, bool>, UpdateInstructorCommandHandler>();
}
```

2. Middleware設定 => Configure() [[middleware]]
```aspx-csharp
public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
{
    if (env.IsDevelopment())
            {
                //開發者例外頁
                app.UseDeveloperExceptionPage();
                //swagger open api
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "code.Api v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //http 轉向https
            app.UseHttpsRedirection();
            //啟用靜態檔服務
            app.UseStaticFiles();
            //使用路由
            app.UseRouting();
            //跨原始資源共用
            app.UseCors(StartupExtensionMethods.CorsPolicy);
            //授權
            app.UseAuthorization();
            //端點路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
}
```

## Environment

調用`IWebHostEnvironment`取得環境變數，可在`Configure`判斷環境

[//begin]: # "Autogenerated link references for markdown compatibility"
[middleware]: project/middleware/middleware.md "middleware"
[//end]: # "Autogenerated link references"