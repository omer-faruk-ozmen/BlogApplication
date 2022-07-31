using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogApplication.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogAppDbContext>
    {
        public BlogAppDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<BlogAppDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
