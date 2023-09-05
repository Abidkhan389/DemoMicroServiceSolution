using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Persistance
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions)
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
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersDetails> OrdersDetails { get; set; }
    }
}
