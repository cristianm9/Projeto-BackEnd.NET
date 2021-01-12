
using Curso_DIO.Business.Entities;
using Curso_DIO.Business.Repositories;
using Curso_DIO.Configurations;
using Curso_DIO.Infraestrutura.Data;
using Curso_DIO.Models;
using Curso_DIO.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Curso_DIO.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        
        private readonly IAuthenticationService _authenticationService;
        public UsuarioController(IUsuarioRepository usuarioRepository, 
          
            IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
           
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// teste
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retornar status ok,dados do usuario e o token e ativo.</returns>
        /// 
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode:400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
           var usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if (usuario == null) 
            {
                return BadRequest("Houve um erro ao tentar acessar.");

            }

            //if (usuario.Senha !=LoginViewModel.Senha.GerarSenhaCriptografada())
            //{
            //    return BadRequest("Houve um erro ao tentar acessar.");

            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };
            var token = _authenticationService.GerarToken(usuarioViewModelOutput);
            //var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations : Secret").Value);
            //var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            //var securityTokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
            //        new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
            //        new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature) 

            //};
            //var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);



            return Ok(new 
            {
             Token= token,
             Usuario = usuarioViewModelOutput
            });
        }
        /// <summary>
        /// Este Serviço  permite cadastrar  um usuario   cadastrado nao existente 
        /// </summary>
        /// <param name="registro">View Model  do Registro de Login</param>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        public IActionResult Registrar(RegistrarViewModelInput loginViewModelInput)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            //optionsBuilder.UseSqlServer();
            //CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            //var migracoesPendentes = contexto.Database.GetPendingMigrations();
            //if(migracoesPendentes.Count() >0) 
            //{
            //    contexto.Database.Migrate();
            //}


            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;
           

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();


            return Created("", loginViewModelInput);
        }
    }
}
