using DIS.ApiTwo.CycleTracker.Interfaces;
using DIS.ApiTwo.CycleTracker.Models;
using DIS.ApiTwo.DTO;
using DIS.ApiTwo.Interfaces;

namespace DIS.ApiTwo.CycleTracker;

public class CycleTrackerService : ICycleTrackerService
{
    private readonly ICycleTrackerRepository _repository;

    public CycleTrackerService(ICycleTrackerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CycleDTO>> GetAllCycles()
    {
        var cycles = await _repository.GetAllCycles();

        return cycles.Select(cycle => new CycleDTO
        {
            CycleId = cycle.CycleId,
            StartDate = cycle.StartDate,
            EndDate = cycle.EndDate
        }).ToList();
    }

    public async Task<List<FlowLevelDTO>> GetAllFlowLevels()
    {
        var flowLevels = await _repository.GetAllFlowLevels();

        return flowLevels.Select(flowLevel => new FlowLevelDTO
        {
            FlowLevelId = flowLevel.FlowLevelId,
            LevelName = flowLevel.LevelName,
        }).ToList();
    }

    public async Task<List<PhysicalSymptomDTO>> GetAllPhysicalSymptom()
    {
        var physicalSymptom = await _repository.GetAllPhysicalSymptom();

        return physicalSymptom.Select(symptom => new PhysicalSymptomDTO
        {
            PhysicalSymptomId = symptom.PhysicalSymptomId,
            PhysicalSymptomName = symptom.PhysicalSymptomName
        }).ToList();
    }

    public async Task<List<DailyLogDTO>> GetLogsByCycleId(int cycleId)
    {
        var dailyLogs = await _repository.GetLogsByCycleId(cycleId);

        return dailyLogs.Select(ToDto).ToList();
    }

    public async Task<DailyLogDTO> CreateDailyLog(CreateDailyLogDTO dto)
    {
        var dailyLog = new DailyLog
        {
            Date = dto.Date,
            CycleDay = dto.CycleDay,
            CycleId = dto.CycleId,
            FlowLevelId = dto.FlowLevelId
        };

        var created = await _repository.CreateDailyLog(dailyLog, dto.SymptomIds);

        return ToDto(created);
    }

    public async Task<DailyLogDTO?> UpdateDailyLog(int dailyLogId, UpdateDailyLogDTO dto)
    {
        var dailyLog = new DailyLog
        {
            Date = dto.Date,
            CycleDay = dto.CycleDay,
            FlowLevelId = dto.FlowLevelId
        };

        var updated = await _repository.UpdateDailyLog(dailyLogId, dailyLog, dto.SymptomIds);

        return updated is null ? null : ToDto(updated);
    }

    private static DailyLogDTO ToDto(DailyLog log)
    {
        return new DailyLogDTO
        {
            DailyLogId = log.DailyLogId,
            Date = log.Date,
            CycleDay = log.CycleDay,
            CycleId = log.CycleId,
            FlowLevelId = log.FlowLevelId,
            SymptomIds = log.DailyLogSymptoms.Select(s => s.PhysicalSymptomId).ToList()
        };
    }
}

