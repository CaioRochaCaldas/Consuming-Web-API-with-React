namespace AlunosApi.Services
{
    public interface IAuthenticate
    {
        //contrato login e logout com email e senha
        //declaramos aqui os parametros de autenticação que queremos no projeto



        //loga usuario
        Task<bool> AuthenticateAsync(string email,string password);
        
        //Cria usuario
        Task<bool> RegisterUser(string email,string password);
        
        //logout usuario
        Task Logout();

    }
}
