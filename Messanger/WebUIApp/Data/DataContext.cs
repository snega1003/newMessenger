using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiApp.Models;

namespace ApiApp.Data
{
    public class DataContext : DbContext
    {

        private static bool _created = false;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            if (!_created)
            {
                Database.Migrate();
                _created = true;
            }
        }
        public DbSet<Message> Message { get; set; }
        public DbSet<Contact> UserContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property<string>("ExtendedDataStr")
                .HasField("_extendedData");
        }

    }
}
