using Microsoft.EntityFrameworkCore;
using DomainModel.Models;
using TechnoLab.CommonLib.EntityFramework.Persistence;

namespace DataAccess
{
    public partial class studentsContext : AuditDbContext
    {
        public studentsContext(DbContextOptions nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=.;Database=students;User ID=sa;Password=sa123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.PkDepartmentId)
                    .HasName("PK__departme__434DFAABA1F5C148");
            });
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<StudentsInfo> StudentsInfo { get; set; }
    }
}