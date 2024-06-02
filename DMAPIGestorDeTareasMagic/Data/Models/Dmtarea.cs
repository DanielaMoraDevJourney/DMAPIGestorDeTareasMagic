using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMAPIGestorDeTareasMagic.Data.Models;

public partial class Dmtarea
{
    [Key]
    public int DmtareaId { get; set; }

    [Required]

    public string Dmtitulo { get; set; } = null!;

    public string Dmdescripcion { get; set; } = null!;

    public DateTime DmfechaVencimiento { get; set; }

    public int DmprioridadId { get; set; }

    public int DmcategoriaId { get; set; }

    public virtual ICollection<Dmcategorium> Dmcategoria { get; set; } = new List<Dmcategorium>();

    public virtual Dmprioridad Dmprioridad { get; set; } = null!;
}
