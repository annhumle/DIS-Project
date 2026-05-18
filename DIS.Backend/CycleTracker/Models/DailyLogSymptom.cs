namespace DIS.ApiTwo.CycleTracker.Models;

public class DailyLogSymptom
{
    public int DailyLogId { get; set; }

    public DailyLog DailyLog { get; set; } = null!;

    public int PhysicalSymptomId { get; set; }

    public PhysicalSymptom PhysicalSymptom { get; set; } = null!;
}