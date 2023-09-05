using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Insfrastructure.Persistance
{
    public class IdentityUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Userdetail> Userdetail { get; set; } = null!;
    }
}
