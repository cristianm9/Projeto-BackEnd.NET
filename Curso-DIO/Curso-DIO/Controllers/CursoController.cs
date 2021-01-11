using Curso_DIO.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso_DIO.Controllers
{
    [Route("api/c1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
      
        [HttpPost]
        [Route("")]
    
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput) 
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?.Value);
            return Created("", cursoViewModelInput);
        }
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CursosViewModelOutput>();
            //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            cursos.Add(new CursosViewModelOutput()
            {
                Login = "",
                Descricao = "teste",
                Nome = "teste"
            });

            return Ok(cursos);
        }
    }

}
