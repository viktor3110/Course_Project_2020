using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskRental.Data
{
    public class DiskRentalContext : DbContext
    {
        public DiskRentalContext()
        {

        }

        public DiskRentalContext(DbContextOptions<DiskRentalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Actor> Actors { get; set; }
        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.ClientDisk> ClientsDisks { get; set; }
        public DbSet<Models.Country> Countries { get; set; }
        public DbSet<Models.Disk> Disks { get; set; }
        public DbSet<Models.DiskType> DiskTypes { get; set; }
        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.FilmGenre> FilmGenres { get; set; }
        public DbSet<Models.Manufacturer> Manufacturers { get; set; }
        public DbSet<Models.Position> Positions { get; set; }
    }
}
