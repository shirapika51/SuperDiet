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

        public DbSet<SuperDiet.Models.Item> Item { get; set; }

        public DbSet<SuperDiet.Models.Order> Order { get; set; }

        public DbSet<SuperDiet.Models.User> User { get; set; }
    }
}
