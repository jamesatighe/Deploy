using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Deploy.DAL
{
    public class DeployDBContextFactory : IDesignTimeDbContextFactory<DeployDBContext>
    {
        DeployDBContext IDesignTimeDbContextFactory<DeployDBContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeployDBContext>();
            string connection = "Server=tcp:tighesql.database.windows.net,1433;Initial Catalog=deployment;Persist Security Info=False;User ID=sqladmin;Password=Naughty0lymp1@.1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connection);

            return new DeployDBContext(optionsBuilder.Options);
        }

    }
}
