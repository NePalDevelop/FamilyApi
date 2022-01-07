using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace FamilyApi.Models
{
    public class FamilyApiContext : DbContext
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public FamilyApiContext(DbContextOptions<FamilyApiContext> options)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
            : base(options)
        {
        }

        public DbSet<Human> Humans { get; set; }
        public DbSet<Relation> Relations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(System.Configuration.ConfigurationManager.ConnectionStrings["FamilyApiContext"].ConnectionString);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Human>().ToTable("Human");
            modelBuilder.Entity<Relation>().ToTable("Relation");
            
            modelBuilder.Entity<Relation>()
                .HasOne(r => r.Human)
                .WithMany(h => h.Relations);

            modelBuilder.Entity<Relation>()
               .HasOne(r => r.Kin)
               .WithOne();
        }
    }
}
