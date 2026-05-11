namespace DIS.Backend.Models;

public class Cycle
{
    public int CycleNumber { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int PersonId { get; set; }

    public Person Person { get; set; } = null!;

    public List<DailyLog> DailyLogs { get; set; } = new();
}