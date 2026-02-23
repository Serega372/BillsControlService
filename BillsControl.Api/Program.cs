using System.Text;
using BillsControl.Api.CustomMiddlewares;
using BillsControl.Api.Extensions;
using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Abstract.Auth;
using BillsControl.Infrastructure.Repositories;
using BillsControl.ApplicationCore.Services;
using BillsControl.Infrastructure.AuthHelpers;
using BillsControl.Infrastructure.Migrations;
using BillsControl.Infrastructure.TypeHandlers;
using Dapper;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Versioning;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.AspNetCore.CookiePolicy;

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPassword};Database={dbName}";

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddSingleton(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore();
builder.Services.ConfigureRunner(rb => rb
    .AddPostgres()
    .WithGlobalConnectionString(connectionString)
    .ScanIn(typeof(InitMigration).Assembly).For.All());

builder.Services.AddScoped<IPersonalBillsRepository, PersonalBillsRepository>();
builder.Services.AddScoped<IResidentsRepository, ResidentsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPersonalBillsService, PersonalBillsService>();
builder.Services.AddScoped<IResidentsService, ResidentsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IVersionTableMetaData, CustomVersionTableMetaData>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.MapControllers();

await app.RunAsync();
