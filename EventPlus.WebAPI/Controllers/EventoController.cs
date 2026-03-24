using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo listar eventos filtado pelo id do usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo lista eventos
    /// </summary>
    /// <returns>Lista eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventoS());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);    
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para cadastrar um evento  
    /// </summary>
    /// <param name="evento"></param>
    /// <returns>Um evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(Evento evento)
    {
        try
        {
            _eventoRepository.Cadatrar(evento);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para atualizar um evento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="evento"></param>
    /// <returns>Um a=evento atualizado</returns>
    [HttpPut]
    public IActionResult Atualizar(Guid id, Evento evento)
    {
        try
        {
            _eventoRepository.Atualizar(id, evento);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para deletar um evento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
