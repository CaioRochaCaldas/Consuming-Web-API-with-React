using AlunosApi.Models;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAlunos(); //contrato metodo obter todos os alunos.
        Task<Aluno> GetAluno(int id); //... obter um aluno por id
        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome); // ... alunos por nome
        Task CreateAluno(Aluno aluno); // criar novo aluno
        Task UpdateAluno(Aluno aluno); // atualizar novo aluno
        Task DeleteAluno(Aluno aluno); // deletar aluno
    }
}
