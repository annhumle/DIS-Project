namespace DIS.ApiTwo.Models;

public class Cycle
{
    public int CycleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PersonId { get; set; }
    public List<DailyLog> DailyLogs { get; set; } = new();
}