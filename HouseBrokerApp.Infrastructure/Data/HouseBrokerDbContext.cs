using HouseBrokerApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBrokerApp.Infrastructure.Data
{
    public  class HouseBrokerDbContext:IdentityDbContext
    {
        public HouseBrokerDbContext(DbContextOptions<HouseBrokerDbContext> options):base(options)
        {
            
        }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });
        }
    }
}
