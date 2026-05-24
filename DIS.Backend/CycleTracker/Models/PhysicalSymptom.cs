namespace DIS.ApiTwo.CycleTracker.Models;

public class PhysicalSymptom
{
    public int PhysicalSymptomId { get; set; }

    public string PhysicalSymptomName { get; set; } = string.Empty;

    public List<DailyLogSymptom> DailyLogSymptoms { get; set; } = new();
}