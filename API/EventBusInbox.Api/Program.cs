using EventBusInbox.TypeConverters.Extensions;
using EventBusInbox.Repositories.Extensions;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Handlers.Extensions;
using EventBusInbox.Api.Middlewares;
using Serilog;
using EventBusInbox.Workers.Extensions;
using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureEnvironmentSettings();
builder.ConfigureAppSerilog();
builder.Services.ConfigureAppAutoMapper();
builder.Services.ConfigureAppRepositories();
builder.Services.ConfigureAppMediator();
builder.Services.ConfigureWorkers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
});

builder.Services.ConfigureAppVersioning();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    });
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.Run();
