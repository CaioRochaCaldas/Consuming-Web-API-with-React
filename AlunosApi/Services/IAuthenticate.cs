namespace AlunosApi.Services
{
    public interface IAuthenticate
    {
        //contrato login e logout com email e senha

        Task<bool> AuthenticateAsync(string email,string password);
        Task Logout();

    }
}
