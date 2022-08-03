using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApplication.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services)
    {
        services.AddDbContext<BlogApplicationContext>(options =>
        {

            options.UseNpgsql(Configuration.ConnectionString);
        });

        //var seedData = new SeedData();
        //seedData.SeedAsync().GetAwaiter().GetResult();

        return services;
    }
}