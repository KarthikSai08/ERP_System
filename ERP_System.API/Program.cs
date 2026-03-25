using ERP_System.API.Presentation;
using ERP_System.Infrastructure;
using ERP_System.Infrastructure.Persistence.Context;
using Scalar.AspNetCore;
using ERP_System.Application.DependencyInjection;
using  ERP_System.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication();
//builder.Services.AddApplication();
builder.Services.AddScoped<DapperContext>();

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
    cfg.AddMaps(typeof(ApplicationServices).Assembly);  // ← Also scan Application assembly
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ERP System API — CQRS + MediatR",
        Version = "v1",
        Description = "Modules: Products, Inventory, Orders, Customers, Suppliers, Employees"
    });
});
builder.Services.AddCors(o =>
    o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP System v1");
    c.RoutePrefix = "swagger"; // ← move swagger off root
});

app.MapScalarApiReference(options =>
{
    options.Title = "ERP System API";
    options.OpenApiRoutePattern = "/swagger/v1/swagger.json";
    options.Theme = ScalarTheme.DeepSpace;
});

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
