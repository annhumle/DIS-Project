using DIS.ApiTwo.Data;
using DIS.ApiTwo.CycleTracker;
using DIS.ApiTwo.CycleTracker.Interfaces;
using DIS.ApiTwo.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<DatabaseConnection>();

builder.Services.AddScoped<ICycleTrackerRepository, CycleTrackerRepository>();
builder.Services.AddScoped<ICycleTrackerService, CycleTrackerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.MapControllers();

app.Run();