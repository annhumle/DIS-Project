namespace DIS.ApiTwo.DTO;

public class CycleDTO
{
    public int CycleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class FlowLevelDTO
{
    public int FlowLevelId { get; set; }
    public string LevelName { get; set;} = string.Empty;
}