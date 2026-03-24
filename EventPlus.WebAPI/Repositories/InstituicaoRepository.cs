using EventPlus.WebAPI.BdContEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = String.IsNullOrWhiteSpace (instituicao.NomeFantasia) ? instituicao.NomeFantasia : instituicao.NomeFantasia;

            instituicaoBuscada.Cnpj =String.IsNullOrWhiteSpace (instituicao.Cnpj) ? instituicaoBuscada.Cnpj : instituicaoBuscada.Cnpj;

            instituicaoBuscada.Endereço = String.IsNullOrWhiteSpace (instituicao.Endereço) ? instituicao.Endereço : instituicao.Endereço;
           
            _context.SaveChanges();
        }
    }
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(Instituicao => Instituicao.NomeFantasia).ToList();
    }
}
