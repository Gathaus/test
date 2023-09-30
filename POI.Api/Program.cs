using POI.Api.Configurations;
using POI.Api.Middlewares;
using POI.Application;
using POI.Infrastructure;
using POI.Infrastructure.Ef;
using POI.Infrastructure.Hangfire;
using POI.Infrastructure.MessageBus.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configure Serilog
LoggingConfiguration.ConfigureSerilog(builder.Configuration);

builder.Host.UseSerilog();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();

// Add Swagger
builder.Services.AddCustomSwagger();

//External Api Base Url
builder.Services.AddHttpClient();

// Add CORS
builder.Services.AddCustomCors();
var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ");
builder.Services.ConfigureMassTransit(rabbitMqConfig["Url"]);

builder.Services.AddApplication();
builder.Services.AddInfrastructureEf(configuration.GetConnectionString("SqlServer"));
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poi v1");
        c.SwaggerEndpoint("/swagger/experimental/swagger.json", "Experimental Api");
    });
}
    // Add Hangfire
        // builder.Services.AddHangfireServices(configuration.GetConnectionString("SqlServer"));
    //hangfire dashboard
        // app.UseCustomHangfireDashboard();
// app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();



app.UseEndpoints(endpoints => { endpoints.MapHealthChecks("/api/v1/healthcheck"); });

app.MapControllers();

app.Run();

Log.CloseAndFlush();