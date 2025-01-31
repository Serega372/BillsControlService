using BillsControl.Core;
using BillsControl.DataAccess.SQLite;
using BillsControl.DataAccess.SQLite.Repositories;
using BillsControl.Application;
using Microsoft.EntityFrameworkCore;
using BillsControl.Application.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
//    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(
    options =>
    {
        options.UseSqlite(configuration.GetConnectionString(nameof(DatabaseContext)));
    });

builder.Services.AddScoped<IPersonalBillsService, PersonalBillsService>();
builder.Services.AddScoped<IPersonalBillsRepository, PersonalBillsRepository>();
builder.Services.AddScoped<IResidentsService, ResidentsService>();
builder.Services.AddScoped<IResidentsRepository, ResidentsRepository>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
