using Microsoft.EntityFrameworkCore;
using Models;
using PatientService.Repositories;
using FeatureHubSDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Feature Hub
var fhConfig = new EdgeFeatureHubConfig("http://localhost:8085", Environment.GetEnvironmentVariable("FH_SDK_KEY"));
builder.Services.AddSingleton<IClientContext>(await fhConfig.NewContext().Build());

// Db and repository for controller(s)
builder.Services.AddDbContext<BtpDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("MySQLDbConnection");
    options.UseMySQL(connectionString ?? throw new NullReferenceException("MySQLDbConnection not supplied in environment variables."));
});

builder.Services.AddTransient<IPatientRepository, PatientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
