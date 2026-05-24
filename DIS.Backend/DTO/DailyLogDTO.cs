namespace DIS.ApiTwo.DTO;

public class DailyLogDTO
{
    public int DailyLogId { get; set; }
    public DateTime Date { get; set; }
    public int CycleDay { get; set; }
    public int CycleId { get; set; }
    public int? FlowLevelId { get; set; }
    public List<int> SymptomIds { get; set; } = new();
}
