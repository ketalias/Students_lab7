using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Students_lab7;
using Microsoft.EntityFrameworkCore;


namespace Students_lab7
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<ClubRegister> ClubRegisters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClubRegister>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Club)
                .HasForeignKey(s => s.ClubId);

            base.OnModelCreating(modelBuilder);
        }

        public UniversityContext(DbContextOptions<UniversityContext> options)
    : base(options)
        {


        }
    }
}

