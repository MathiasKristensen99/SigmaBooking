using SigmaBooking.Core.IServices;
using SigmaBooking.Domain.IRepositories;
using SigmaBooking.Domain.Services;
using SigmaBooking.MongoDB;
using SigmaBooking.MongoDB.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SigmaBookingDatabaseSettings>(builder.Configuration.GetSection("SigmaBookingDatabase"));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<ITableLayoutService, TableLayoutService>();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddSingleton<ITableRepository, TableRepository>();
builder.Services.AddSingleton<ITableLayoutRepository, TableLayoutRepository>();

builder.Services.AddCors(options => options
    .AddPolicy("dev-policy", policyBuilder =>
        policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("dev-policy");
}

app.UseCors("dev-policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();