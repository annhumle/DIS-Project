using DIS.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "DIS Backend is running");

app.MapGet("/api/person", async (AppDbContext db) =>
{
    var person = await db.Persons
        .Select(p => new
        {
            p.PersonId,
            p.Name,
            p.Gender,
            p.Birthdate
        })
        .FirstOrDefaultAsync();

    return person is null ? Results.NotFound() : Results.Ok(person);
});

app.MapGet("/api/cycles", async (AppDbContext db) =>
{
    var cycles = await db.Cycles
        .OrderBy(c => c.StartDate)
        .Select(c => new
        {
            c.CycleId,
            c.CycleNumber,
            c.StartDate,
            c.EndDate,
            c.PersonId,
            DailyLogCount = c.DailyLogs.Count
        })
        .ToListAsync();

    return Results.Ok(cycles);
});

app.MapGet("/api/cycles/{id}/logs", async (int id, AppDbContext db) =>
{
    var logs = await db.DailyLogs
        .Where(log => log.CycleId == id)
        .OrderBy(log => log.CycleDay)
        .Select(log => new
        {
            log.DailyLogId,
            log.Date,
            log.CycleDay,
            log.CycleId,
            FlowLevel = log.FlowLevel == null ? null : new
            {
                log.FlowLevel.FlowLevelId,
                log.FlowLevel.Amount
            },
            Symptoms = log.DailyLogSymptoms
                .Select(dls => new
                {
                    dls.PhysicalSymptom.PhysicalSymptomId,
                    dls.PhysicalSymptom.Name
                })
                .ToList()
        })
        .ToListAsync();

    return Results.Ok(logs);
});

app.MapGet("/api/dailylogs", async (AppDbContext db) =>
{
    var logs = await db.DailyLogs
        .OrderBy(log => log.Date)
        .Select(log => new
        {
            log.DailyLogId,
            log.Date,
            log.CycleDay,
            log.CycleId,
            FlowLevel = log.FlowLevel == null ? null : new
            {
                log.FlowLevel.FlowLevelId,
                log.FlowLevel.Amount
            },
            Symptoms = log.DailyLogSymptoms
                .Select(dls => new
                {
                    dls.PhysicalSymptom.PhysicalSymptomId,
                    dls.PhysicalSymptom.Name
                })
                .ToList()
        })
        .ToListAsync();

    return Results.Ok(logs);
});

app.MapGet("/api/flow-levels", async (AppDbContext db) =>
{
    var flowLevels = await db.FlowLevels
        .OrderBy(flow => flow.FlowLevelId)
        .Select(flow => new
        {
            flow.FlowLevelId,
            flow.Amount
        })
        .ToListAsync();

    return Results.Ok(flowLevels);
});

app.MapGet("/api/symptoms", async (AppDbContext db) =>
{
    var symptoms = await db.PhysicalSymptoms
        .OrderBy(symptom => symptom.Name)
        .Select(symptom => new
        {
            symptom.PhysicalSymptomId,
            symptom.Name
        })
        .ToListAsync();

    return Results.Ok(symptoms);
});

app.Run();