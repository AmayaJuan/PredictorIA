using Microsoft.EntityFrameworkCore;
using Predictor.Entities;
using System.Reflection.Emit;

namespace Predictor.Repositories.Data;

// Database context: tables, relationships and automatic audit stamping
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Prediction> Predictions => Set<Prediction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Team>(e =>
        {
            e.Property(t => t.Name).HasMaxLength(80).IsRequired();
            e.Property(t => t.City).HasMaxLength(60).IsRequired();
            e.HasIndex(t => new { t.Sport, t.Name }).IsUnique();
        });

        builder.Entity<Match>(e =>
        {
            e.Property(m => m.HomeOdds).HasPrecision(5, 2);
            e.Property(m => m.DrawOdds).HasPrecision(5, 2);
            e.Property(m => m.AwayOdds).HasPrecision(5, 2);

            e.HasOne(m => m.HomeTeam).WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(m => m.AwayTeam).WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Prediction>(e =>
        {
            e.Property(p => p.ModelVersion).HasMaxLength(40).IsRequired();
            e.Property(p => p.ProbHome).HasPrecision(5, 4);
            e.Property(p => p.ProbDraw).HasPrecision(5, 4);
            e.Property(p => p.ProbAway).HasPrecision(5, 4);

            e.HasOne(p => p.Match).WithMany()
                .HasForeignKey(p => p.MatchId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        StampAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    // Sets CreatedDate on insert and ModifiedDate on update
    private void StampAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedDate = DateTime.UtcNow;
            else if (entry.State == EntityState.Modified)
                entry.Entity.ModifiedDate = DateTime.UtcNow;
        }
    }
}
