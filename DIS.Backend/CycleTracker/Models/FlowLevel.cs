namespace DIS.ApiTwo.CycleTracker.Models;

public class FlowLevel
{
    public int FlowLevelId { get; set; }

    public string LevelName { get; set; } = string.Empty;

    public List<DailyLog> DailyLogs { get; set; } = new();
}