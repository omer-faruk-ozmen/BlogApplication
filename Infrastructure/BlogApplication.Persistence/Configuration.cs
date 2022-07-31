﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlogApplication.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                    "../../Presentation/BlogApplication.Presentation"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("BlogAppPostgres");
            }
        }
    }
}
