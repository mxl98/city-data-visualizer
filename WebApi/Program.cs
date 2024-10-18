using WebApi.Controllers.DataController;
using WebApi.Services.CsvParserService;
using WebApi.Services.ExternalApiService;
using WebApi.Services.JsonParserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();
builder.Services.AddScoped<DataController>();
builder.Services.AddScoped<IJsonParserService, JsonParserService>();
builder.Services.AddScoped<ICsvParserService, CsvParserService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/update_db", async () =>
{
    var data = String.Empty;

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var dataController = services.GetRequiredService<DataController>();
            var SourceUrls = dataController.GetSourceUrls();
            
            foreach(var pair in SourceUrls) 
            {
                Console.WriteLine($"Title: { pair.Key }");
                Console.WriteLine($"Url: { pair.Value }");
                data = await dataController.FetchFromExternalApi(pair.Value);
                dataController.WriteFile(data, pair);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Program - Error occured: { e.Message }");
        }
    }
    return "Database updated!";
})
.WithName("UpdateDB")
.WithOpenApi();

app.MapGet("/api/piscines_all", async () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var dataController = services.GetRequiredService<DataController>();
            dataController.ReadCsvFile<PiscineModel>("db/piscines.csv");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Program - Error occured: { e.Message }");
        }
    }
    return "Read piscines.csv!";
})
.WithName("GetAllPiscines")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}