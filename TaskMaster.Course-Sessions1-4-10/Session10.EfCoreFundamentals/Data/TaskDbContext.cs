using Microsoft.EntityFrameworkCore;
using TaskMaster.Api.Models;

namespace TaskMaster.Api.Data;

/// <summary>
/// Slides 8-9. DbContext is EF Core's gateway to the database: one instance = one unit of work.
/// It exposes a DbSet per table, tracks the entities it loads, and sends SQL when SaveChangesAsync runs.
///
/// Registered as SCOPED (AddDbContext does this): one instance per HTTP request. Never make it a singleton.
/// </summary>
public class TaskDbContext : DbContext
{
    // DI supplies the options (provider + connection string) that Program.cs configured.
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<User> Users => Set<User>();

    // Conventions already give us most of the model (Id = primary key, property names = column names).
    // Here we only state what conventions cannot guess. This is the same shape as the Session 9 SQL tables.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(task =>
        {
            task.ToTable("Tasks");

            task.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);          // NVARCHAR(200) NOT NULL

            task.Property(t => t.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");   // used only if the code leaves CreatedAt unset

            task.HasIndex(t => t.Status);    // we filter by status often (Session 9: indexes)

            // One User has many Tasks. If a user is deleted, their tasks stay but become unassigned.
            task.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("Users");
            user.Property(u => u.Name).IsRequired().HasMaxLength(100);
        });
    }
}
