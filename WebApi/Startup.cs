using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Quartz;
using Quartz.Spi;
using WebApi.Controllers.DataController;
using WebApi.Services.CsvParserService;
using WebApi.Services.ExternalApiService;
using WebApi.Services.JsonParserService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfigurationManager config)
    {   
        string connectionString = $"Server=db;Database=cdv;User={Environment.GetEnvironmentVariable("MYSQL_USER")};Password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD")};Port=3306";

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddHttpClient<IExternalApiService, ExternalApiService>();
        services.AddScoped<DataController>();
        services.AddScoped<IJsonParserService, JsonParserService>();
        services.AddScoped<ICsvParserService, CsvParserService>();
        services.AddScoped<IPiscineService, PiscineService>();
        services.AddScoped<DbUpdateJob>();
        services.AddControllers();

        services.AddDbContextPool<AppDbContext>(options =>
            options.UseMySql(connectionString, 
            ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
            )
        );

        services.AddSingleton<IJobFactory, CustomJobFactory>();
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey("DbUpdateJob");
            q.AddJob<DbUpdateJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("DbUpdateJobTrigger")
                .WithCronSchedule("0 0 0 * * ?"));
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseCors("AllowAngularApp");

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.MapControllers();
    }
}