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
            FROM flow_levels;
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

    public async Task<List<PhysicalSymptom>> GetAllPhysicalSymptom()
    {
        var physicalSymptom = new List<PhysicalSymptom>();

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        var sql = """
            SELECT physical_symptom_id, physical_symptom_name
            FROM physical_symptom;
        """;

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            physicalSymptom.Add(new PhysicalSymptom
            {
                PhysicalSymptomId = reader.GetInt32(0),
                PhysicalSymptomName = reader.GetString(1)
            });
        }

        return physicalSymptom;
    }

    public async Task<List<DailyLog>> GetLogsByCycleId(int cycleId)
    {
        var dailyLogs = new List<DailyLog>();

        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        var logsSql = """
            SELECT daily_log_id, date, cycle_day, cycle_id, flow_level_id
            FROM daily_logs
            WHERE cycle_id = @cycleId
            ORDER BY date;
        """;

        await using (var command = new NpgsqlCommand(logsSql, connection))
        {
            command.Parameters.AddWithValue("@cycleId", cycleId);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                dailyLogs.Add(new DailyLog
                {
                    DailyLogId = reader.GetInt32(0),
                    Date = reader.GetDateTime(1),
                    CycleDay = reader.GetInt32(2),
                    CycleId = reader.GetInt32(3),
                    FlowLevelId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                });
            }
        }

        if (dailyLogs.Count == 0) return dailyLogs;

        var logsById = dailyLogs.ToDictionary(log => log.DailyLogId);

        var symptomsSql = """
            SELECT dls.daily_log_id, dls.physical_symptom_id
            FROM daily_log_symptoms dls
            JOIN daily_logs dl ON dls.daily_log_id = dl.daily_log_id
            WHERE dl.cycle_id = @cycleId;
        """;

        await using (var command = new NpgsqlCommand(symptomsSql, connection))
        {
            command.Parameters.AddWithValue("@cycleId", cycleId);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var dailyLogId = reader.GetInt32(0);
                var physicalSymptomId = reader.GetInt32(1);

                if (logsById.TryGetValue(dailyLogId, out var log))
                {
                    log.DailyLogSymptoms.Add(new DailyLogSymptom
                    {
                        DailyLogId = dailyLogId,
                        PhysicalSymptomId = physicalSymptomId
                    });
                }
            }
        }

        return dailyLogs;
    }

    public async Task<DailyLog> CreateDailyLog(DailyLog dailyLog, List<int> symptomIds)
    {
        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        var insertLogSql = """
            INSERT INTO daily_logs (date, cycle_day, cycle_id, flow_level_id)
            VALUES (@date, @cycleDay, @cycleId, @flowLevelId)
            RETURNING daily_log_id;
        """;

        int newId;

        await using (var command = new NpgsqlCommand(insertLogSql, connection, transaction))
        {
            command.Parameters.AddWithValue("@date", dailyLog.Date);
            command.Parameters.AddWithValue("@cycleDay", dailyLog.CycleDay);
            command.Parameters.AddWithValue("@cycleId", dailyLog.CycleId);
            command.Parameters.AddWithValue("@flowLevelId", (object?)dailyLog.FlowLevelId ?? DBNull.Value);

            newId = (int)(await command.ExecuteScalarAsync())!;
        }

        dailyLog.DailyLogId = newId;

        await InsertSymptomLinks(connection, transaction, newId, symptomIds);

        foreach (var symptomId in symptomIds)
        {
            dailyLog.DailyLogSymptoms.Add(new DailyLogSymptom
            {
                DailyLogId = newId,
                PhysicalSymptomId = symptomId
            });
        }

        await transaction.CommitAsync();

        return dailyLog;
    }

    public async Task<DailyLog?> UpdateDailyLog(int dailyLogId, DailyLog dailyLog, List<int> symptomIds)
    {
        await using var connection = _database.CreateConnection();
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        var updateSql = """
            UPDATE daily_logs
            SET date = @date,
                cycle_day = @cycleDay,
                flow_level_id = @flowLevelId
            WHERE daily_log_id = @dailyLogId
            RETURNING cycle_id;
        """;

        int? cycleId;

        await using (var command = new NpgsqlCommand(updateSql, connection, transaction))
        {
            command.Parameters.AddWithValue("@date", dailyLog.Date);
            command.Parameters.AddWithValue("@cycleDay", dailyLog.CycleDay);
            command.Parameters.AddWithValue("@flowLevelId", (object?)dailyLog.FlowLevelId ?? DBNull.Value);
            command.Parameters.AddWithValue("@dailyLogId", dailyLogId);

            var result = await command.ExecuteScalarAsync();
            cycleId = result is int id ? id : null;
        }

        if (cycleId is null)
        {
            await transaction.RollbackAsync();
            return null;
        }

        var deleteSql = "DELETE FROM daily_log_symptoms WHERE daily_log_id = @dailyLogId;";

        await using (var command = new NpgsqlCommand(deleteSql, connection, transaction))
        {
            command.Parameters.AddWithValue("@dailyLogId", dailyLogId);
            await command.ExecuteNonQueryAsync();
        }

        await InsertSymptomLinks(connection, transaction, dailyLogId, symptomIds);

        await transaction.CommitAsync();

        dailyLog.DailyLogId = dailyLogId;
        dailyLog.CycleId = cycleId.Value;
        dailyLog.DailyLogSymptoms = symptomIds
            .Select(id => new DailyLogSymptom { DailyLogId = dailyLogId, PhysicalSymptomId = id })
            .ToList();

        return dailyLog;
    }

    private static async Task InsertSymptomLinks(
        NpgsqlConnection connection,
        NpgsqlTransaction transaction,
        int dailyLogId,
        List<int> symptomIds)
    {
        if (symptomIds.Count == 0) return;

        var sql = """
            INSERT INTO daily_log_symptoms (daily_log_id, physical_symptom_id)
            VALUES (@dailyLogId, @physicalSymptomId);
        """;

        foreach (var symptomId in symptomIds)
        {
            await using var command = new NpgsqlCommand(sql, connection, transaction);
            command.Parameters.AddWithValue("@dailyLogId", dailyLogId);
            command.Parameters.AddWithValue("@physicalSymptomId", symptomId);
            await command.ExecuteNonQueryAsync();
        }
    }
}


