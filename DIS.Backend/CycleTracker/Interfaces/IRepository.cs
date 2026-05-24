using DIS.ApiTwo.CycleTracker.Models;
using DIS.ApiTwo.DTO;

namespace DIS.ApiTwo.CycleTracker.Interfaces
{
    public interface ICycleTrackerRepository
    {
        Task<List<Cycle>> GetAllCycles();
        Task<List<FlowLevel>> GetAllFlowLevels();
        Task<List<PhysicalSymptom>> GetAllPhysicalSymptom();
        Task<List<DailyLog>> GetLogsByCycleId(int cycleId);
        Task<DailyLog> CreateDailyLog(DailyLog dailyLog, List<int> symptomIds);
        Task<DailyLog?> UpdateDailyLog(int dailyLogId, DailyLog dailyLog, List<int> symptomIds);
    }
}