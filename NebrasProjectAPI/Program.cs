using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDBContext>();
builder.Services.AddScoped<IRepository<Users>, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.NebrasProjectRepository
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
