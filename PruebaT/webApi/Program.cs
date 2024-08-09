using webApi.Repositories.Implementations;
using webApi.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using webApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//establece conexion a bd
builder.Services.AddDbContext<PruebaTecnicaContext>(
    options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DataBase"), new MySqlServerVersion(new Version(8, 0, 39)));
}
    );
builder.Services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
