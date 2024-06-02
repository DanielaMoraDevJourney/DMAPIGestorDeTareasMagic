using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMAPIGestorDeTareasMagic.Data.Models;
    public partial class Dmcategorium
    {
        [Key]
        public int DmcategoriaId { get; set; }

        [Required]
        public string Dmnombre { get; set; } = null!;

        public string? Dmdescripcion { get; set; }

        public int? DmtareaId { get; set; }

        public virtual Dmtarea? Dmtarea { get; set; }
    }

