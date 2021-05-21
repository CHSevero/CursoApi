using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoAPI.Models.Cursos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using CursoAPI.Business.Repositories;
using CursoAPI.Business.Entities;

namespace CursoAPI.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursosRepository _cursoRepository;

        public CursoController(ICursosRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 400, description: "Não autorizado")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput){

            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.Codigo = codigoUsuario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();
            return Created("", cursoViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao Cadastrar os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [SwaggerResponse(statusCode: 500, description: "Erro interno")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(){
            
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    Login = s.Usuario.Login
                });

            return Ok(cursos);
        }
    }
}