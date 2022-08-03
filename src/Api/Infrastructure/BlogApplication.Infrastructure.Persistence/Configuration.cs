using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlogApplication.Infrastructure.Persistence
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../WebApi/BlogApplication.Api.WebApi"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("BlogApplicationPostgresDb");
            }
        }
    }
}
