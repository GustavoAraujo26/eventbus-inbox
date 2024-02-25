using EventBusInbox.TypeConverters.Extensions;
using EventBusInbox.Repositories.Extensions;
using EventBusInbox.Shared.Extensions;
using Microsoft.OpenApi.Models;
using EventBusInbox.Handlers.Extensions;
using EventBusInbox.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureAppAutoMapper();
builder.Services.ConfigureAppRepositories();
builder.Services.ConfigureEnvironmentSettings();
builder.Services.ConfigureAppMediator();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Bus Inbox API",
        Description = "An .NET Core API to act like a inbox for event bus, on microsservices environment.",
    });

    // using System.Reflection;
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
