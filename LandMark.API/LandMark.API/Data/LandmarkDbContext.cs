using LandMark.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandMark.API.Data
{
    /// <summary>
    /// This is the DB context class
    /// It is used to initialize the database
    /// </summary>
    public class LandmarkDbContext : DbContext
    {
        public LandmarkDbContext(DbContextOptions<LandmarkDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// This DB set property corresponds to User Notes table
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// This method is used to initialize the database and seed it with sample data
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Note>().HasData(new Note()
            {
                Id = Guid.NewGuid(),
                User = "Surry hills user",
                Content = "Welcome to Surry hills Sydney",
                Latitude = 33.8861,
                Longitude = 151.2111,
                Address = "Surry hills Sydney"
            },
            new Note()
            {
                Id = Guid.NewGuid(),
                User = "Town hall user",
                Content = "Welcome to town hall Sydney",
                Latitude = 33.8732,
                Longitude = 151.2061,
                Address = "Sydney Town Hall"
            },
            new Note()
            {
                Id = Guid.NewGuid(),
                User = "Circular Quay user",
                Content = "Welcome to Circular Quay Sydney",
                Latitude = 33.8611,
                Longitude = 151.2126,
                Address = "Circular Quay Sydney"

            }
            );
        }

    }
}
