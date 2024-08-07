using Domain.Entity;
using Domain.StoredProcedure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.ProgrammingLanguage)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.ProgrammingLanguageId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<EmployeeSpResult>()
            .HasNoKey()
            .ToView(null);
        
        modelBuilder.Entity<DepartmentSpResult>()
            .HasNoKey()
            .ToView(null);
        
        modelBuilder.Entity<ProgrammingLanguageSpResult>()
            .HasNoKey()
            .ToView(null);
        
        base.OnModelCreating(modelBuilder);
    }
}