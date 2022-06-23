using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommercePortal.Models;

namespace ECommercePortal.Models
{
    public class EcommerceDbContext:DbContext
    {
        public EcommerceDbContext() { }
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> option) : base(option) { }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(@"Server=GHOST; Database=EcommerceProject; Trusted_Connection=True;");
        }
        public virtual DbSet<UpdatedRating> products { get; set; }
        public virtual DbSet<Show> cart { get; set; }
      
              
    }
}
