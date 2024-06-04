using Microsoft.EntityFrameworkCore;

/* 
 
 question: How to generatedb SQL FROM Model in visual studio 2022 package manger console
 Commends:
 1) Add-Migration InitialCreate
 2) update-Database
 */


// Opertion On DBSET Collection will be reflected to the DB Without SQL Sytax
namespace GradesMaterAPI.DB
{
    public class GradeMasterDbContext: DbContext
    {
        
        public GradeMasterDbContext()
        {

        }

        public GradeMasterDbContext(DbContextOptions<GradeMasterDbContext> options) : base(options) { }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\\\ProjectModels;Initial Catalog=GradeMasterdb_EFDb;Integrated Security=True;Connect Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
            
        }

        // Related Teacher Table
        public DbSet<DbModels.Teacher> Teachers { get; set; }
        // Related Students Table
        public DbSet<DbModels.Student> Student { get; set; }
        // Related Courses Table
        public DbSet<DbModels.Course> Courses { get; set; }

    }
}
