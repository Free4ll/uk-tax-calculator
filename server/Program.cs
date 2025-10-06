using Microsoft.EntityFrameworkCore;
using Tax.Api.Infrastructure;
using Tax.Api.Api;
using Tax.Api.Domain;
using FluentValidation;
using FluentValidation.AspNetCore;
using Tax.Api.Infrastructure.Interfaces;
using Tax.Api.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaxDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("TaxDb") ?? "Data Source=tax.db"));

builder.Services.AddScoped<ITaxBandRepository, TaxBandRepository>();
builder.Services.AddScoped<ITaxCalculator, ProgressiveTaxCalculator>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CalculateRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB exists and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaxDbContext>();
    await db.Database.EnsureCreatedAsync();
    await db.EnsureSeededAsync();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
