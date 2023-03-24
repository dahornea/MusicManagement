using Microsoft.EntityFrameworkCore;

namespace Model;

    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
    {
    }
    public DbSet<Album> Album {get; set; } = null!;
    public DbSet<Artist> Artist {get; set; } = null!;
    public DbSet<RecordLabel> RecordLabel {get; set; } = null!;

}