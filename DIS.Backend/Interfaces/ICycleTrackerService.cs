using DIS.ApiTwo.DTO;

namespace DIS.ApiTwo.Interfaces
{
    public interface ICycleTrackerService
    {
        Task<List<CycleDTO>> GetAllCycles();
        Task<List<FlowLevelDTO>> GetAllFlowLevels();
        Task<List<PhysicalSymptomDTO>> GetAllPhysicalSymptom();
        Task<List<DailyLogDTO>> GetLogsByCycleId(int cycleId);
        Task<DailyLogDTO> CreateDailyLog(CreateDailyLogDTO dto);
        Task<DailyLogDTO?> UpdateDailyLog(int dailyLogId, UpdateDailyLogDTO dto);
        Task<List<SymptomSearchResultDTO>> SearchDailyLogsBySymptomRegexPattern(string pattern);
    }
}
