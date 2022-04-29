using Microsoft.AspNetCore.Identity;

namespace AlunosApi.Services
{
    public class AuthenticateService : IAuthenticate
    {
        //implementação de login,logout e criar usuario

        
        private readonly SignInManager<IdentityUser> _signInManager; //é o contexto de login de usuario
        private readonly UserManager<IdentityUser> _userManager; // é o contexto de criar usuario

        public AuthenticateService(SignInManager<IdentityUser> signInManager ,UserManager<IdentityUser> userManager)
        {
           _signInManager = signInManager;
            _userManager = userManager;
        }


        //metodo de autenticação com os parametros passados e declarados no AppDbcontext
        public async Task<bool> Authenticate(string email, string password)
        {
            //email,password <- parametros , false <- se quer salvar os dados do login automatico no navegador do usuario ,lockoutOnFailure:false <- a conta do usuario é bloqueada sempre que falhar no caso botamos falso
            var result = await _signInManager.PasswordSignInAsync(email,password,false,lockoutOnFailure:false); 
            return result.Succeeded; //retorno deu certo
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync(); //metodo de logout 
        }

        //registro do usuario
        public async Task<bool> RegisterUser(string email, string password)
        {
            var appUser = new IdentityUser //instancia de IdentityUser para apontar os parametros que já vem do identity e da regra de negocio da validação
            {
                UserName = email,
                Email = email,
            };
            var result = await _userManager.CreateAsync(appUser, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false); //login ok ao sair faça logout
            }
            return result.Succeeded;
        }
    }
}
