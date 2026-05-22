using DIS.ApiTwo.DTO;

namespace DIS.ApiTwo.Interfaces
{
    public interface ICycleTrackerService
    {
        Task<List<CycleDTO>> GetAllCycles();
        Task<List<FlowLevelDTO>> GetAllFlowLevels();
        Task<List<PhysicalSymptomDTO>> GetAllPhysicalSymptom();
    }
}
