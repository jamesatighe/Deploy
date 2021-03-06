﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

        public DbSet<DeployChoices> DeployChoices { get; set; }
        public DbSet<DeployList> DeployList { get; set; }

        public DbSet<Queue> Queue { get; set; }

    }
}
