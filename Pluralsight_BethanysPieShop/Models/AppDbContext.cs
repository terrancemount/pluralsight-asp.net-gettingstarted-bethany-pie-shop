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

        //connect to the Categories table
        public DbSet<Category> Categories { get; set; }

        //connect to the Pies table  -- has foriegn key to the categories
        public DbSet<Pie> Pies { get; set; }  

        //connect to the Shopping Cart Item table
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
