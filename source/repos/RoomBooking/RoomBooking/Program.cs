using BuisnessLogics.BusinessLogics;
using BuisnessLogics.IBusinessLogics;
using BuisnessRepository.BusinessRepository;
using BuisnessRepository.IBusinessRepository;
using Data.NewFolder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RoomBookingDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Controllers")));
builder.Services.AddTransient<IUserLogics, UserLogics>();
builder.Services.AddTransient<IUserRepository,UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
