using AlunosApi.Context;
using AlunosApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//coneção sql server default aqui mesmo
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("\\SQLEXPRESS;Initial Catalog=AlunosDemoDB;Integrated Security=True"));

//abilitando o cors
builder.Services.AddCors();


//Registro de serviço de uma entidade (Aluno)
builder.Services.AddScoped<IAlunoService,AlunosService>();

var app = builder.Build();



// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//registro do cors
app.UseCors(c =>
{
    c.WithOrigins("http://localhost:3000");
    c.AllowAnyMethod();
    c.AllowAnyHeader();
});


app.UseHttpsRedirection();

app.UseRouting();//mapeamento

app.UseAuthorization();

app.MapControllers();

app.Run();
