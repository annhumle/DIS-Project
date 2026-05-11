namespace DIS.Backend.Models;

public class DailyLog
{
    public int LogId { get; set; }

    public DateTime Date { get; set; }

    public int CycleDay { get; set; }

    public int CycleId { get; set; }

    public Cycle Cycle { get; set; } = null!;
    
    public FlowLevel? FlowLevel { get; set; }

    public List<PhysicalSymptom> PhysicalSymptoms { get; set; } = new();
}