namespace DIS.ApiTwo.CycleTracker.Models;

public class FlowLevel
{
    public int FlowLevelId { get; set; }

    public string Amount { get; set; } = string.Empty;

    public List<DailyLog> DailyLogs { get; set; } = new();
}