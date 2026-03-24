using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Instituicao")]
[Index("Endereço", Name = "UQ__Institui__4DFC5FCE34D0A8D8", IsUnique = true)]
[Index("Cnpj", Name = "UQ__Institui__AA57D6B42E7BC5EA", IsUnique = true)]
public partial class Instituicao
{
    [Key]
    public Guid IdInstituicao { get; set; }
    
    [StringLength(100)]
    [Unicode(false)]
    public string NomeFantasia { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Endereço { get; set; } = null!;

    [Column("CNPJ")]
    [StringLength(14)]
    [Unicode(false)]
    public string Cnpj { get; set; } = null!;

    [InverseProperty("IdInstituicaoNavigation")]
    [JsonIgnore]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
