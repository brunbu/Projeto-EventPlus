using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
    void Increver(Presenca inscricao);
    void Deleter(Guid id);
    List<Presenca> Listar();
    Presenca BuscarPorId(Guid id);
    void Atualizar(Guid id);
    List<Presenca> ListarMinhas(Guid idUsuario);
}
