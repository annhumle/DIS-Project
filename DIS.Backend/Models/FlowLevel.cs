namespace DIS.Backend.Models;

public class FlowLevel
{
    public int FlowId { get; set; }

    public string Amount { get; set; } = string.Empty;

    public List<DailyLog> DailyLogs { get; set; } = new();
}