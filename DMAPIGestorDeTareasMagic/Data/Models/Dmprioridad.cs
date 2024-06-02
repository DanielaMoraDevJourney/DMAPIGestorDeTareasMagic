using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMAPIGestorDeTareasMagic.Data.Models;

public partial class Dmprioridad
{
    [Key]
    public int DmprioridadId { get; set; }

    [Required]
    public string Dmnombre { get; set; } = null!;

    public string Dmdescripcion { get; set; } = null!;

    public virtual Dmtarea? Dmtarea { get; set; }
}
