using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuarioRepository _tipoUsuarioRepository;
    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }
    /// <summary>
    /// Endpoint da API que Faz chamada para o metodo de listar os tipos de Usuario
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {

            return Ok(_tipoUsuarioRepository.Listar());

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }

    }
    /// <summary>
    /// Endpoint da API que Faz chamada para o metodo de buscar um tipo de Usuario especifico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }

    }
    /// <summary>
    /// Endpoint da API que Faz chamada para o metodo de cadastrar um novo tipo de Usuario
    /// </summary>
    /// <param name="tipoUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201, novoTipoUsuario);

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que Faz chamada para o metodo de atualizar um tipo de Usuario especifico
    /// </summary>
    /// <param name="id">Id do tipo de Usuario a ser atualizado</param>
    /// <param name="tipoUsuario">tipo de Usuario com os dados atualizados</param>
    /// <returns></returns>
    [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id,TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var TipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Atualizar(id, TipoUsuarioAtualizado);
            return StatusCode(204, TipoUsuarioAtualizado);
        }catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
        
    }
    /// <summary>
    /// Endpoint da API que Faz chamada para o metodo de deletar um tipo de usuario especifico
    /// </summary>
    /// <param name="id">Id do tipo do Usuario a ser excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}


