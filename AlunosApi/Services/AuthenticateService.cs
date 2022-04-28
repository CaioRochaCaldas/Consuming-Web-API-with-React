using Microsoft.AspNetCore.Identity;

namespace AlunosApi.Services
{
    public class AuthenticateService : IAuthenticate
    {
        //implementação de login e logout

        //classe de usuario recebe dados do contrutor dos dados
        private readonly SignInManager<IdentityUser> _signInManager; //ou seja, é contexto de usuarios

        public AuthenticateService(SignInManager<IdentityUser> signInManager)
        {
           _signInManager = signInManager;
        }

        //metodo de autenticação com os parametros passados e declarados no AppDbcontext
        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            //email,password <- parametros , false <- se quer salvar os dados do login automatico no navegador do usuario ,lockoutOnFailure:false <- a conta do usuario é bloqueada sempre que falhar no caso botamos falso
            var result = await _signInManager.PasswordSignInAsync(email,password,false,lockoutOnFailure:false); 
            return result.Succeeded; //retorno deu certo
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync(); //metodo de logout 
        }
    }
}
