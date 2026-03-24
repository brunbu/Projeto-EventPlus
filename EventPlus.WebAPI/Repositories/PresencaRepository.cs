using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;
    public PresencaRepository(EventContext eventcontext)
    {
        _eventContext = eventcontext;
    }

    public void Atualizar(Guid id, Presenca presenca)
    {
        var presencaBuscada = _eventContext.Presencas.Find(presenca);
        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presenca.Situacao;

            _eventContext.SaveChanges();
        }
    }
    /// <summary>
    /// Busca a presença por id da
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
    }
    /// <summary>
    /// Deleta a presença por id da presença
    /// </summary>
    /// <param name="id"></param>
    public void Deleter(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }
    /// <summary>
    /// Inscreve um usuário em um evento
    /// </summary>
    /// <param name="inscricao"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Inscrever(Presenca inscricao)
    {
        var presencaBuscada = _eventContext.Presencas.FirstOrDefault(p => p.IdUsaurio == inscricao.IdUsaurio && p.IdEvento == inscricao.IdEvento);
        if (presencaBuscada != null)
        {
            throw new InvalidOperationException("Usuário já inscrito neste evento.");
        }
    }
    /// <summary>
    /// Lista todas as presenças cadastradas
    /// </summary>
    /// <returns></returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).ToList();
    }
    /// <summary>
    /// Lista as presenças de um usuário especifico
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presença de presencas de um usuario especifico</returns>
    public List<Presenca> ListarMinhas(Guid idUsuario)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsaurio == idUsuario).ToList();    }
}
