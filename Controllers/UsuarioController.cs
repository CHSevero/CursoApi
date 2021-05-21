using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CursoAPI.Models.Usuarios;
using Swashbuckle.AspNetCore.Annotations;
using CursoAPI.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CursoAPI.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using CursoAPI.Business.Entities;
using CursoAPI.Business.Repositories;
using CursoAPI.Infraestruture.Data.Repositories;
using Microsoft.Extensions.Configuration;
using CursoAPI.Configuration;

namespace CursoAPI.Controllers
{
    
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        public UsuarioController(
            IUsuarioRepository usuarioRepository, 
            IAuthenticationService authenticationService
            )
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar",Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCompoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput) {  

            Usuario usuario =_usuarioRepository.ObterUsuario(loginViewModelInput.Login);
            if(usuario == null){
                return BadRequest("Houve um error ao tentar acessar. ");
            }
            var usuarioViewModelOutput = new UsuarioViewModelOutput(){
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

           
            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new 
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar",Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCompoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput) {

            // var migracoesPendentes = contexto.Database.GetPendingMigrations();
            // if(migracoesPendentes.Count() > 0){
            //     contexto.Database.Migrate();
            // }

            var usuario = new Usuario();
            usuario.Login = registroViewModelInput.Login;
            usuario.Senha = registroViewModelInput.Senha;
            usuario.Email = registroViewModelInput.Email;

            
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registroViewModelInput);
        }

    }

}