using BillsControl.DataAccess.SQLite;
using BillsControl.DataAccess.SQLite.Repositories;
using Microsoft.EntityFrameworkCore;
using BillsControl.Application.Services;
using BillsControl.Core.Abstract;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();
