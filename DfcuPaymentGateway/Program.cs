using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DfcuPaymentGateway.Data;
using Microsoft.Extensions.Configuration;
using DfcuPaymentGateway.Services; // Ensure this is included  

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
// Configure Entity Framework Core with SQL Server  
builder.Services.AddDbContext<PaymentsDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentsDatabase")));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
