using AlunosApi.Services;
using AlunosApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration; //interface de configurações base que refere-se ao arquivo json com a chave jwt
        private readonly IAuthenticate _authentication; //interface  de login

        //Este construtor vai deixar ter acesso aos serviços acima (login + .json(jwt))
        public AccountController(IConfiguration configuration, IAuthenticate authentication)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
        }


        //url endpoint de criar um usuario
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword) //teste se as senhas do usuario confere com uma conta ou não
            {
                ModelState.AddModelError("ConfirmPassword","As senhas não conferem");
                return BadRequest(ModelState); //senha não conferiu
            }
                var result = await _authentication.RegisterUser(model.Email, model.Password); //se senha conferiu e criou usuario

            if (result)
            {
                return Ok($"Usuário {model.Email} criado com sucesso"); //tudo certo usuario criado
            }
            else
            {
                ModelState.AddModelError("CreateUser","Registro inválido");//ModelState sava os dados inseridos para eventuais erros de validação e como nós vamos lidar com eles
                return BadRequest(ModelState);
            }
            
        }


        //logar usuario já autenticado (criou email e senha  e só quer entrar)
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
           var result = await _authentication.Authenticate(userInfo.Email,userInfo.Password); //se email e senha conferem

            if (result)
            {
                return GenerateToken(userInfo); //gera o token
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido");//erro ao criar o token (observe o detalhe do ModelState aqui mesma funcionalidade)
                return BadRequest(ModelState);
            }


            
        }

        //metodo gerar token caso tudo der certo ao fazer login logo acima
        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[] //aponta os integrantes do token dessa clain que é a validação futura e final do JWT
            {
                new Claim("email",userInfo.Email),
                new Claim("meuToken","token do macoratti"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"])); //a chave secreta que vem do json para o token

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256); //objeto das chaves 

            var expiration = DateTime.UtcNow.AddMinutes(20); //apos 20 min o token deixa de funcionar


            //classe do token que recebe todos os parametros de criação de token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken() //converte o token jwt em formato string para usar (deixar de ser objeto)
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };

        }
    }
}
