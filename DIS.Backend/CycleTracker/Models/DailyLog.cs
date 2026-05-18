namespace DIS.ApiTwo.Models;

public class DailyLog
{
    public int DailyLogId { get; set; }

    public DateTime Date { get; set; }

    public int CycleDay { get; set; }

    public int CycleId { get; set; }

    public Cycle Cycle { get; set; } = null!;

    public int? FlowLevelId { get; set; }

    public FlowLevel? FlowLevel { get; set; }

    public List<DailyLogSymptom> DailyLogSymptoms { get; set; } = new();
}