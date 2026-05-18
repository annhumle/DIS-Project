using DIS.ApiTwo.DTO;
using DIS.ApiTwo.Models;

namespace DIS.ApiTwo.CycleTracker.Interfaces
{
    public interface ICycleTrackerRepository
    {
        Task<List<Cycle>> GetAllCycles();
    }
}