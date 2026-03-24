using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
        {
            _context = context;
    }

    /// <summary>
    /// Método que atualiza um evento
    /// </summary>
    /// <paramname="id"></param>
    /// <paramname="evento"></param>
    public void Atualizar(Guid id, Evento evento)
    {
        var eventoBuscado = _context.Evento.Find(id);
        if (evento != null && evento != null)
        {
            // Atualize as propriedades relevantes do evento com base no DTO
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento;
            eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
            eventoBuscado.IdInstituicao = evento.IdInstituicao;
            eventoBuscado.Nome = evento.Nome!;

            _context.SaveChanges();
        }
    }

    public Evento BuscarPorId(Guid id)
    {
        return _context.Evento.Find(id)!;
    }
    /// <summary>
    /// Método que cadastra um evento
    /// </summary>
    /// <param name="evento"></param>
    public void Cadatrar(Evento evento)
    {
        _context.Evento.Add(evento);
        _context.SaveChanges();
    }
    /// <summary>
    /// Método que deleta um evento
    /// </summary>
    /// <param name="id">id do evento a ser deletado</param>
    public void Deletar(Guid id)
    {
        var EventoBuscado = _context.TipoEventos.Find(id);
        if (EventoBuscado != null)
        {
            _context.TipoEventos.Remove(EventoBuscado);

        }
    }
    /// <summary>
    /// Método que traz a lista de eventos disponiveis
    /// </summary>
    /// <returns>Uma lista com eventos disponiveis</returns>
    public List<Evento> Listar()
    {
        return _context.Evento.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).ToList();
    }
    /// <summary>
    /// Método que buscar evento no qual um usuario confirmou presença
    /// </summary>
    /// <param name="IdUsuario">Id do Usuario a ser buscado</param>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Evento.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.Presencas.Any(p => p.IdUsaurio == IdUsuario && p.Situacao == true)).ToList();
    }
    /// <summary>
    /// Método que traz a lista de oriximos eventos
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ProximosEventoS()
    {
        return _context.Evento.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.DataEvento > DateTime.Now).OrderBy(e => e.DataEvento).ToList();
    }
}
