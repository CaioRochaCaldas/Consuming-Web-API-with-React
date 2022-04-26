using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;
        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        //Eu Transformai todos os metodos async 
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }
        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            //após o nome ser passado devolva:
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrWhiteSpace(nome))
            { //alunos por nome
                alunos = await _context.Alunos.Where(n => n.Name.Contains(nome)).ToListAsync();
            }
            else //ou
            {
                alunos = await GetAlunos(); //todos os alunos
            }
            return alunos;
        }

        //retorno de um aluno pelo seu id
        public async Task<Aluno> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);//.FindAsync(id)<- vai na memoria e bisca ,ou também .FirstourDefault(id); <- vai no banco e busca 
            return aluno;
        }

        //cria um aluno
        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }
        //atualiza aluno
        public async  Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified; //passa os dados para entidade aluno que eu quero modificar e modifico
            await _context.SaveChangesAsync();
        }

        //deleto aluno
        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno); //remove do contesto
            await _context.SaveChangesAsync();
        }

    }
}
