namespace DIS.Backend.Models;

public class PhysicalSymptom
{
    public int SymptomId { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<DailyLog> DailyLogs { get; set; } = new();
}