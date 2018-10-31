using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperDiet.Models;

namespace SuperDiet.Models
{
    public class SuperDietContext : DbContext
    {
        public SuperDietContext (DbContextOptions<SuperDietContext> options)
            : base(options)
        {
        }

        public SuperDietContext() :base() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemOrder>()
                .HasKey(po => new { po.ItemID, po.OrderID });
        }

        public DbSet<SuperDiet.Models.Item> Item { get; set; }

        public DbSet<SuperDiet.Models.Order> Order { get; set; }

        public DbSet<SuperDiet.Models.ItemOrder> ItemOrder { get; set; }

        public DbSet<SuperDiet.Models.User> User { get; set; }

        public DbSet<SuperDiet.Models.Branch> Branch { get; set; }
    }
}
