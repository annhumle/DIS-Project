using DIS.ApiTwo.CycleTracker.Interfaces;
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
}

