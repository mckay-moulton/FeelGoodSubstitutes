using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeelGoodSubstitutes.Models
{
    public class MyDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public MyDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            //options.UseMySql(Configuration.GetConnectionString("MoviesDbConnection"));
            options.UseMySql(Configuration.GetConnectionString("MyDbConnection"), new MySqlServerVersion(new Version(8, 0, 28)));
        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
