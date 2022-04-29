using AlunosApi.Context;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//adicionando Iconfiguration devido a necessidade do AddAuthenticationda autentica��o

var provider = builder.Services.BuildServiceProvider();
var Configuration = provider.GetRequiredService<IConfiguration>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cone��o sql server default aqui mesmo
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=LAPTOP-6KK5BAFI\\SQLEXPRESS;Initial Catalog=AlunosDemoDB;Integrated Security=True"));

//abilitando o identity das tabelas padr�o
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//abilitando servi�os de login logout e criar usuario
builder.Services.AddScoped<IAuthenticate,AuthenticateService>();

//Abilitando autentica��o segundo aos padr�es da microsoft dos padr�es do Token JWT e tudo que ele vai receber

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //valida emissor do token
        ValidateAudience = true,
        ValidateLifetime = true, //valida tempo de vida ...
        ValidateIssuerSigningKey = true, // ... chave de assinatura do token ...
        ValidIssuer = Configuration["Jwt:Issuer"], // obtem o emissor do token no .appsettigs.json
        ValidAudience = Configuration["Jwt:Audience"],// obtem a audiencia ...
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) // valida chave do token
    };
});


//abilitando o  cors 
builder.Services.AddCors();


//Registro de servi�o de uma entidade (Aluno)
builder.Services.AddScoped<IAlunoService,AlunosService>();

//OBS* Para Configura��o do Swagger para aceirar o token jwt entre aplica��es de autentica��o da api devemos por o seguinte codigo

builder.Services.AddSwaggerGen(c => //declara��o do swagger ate aqui 
{
    c.SwaggerDoc("v1",new OpenApiInfo { Title = "Alunos.API", Version = "v1"});

    //codigo para aceitar o token na aplica��o

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme." + "Enter 'Bearer' [space] and then your token in the next input below" + "Example: Bearer 12345abcdef",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



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

//autoriza��o e autentica��o devem estar exatamente da forma abaixo
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
