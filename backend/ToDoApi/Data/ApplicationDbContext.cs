using Microsoft.EntityFrameworkCore;
using ToDoApi.Entities;

namespace ToDoApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
}
