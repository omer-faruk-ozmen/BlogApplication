using BlogApplication.Api.Application.Extensions;
using BlogApplication.Api.Application.Interfaces.Services;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Api.WebApi.Configurations.ColumnWriters;
using BlogApplication.Api.WebApi.Extensions;
using BlogApplication.Api.WebApi.Infrastructure.ActionFilters;
using BlogApplication.Infrastructure.Persistence.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>

    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()

));

builder.Services.AddHttpContextAccessor();



Logger log = new LoggerConfiguration()
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("BlogAppLogDb"), "Logs", needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"Message",new RenderedMessageColumnWriter()},
            {"MessageTemplate",new MessageTemplateColumnWriter()},
            {"Level",new LevelColumnWriter()},
            {"TimeStamp",new TimestampColumnWriter()},
            {"Exception",new ExceptionColumnWriter()},
            {"LogEvent", new LogEventSerializedColumnWriter()},
            {"Username",new UsernameColumnWriter()}
        })
    //.WriteTo.File("logs/log.txt")
    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("ShoppingAppLog");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services
    .AddControllers(opt => opt.Filters.Add<ValidationModelStateFilter>())
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation().ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddApplicationRegistration();

builder.Services.AddPersistenceRegistration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;

    LogContext.PushProperty("Username", username);


    await next();
});

app.MapControllers();

app.Run();
