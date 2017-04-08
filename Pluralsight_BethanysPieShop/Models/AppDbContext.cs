using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight_BethanysPieShop.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        //connnection properties between the database and the models
        public DbSet<Category> Categories { get; set; }

        public DbSet<Pie> Pies { get; set; }
    }
}
