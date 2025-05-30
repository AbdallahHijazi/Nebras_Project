using Microsoft.IdentityModel.Tokens;
using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Schools;
using NebrasProjectModels.Models.SchoolStatues;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDBContext>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<School>, SchoolRepository>();
builder.Services.AddScoped<IRepository<Governorate>, GovernorateRepository>();
builder.Services.AddScoped<IRepository<City>, CitiesRepository>();
builder.Services.AddScoped<IRepository<SchoolStatus>, SchoolStatusRepository>();
builder.Services.AddScoped<GovernorateRepository>();
builder.Services.AddScoped<CitiesRepository>();
builder.Services.AddScoped<SchoolRepository>();
builder.Services.AddScoped<SchoolStatusRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audiance"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                                                    builder.Configuration["Authentication:SecretKey"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        //بسم الله

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.NebrasProjectRepository
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
