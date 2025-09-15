using Microsoft.OpenApi.Models;
using Million.RealEstate.Backend.Api.Filters;
using Million.RealEstate.Backend.Api.Middleware;
using Million.RealEstate.Backend.Application.Interfaces;
using Million.RealEstate.Backend.Application.Mapping;
using Million.RealEstate.Backend.Application.Services;
using Million.RealEstate.Backend.Infrastructure.Data;
using Million.RealEstate.Backend.Infrastructure.Helpers;
using Million.RealEstate.Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Million Real Estate API",
        Version = "v1",
        Description = "APi prueba tecnica Million"
    });
});

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<PruebaMillionContext>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
builder.Services.AddScoped<IPropertyTraceService, PropertyTraceService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<ValidationActionFilter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3000/properties/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
