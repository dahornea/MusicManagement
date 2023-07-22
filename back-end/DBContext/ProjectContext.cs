using Microsoft.EntityFrameworkCore;

namespace Model;

    public class ProjectContext : DbContext
    {
    public ProjectContext()
    {
    }
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
    {
    }
    public virtual DbSet<Album> Album {get; set; } = null!;
    public virtual DbSet<Artist> Artist {get; set; } = null!;
    public virtual DbSet<RecordLabel> RecordLabel {get; set; } = null!;
    public virtual DbSet<Certifications> Certifications {get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Many to many for Certifications
        modelBuilder.Entity<Certifications>()
            .HasKey(c => new { c.RecordLabelId, c.ArtistId });
        modelBuilder.Entity<Certifications>()
            .HasOne(c => c.RecordLabel)
            .WithMany(c => c.Certifications)
            .HasForeignKey(c => c.RecordLabelId);

        modelBuilder.Entity<Certifications>()
            .HasOne(c => c.Artist)
            .WithMany(c => c.Certifications)
            .HasForeignKey(c => c.ArtistId);

        //One to many for Album-RecordLabel
        modelBuilder.Entity<Album>()
            .HasOne(a => a.RecordLabel)
            .WithMany(r => r.Albums)
            .HasForeignKey(a => a.RecordLabelId);

        //One to many for Album-Artist
        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany(r => r.Albums)
            .HasForeignKey(a => a.ArtistId);
    }

}