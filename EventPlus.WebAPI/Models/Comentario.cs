using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Comentario")]
public partial class ComentarioEvento
{
    [Key]
    public Guid IdComentario { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public bool Exibe { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataComentarioEvento { get; set; }

    public Guid? IdUsaurio { get; set; }

    public Guid? IdEvento { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Comentarios")]
    [JsonIgnore]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsaurio")]
    [InverseProperty("Comentarios")]
    public virtual Usuario? IdUsaurioNavigation { get; set; }
}
