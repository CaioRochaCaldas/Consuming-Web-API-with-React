using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //proteção para apenas usuario autorizados com a jwt podem usar a api
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        //lista de todos os alunos

        [HttpGet] 
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos() {

            try
            {
                var alunos = await _alunoService.GetAlunos(); //aponta ao get alunos do serviço aluno
                return Ok(alunos); //lista de alunos devolve
            }
            catch 
            {
                //return BadRequest("Request invalido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }

        }

        //lista de alunos por nome especifico
            [HttpGet("AlunoPorNome")]
            public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNome([FromQuery] string nome) { //query especifica é passada e usa um nome passado
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome); //aponta ao get alunos do serviço aluno

                if (alunos.Count() == 0) //conte os nomes iguais ao dado e exiba se não achar
                    return NotFound($"Não existem alunos com o criterio {nome}" );
                return Ok(alunos);

            }
            catch
            {
                return BadRequest("Request invalido");
                //return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }

         }

            //mostra um  aluno  por nome especifico

             [HttpGet("{id:int}", Name = "GetAluno")]
            public async Task<ActionResult<Aluno>> GetAluno(int id)
            {
                try
                {
                    var aluno = await _alunoService.GetAluno(id);
                if (aluno == null) return NotFound($"Não existe aluno com id={id}");
                
                    return Ok(aluno);
                }
                catch
                { 
                    return BadRequest("Request inválido");
                }
            }

            // cria aluno e avisa qual o id do aluno criado e referencia 
            [HttpPost]
            public async Task<ActionResult> Create(Aluno aluno)
            {
                try
                {
                    await _alunoService.CreateAluno(aluno);
                    return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);//referencia do id criado desse aluno (recursos que acabou de criar)
                           //CreatedAtRoute(nameof(GetAluno) referencia do recurso resem criado
            }
            catch
                { 
                     return BadRequest("Request invalido");
                }
            }
            //Atualiza um aluno com um id especifico

            [HttpPut("{id:int}")]
            public async Task<ActionResult> Edit(int id,[FromBody] Aluno aluno)
            {
                try
                {
                    if(aluno.Id == id)
                    {
                            await _alunoService.UpdateAluno(aluno);
                            return Ok($"Aluno com id={id} foi atualizado com sucesso");
                    }

                    else
                    {
                    return BadRequest("Dados inconsistentes");
                    }
                }
                catch
                {
                    return BadRequest("Request invalido");
                }
            }
        //deleta aluno
            [HttpDelete("{id:int}")]
            public async Task<ActionResult> Delete(int id)
            {
                try
                {
                    var aluno = await _alunoService.GetAluno(id);

                    if (aluno != null)
                    {
                        await _alunoService.DeleteAluno(aluno);
                        return Ok($"Aluno de id={id} foi excluido com sucesso");
                    }
                    else
                    {
                        return NotFound($"Aluno com id={id} não encontrado");
                    }

                }
                catch
                {
                    return BadRequest("Request invalido");
                }
            }


        }
}
