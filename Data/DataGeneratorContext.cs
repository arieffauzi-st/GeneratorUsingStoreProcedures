
using GeneratorUsingStoreProcedures.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneratorUsingStoreProcedures.Data;

    public class DataGeneratorContext : DbContext
    {
        public DataGeneratorContext(DbContextOptions<DataGeneratorContext> options) : base(options) { }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Personal> Personals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().ToTable("tblM_gender");
            modelBuilder.Entity<Hobby>().ToTable("tblM_hobby");
            modelBuilder.Entity<Personal>().ToTable("tblT_personal");

            modelBuilder.Entity<Gender>().HasIndex(g => g.Name).IsUnique();
            modelBuilder.Entity<Hobby>().HasIndex(h => h.Name).IsUnique();
        }
    }

