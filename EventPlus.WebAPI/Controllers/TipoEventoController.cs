using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private ITipoEventoRepository _tipoEventoRepository;

        public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
        {
            _tipoEventoRepository = tipoEventoRepository;
        }
        /// <summary>
        /// Endpoint da API que Faz chamada para o metodo de listar os tipos de evento
        /// </summary>
        /// <returns>Status code 200 e a lista de tipos de evento</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_tipoEventoRepository.Listar());

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que Faz chamada para o metodo de cadastrar um tipo de evento especifico
        /// </summary>
        /// <param name="id">Id do tipo de evento buscado</param>
        /// <returns>Status code 200 e tipo de evento buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_tipoEventoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que Faz chamada para o metodo de cadastrar um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">tipo de evento a ser cadastrado</param>
        /// <returns>StatusCode 201 e o tipo de evento cadasrar</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
        {
            try
            {
                var novoTipoEvento = new TipoEvento
                {
                    Titulo = tipoEvento.Titulo!
                };
                
                _tipoEventoRepository.Cadastrar(novoTipoEvento);
                return StatusCode(201, novoTipoEvento);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que Faz chamada para o metodo de atualizar um tipo de evento 
        /// </summary>
        /// <param name="id">Id do tipo de evento a ser atualizado</param>
        /// <param name="tipoEvento">tipo de evento com os dados atualizados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento)
        {
            try
            {
                var TipoEventoAtualizado = new TipoEvento
                {
                    Titulo = tipoEvento.Titulo!
                };

                _tipoEventoRepository.Atualizar(id, TipoEventoAtualizado);
                return StatusCode(204, TipoEventoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que Faz chamada para o metodo de deletar um tipo de evento
        /// </summary>
        /// <param name="id">Id do tipo do evento a ser excluido</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _tipoEventoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}