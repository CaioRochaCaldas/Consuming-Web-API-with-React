namespace AlunosApi.Services
{
    public interface IAuthenticate
    {
        //contrato login e logout com email e senha

        //loga usuario
        Task<bool> AuthenticateAsync(string email,string password);
        
        //Cria usuario
        Task<bool> RegisterUser(string email,string password);
        
        //logout usuario
        Task Logout();

    }
}
