using Microsoft.IdentityModel.Tokens;
using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.Photos;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<AppDBContext>();
builder.Services.AddScoped<IRepository<SchoolPhoto>, PhotosRepository>();
builder.Services.AddScoped<PhotosRepository>();
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
    };
});
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
