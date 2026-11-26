namespace TaskMaster.Api.Models;

/// <summary>Slide 9 exposes DbSet&lt;User&gt;. Maps to dbo.Users: one User has many Tasks.</summary>
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<TaskItem> Tasks { get; set; } = new();
}
