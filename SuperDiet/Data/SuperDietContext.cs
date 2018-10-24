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

        public DbSet<SuperDiet.Models.Item> Item { get; set; }

        public DbSet<SuperDiet.Models.Order> Order { get; set; }

        public DbSet<SuperDiet.Models.User> User { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Item>().ToTable("Items");
        //    modelBuilder.Entity<Order>().ToTable("Orders");
        //    modelBuilder.Entity<User>().ToTable("Users");
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string cn = @"Server=.\SQLEXPRESS;Database=test-db;User Id= . . .";
        //    optionsBuilder.UseSqlServer(cn);

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
