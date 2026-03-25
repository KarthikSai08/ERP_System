using ERP_System.API.Filters;
using ERP_System.API.Presentation;
using ERP_System.Application.DependencyInjection;
using ERP_System.Infrastructure.DependencyInjection;
using ERP_System.Infrastructure.Persistence.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Infrastructure and Application project DI
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<DapperContext>();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
    cfg.AddMaps(typeof(ApplicationServices).Assembly);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ERP System API — CQRS + MediatR",
        Version = "v1",
        Description = "Modules: Products, Inventory, Orders, Customers, Suppliers, Employees"
    });

    c.OperationFilter<IdempotencyHeaderOperationFilter>();
});

builder.Services.AddCors(o =>
    o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<IdempotencyMiddleware>();

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
