namespace DIS.ApiTwo.DTO;

public class UpdateDailyLogDTO
{
    public DateTime Date { get; set; }
    public int CycleDay { get; set; }
    public int? FlowLevelId { get; set; }
    public List<int> SymptomIds { get; set; } = new();
}
