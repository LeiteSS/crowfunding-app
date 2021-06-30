using System;
using Microsoft.EntityFrameworkCore;
using Vaquinha.App.Entities;
using Vaquinha.App.Repository.Mapping;

namespace Vaquinha.App.Repository.Context
{
    public class CrowfundingOnlineDBContext : DbContext
    {
        public CrowfundingOnlineDBContext(DbContextOptions<CrowfundingOnlineDBContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Cause> Causes { get; set; }

        /*private const string connectionString = "server=localhost;port=3306;database=Crowfunding;user=ssl;password=03568799We";    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 3, 29)));
        }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
            modelBuilder.ApplyConfiguration(new DonationMapping());
            modelBuilder.ApplyConfiguration(new CauseMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}