using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistance
{
    public class ProductDbContext:DbContext 
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> dbContextOptions) : base(dbContextOptions)
        {
            //try
            //{
            //    var databaseCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (databaseCreater != null)
            //    {
            //        //Create database if cannot connect
            //        if (!databaseCreater.CanConnect()) databaseCreater.Create();
            //        // create tables if there are no tables in the database 
            //        if (!databaseCreater.HasTables()) databaseCreater.CreateTables();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
        public DbSet<Product.domain.Entities.Product> Product { get; set; }
    }
}
