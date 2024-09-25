using WebApi.Controllers.DataController;
using WebApi.Services.ExternalApiService;
using WebApi.Static.SourceUrlsOptions;

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

builder.Services.Configure<SourceUrlsOptions>(builder.Configuration.GetSection(SourceUrlsOptions.SectionName));

builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();
builder.Services.AddScoped<DataController>();
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
        var dataController = services.GetRequiredService<DataController>();
        var urls = builder.Configuration.GetSection(SourceUrlsOptions.SectionName).Get<Dictionary<string, string>>() ?? new Dictionary<string, string>();
        foreach (var url in urls) {
            var data = await dataController.FetchFromExternalApi(url.Value);
            dataDictionary.Add(url.Key, data);
            Console.WriteLine($"Fetched data: { url.Key }\n{ data }");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error occured: { e.Message }");
    }
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}