using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Controllers;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoEventoRepository : ITipoEventoRepository
{
    private readonly EventContext _context;

    public TipoEventoRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipoEvento"></param>
    public void Atualizar(Guid id, TipoEvento tipoEvento)
    {
        var tipoEventoList = _context.TipoEventos.Find(id);

        if (tipoEventoList != null)
        {
            tipoEventoList.Titulo = tipoEvento.Titulo;

            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca um tipo de evento por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>objeto do tipoEvento cpm as informações do tip evento buscado</returns>

    public TipoEvento BuscarPorId(Guid id)
    {
       return _context.TipoEventos.Find(id)!;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">id do tipo evento a ser deletado</param>
    public void Cadastrar(TipoEvento tipoEvento)
    {
        _context.TipoEventos.Add(tipoEvento);
        _context.SaveChanges();
    }

    public void Cadastrar(TipoEventoController tipoEvento)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// deleta um tipo de evento
    /// </summary>
    /// <param name="id"></param>
    public void Deletar(Guid id)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);
        if (tipoEventoBuscado != null)
        {
            _context.TipoEventos.Remove(tipoEventoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca a lista de tipo de eventos cadastrados
    /// </summary>
    /// <returns>Uma lista tipo evento</returns>
    public List<TipoEvento> Listar()
    {
        return _context.TipoEventos.OrderBy(TipoEvento => TipoEvento.Titulo).ToList();
    }
}
