using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;

namespace PP.EntityFramework
{
    public class PPDbContext : DbContext
    {
        public DbSet<Angajati> Angajati { get; set; }
        public DbSet<Articole> Articole { get; set; }
        public DbSet<ProgrammerTask> ProgrammerTask { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<JobType> JobType { get; set; }
        public DbSet<ProgrammerProgress> ProgrammerProgress { get; set; }
        public DbSet<WorkLocation> WorkLocation { get; set; }
        public DbSet<Comenzi> Comenzi { get; set; }
        public DbSet<ArticleDetails> ArticleDetails { get; set; }
        public DbSet<Clienti> Clienti { get; set; }
        public DbSet<Stagiuni> Stagiuni { get; set; }

        public PPDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(p => p.UserID);
            modelBuilder.Entity<ProgrammerProgress>().HasKey(a => a.ProgrammerProgressID);
            modelBuilder.Entity<ProgrammerTask>().HasKey(a => a.ProgrammerTaskID);
            modelBuilder.Entity<WorkLocation>().HasKey(a => a.WorkLocationID);
            modelBuilder.Entity<Sector>().HasKey(a => a.SectorID);
            modelBuilder.Entity<JobType>().HasKey(a => a.JobTypeID);

            modelBuilder.Entity<Users>().Ignore(u => u.Id);
            modelBuilder.Entity<ProgrammerTask>().Ignore(u => u.Id);
            modelBuilder.Entity<ProgrammerTask>().Ignore(u => u.ArticleTitle);
            modelBuilder.Entity<ProgrammerProgress>().Ignore(u => u.ArticleTitle);
            modelBuilder.Entity<Sector>().Ignore(u => u.Id);
            modelBuilder.Entity<ProgrammerProgress>().Ignore(u => u.Id);
            modelBuilder.Entity<WorkLocation>().Ignore(u => u.Id);
            modelBuilder.Entity<JobType>().Ignore(u => u.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}