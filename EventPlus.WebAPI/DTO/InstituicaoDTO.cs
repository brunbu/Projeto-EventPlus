using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class InstituicaoDTO
{
    [Required(ErrorMessage ="O titulo do tipo de Instituição e obrigatorio!") ]
    public string? NomeFantasia { get; set; }
    public string? CNPJ { get; set; }
    public string? Endereco { get; set; }
}
