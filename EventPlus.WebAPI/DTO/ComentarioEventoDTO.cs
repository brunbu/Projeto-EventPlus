namespace EventPlus.WebAPI.DTO;

public class ComentarioEventoDTO
{
    public string Descricao { get; set; } = null!;
    public Guid IdUsuario { get; set; }
    public Guid IdEvento { get; set; }
    public DateTime DataComentarioEvento { get; internal set; }
    public bool Exibe { get; internal set; }
}
