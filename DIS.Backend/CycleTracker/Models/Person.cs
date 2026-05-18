namespace DIS.ApiTwo.CycleTracker.Models;

public class Person
{
    public int PersonId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateTime Birthdate { get; set; }

    public List<Cycle> Cycles { get; set; } = new();
}