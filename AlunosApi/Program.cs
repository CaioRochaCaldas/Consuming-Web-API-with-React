using AlunosApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//coneção sql server default aqui mesmo
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=LAPTOP-6KK5BAFI\\SQLEXPRESS;Initial Catalog=AlunosDemoDB;Integrated Security=True"));



var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
