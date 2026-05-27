namespace DIS.ApiTwo.DTO;

public class SymptomSearchResultDTO
{
    public int DailyLogId { get; set; }
    public DateTime Date { get; set; }
    public int CycleDay { get; set; }
    public int CycleId { get; set; }

    public int PhysicalSymptomId { get; set; }
    public string PhysicalSymptomName { get; set; } = string.Empty;

    public int? FlowLevelId { get; set; }
    public string? FlowLevelName { get; set; }
}