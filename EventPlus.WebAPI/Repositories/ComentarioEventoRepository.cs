using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentariosEventoRepository : IComentariosRepository
{
    private readonly EventContext _context;
    public ComentariosEventoRepository(EventContext context)
    {
        _context = context;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        throw new NotImplementedException();
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.Comentarios.Add(comentarioEvento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var comentarioBuscado = _context.Comentarios.Find(id);
        if (comentarioBuscado != null)
        {
            _context.Comentarios.Remove(comentarioBuscado);
            _context.SaveChanges();
        }

    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.Comentarios.Include(e => e.IdUsaurioNavigation).Include(e => e.IdComentario).ToList();
    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid idEvento)
    {
        return _context.Comentarios.Include(e => e.IdUsaurioNavigation).Include(e => e.IdComentario).Where(e => e.IdComentario(p => p.IdUsaurio == IdUsuario && p.Comentarios == true)).ToList();

    }
}