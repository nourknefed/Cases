using Microsoft.EntityFrameworkCore;
using NOUR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOUR.Data
{
    public class SqlDbContext : DbContext
    {
        
            public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
            {

            }

            public DbSet<User> Users { get; set; }
        public DbSet<NOUR.models.Case> Case { get; set; }



    }
}
