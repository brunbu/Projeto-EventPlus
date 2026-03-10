using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tipoUsuario"></param>
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

            _context.SaveChanges();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tipoUsuario"></param>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void Deletar(Guid id)
    {
        var tipoEventoBuscado = _context.TipoUsuarios.Find(id);
        if (tipoEventoBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoEventoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(TipoEvento => TipoEvento.Titulo).ToList();
    }
}
