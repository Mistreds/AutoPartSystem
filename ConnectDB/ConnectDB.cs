using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data
{
    public class ConnectDB : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public ConnectDB()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=spiriddp.beget.tech;user=spiriddp_auto_te;password=Escort123;database=spiriddp_auto_te;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Admin
            modelBuilder.Entity<Employee>(b => b.ToTable("Employee"));
            
            modelBuilder.Entity<Position>(b => b.ToTable("Position"));
            modelBuilder.Entity<Position>().HasData(new Position[] { new Position(1, "Администратор"), new Position(2, "Завсклад"), new Position(3, "Продажник"), new Position(4, "Продажник регионал") });
            modelBuilder.Entity<Employee>().HasData(new Employee[] {new Employee(1, "Администратор", "Admin", "Admin", 1) });

            #endregion
        }
    }
}
