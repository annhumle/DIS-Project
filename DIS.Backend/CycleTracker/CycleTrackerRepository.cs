using DIS.ApiTwo.CycleTracker.Interfaces;
using DIS.ApiTwo.CycleTracker.Models;
using DIS.ApiTwo.Data;
using Npgsql;

namespace DIS.ApiTwo.CycleTracker;

public class CycleTrackerRepository : ICycleTrackerRepository
{
    private readonly DatabaseConnection _database;

    public CycleTrackerRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public async Task<List<Cycle>> GetAllCycles()
    {
        var cycles = new List<Cycle>();

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        var sql = """
            SELECT cycle_id, start_date, end_date, person_id
            FROM cycles;
        """;

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            cycles.Add(new Cycle
            {
                CycleId = reader.GetInt32(0),
                StartDate = reader.GetDateTime(1),
                EndDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                PersonId = reader.GetInt32(3)
            });
        }

        return cycles;
    }

    public async Task<List<FlowLevel>> GetAllFlowLevels()
    {
        var flowLevels = new List<FlowLevel>();

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        var sql = """
            SELECT flow_level_id, level_name
            FROM flow_levels
        """;
        
        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            flowLevels.Add(new FlowLevel
            {
                FlowLevelId = reader.GetInt32(0),
                LevelName = reader.GetString(1)
            });
        }
        return flowLevels; 
    }
}

