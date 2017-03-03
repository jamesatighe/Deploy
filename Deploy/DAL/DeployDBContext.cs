using Deploy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.DAL
{
    public class DeployDBContext : DbContext
    {
        
        public DeployDBContext(DbContextOptions<DeployDBContext> options)
            : base(options)
        {
        }
        
        public DbSet<Tennant> Tennants { get; set; }
        public DbSet<DeployType> DeployTypes { get; set; }
        public DbSet<TennantParam> TennantParams { get; set; }
        public DbSet<DeployParam> DeployParms { get; set; }


    }
}
