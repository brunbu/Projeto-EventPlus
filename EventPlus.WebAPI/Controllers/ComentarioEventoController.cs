using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;

    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyCliente, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyCliente;
        _comentarioEventoRepository = comentarioEventoRepository;

    }
    /// <summary>
    ///  Endpoint da API que cadastra e modera um comentario
    /// </summary>
    /// <param name="comentarioEvento">comentario a ser moderado</param>
    /// <returns>Status code 201 e o comentario criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio.");
            }
            // criar objeto de analise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            // chamar a API da Azure Content Safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);
            
            // Verificar se texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentarioEvento => comentarioEvento.Severity > 0);

            var novoComentario = new Models.ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsaurio = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comentário vai ser exibido
                Exibe = !temConteudoImproprio
            };

            //
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
    [HttpGet("{IdEvento}")]
    public IActionResult Listar(Guid IdEvento)
    {
        try
        {
            var comentarios = _comentarioEventoRepository.Listar(IdEvento);
            return Ok(comentarios);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("BuscarPorIdUsuario/{idUsuario, idEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os comentários de um evento
    /// </summary>
    /// <param name="idEvento">id do evento para a filtragem</param> 
    /// <returns>Status Code 200 e uma lista de comentários filtrada pelo evento</returns> 
    [HttpGet("BuscarPorEvento/{idEvento}")]
    public IActionResult BuscarPorEvento(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de deletar um comentário pelo seu id
    /// </summary>
    /// <param name="id">id do comentário a ser deletado</param> 
    /// <returns>Status Code 201</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("ListarSomenteExibe/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

