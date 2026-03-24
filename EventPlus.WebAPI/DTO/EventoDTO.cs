using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O campo Descricao é obrigatório.")]
    public string Descricao { get; set; } = null!;
    [Required(ErrorMessage = "O campo Data é obrigatório.")]
    public DateTime DataEvento { get; set; }
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Nome { get; set; }
    public string? NomeFantasia { get; set; }
    public Guid Id { get; set; }
}
