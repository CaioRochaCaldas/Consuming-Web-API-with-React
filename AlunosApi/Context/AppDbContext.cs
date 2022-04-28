using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    //agora o contexto não é mais do entiry e sim do Identiy e vai ser de tipo usuario
    //quando usamos o IdentityUser ele possui dados de um usuario que vai ter suas tabelas no banco
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {

        }
        public DbSet<Aluno> Alunos { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder) //propiedades de enviar dados
        {
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Name = "Maria da Penha",
                    Email = "mariapenha@yahoo.com",
                    Idade = 23
                },
                new Aluno
                {
                    Id = 2,
                    Name = "Manuel Bueno",
                    Email = "manualbueno@yahoo.com",
                    Idade = 22
                }
                );
        }*/
    }
}
