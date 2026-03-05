using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Presenca")]
public partial class Presenca
{
    [Key]
    public Guid IdPresenca { get; set; }

    public bool Situacao { get; set; }

    public Guid? IdUsaurio { get; set; }

    public Guid? IdEvento { get; set; }

    [ForeignKey("IdEvento")]
    [InverseProperty("Presencas")]
    public virtual Evento? IdEventoNavigation { get; set; }

    [ForeignKey("IdUsaurio")]
    [InverseProperty("Presencas")]
    public virtual Usuario? IdUsaurioNavigation { get; set; }
}
