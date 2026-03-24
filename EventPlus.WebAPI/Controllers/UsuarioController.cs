using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo Buscar um usuario 
    /// por id
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Status code 200 e o usuario buscado</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var usuario = _usuarioRepository.BuscarPorId(id);
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
        /// <summary>
        /// Endpoint da API que faz a chamada para o metodo Cadastrar um usuario
        /// </summary>
        /// <param name="id">Usuario a ser cadastrado</param>
        /// <returns>Status code 201 e o usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
            try
       {

            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Email = usuario.Email!,
                Senha = usuario.Senha!,
                IdTipoUsuario = usuario.IdTituloUsuario
            };

                _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201, novoUsuario);
       }
            catch (Exception erro)
       {
                return BadRequest(erro.Message);
       }
    }
}
