using EventBusInbox.TypeConverters.Extensions;
using EventBusInbox.Repositories.Extensions;
using EventBusInbox.Shared.Extensions;
using EventBusInbox.Handlers.Extensions;
using EventBusInbox.Api.Middlewares;
using Serilog;
using EventBusInbox.Workers.Extensions;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureEnvironmentSettings();
builder.ConfigureAppSerilog();
builder.Services.ConfigureAppAutoMapper();
builder.Services.ConfigureAppRepositories();
builder.Services.ConfigureAppMediator();
builder.Services.ConfigureWorkers();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    // using System.Reflection;
    List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
});

builder.Services.ConfigureAppVersioning();

builder.Services.AddCors();

builder.WebHost.UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS"))
    .UseIISIntegration();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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

        options.RoutePrefix = string.Empty;
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
