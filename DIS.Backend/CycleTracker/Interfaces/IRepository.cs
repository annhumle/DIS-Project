using DIS.ApiTwo.CycleTracker.Models;
using DIS.ApiTwo.DTO;

namespace DIS.ApiTwo.CycleTracker.Interfaces
{
    public interface ICycleTrackerRepository
    {
        Task<List<Cycle>> GetAllCycles();
        Task<List<FlowLevel>> GetAllFlowLevels();
        Task<List<PhysicalSymptom>> GetAllPhysicalSymptom();
    }
}