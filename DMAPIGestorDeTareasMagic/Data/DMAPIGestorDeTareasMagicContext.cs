using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMAPIGestorDeTareasMagic.Data.Models;

namespace DMAPIGestorDeTareasMagic.Data
{
    public class DMAPIGestorDeTareasMagicContext : DbContext
    {
        public DMAPIGestorDeTareasMagicContext (DbContextOptions<DMAPIGestorDeTareasMagicContext> options)
            : base(options)
        {
        }

        public DbSet<DMAPIGestorDeTareasMagic.Data.Models.Dmcategorium> Dmcategorium { get; set; } = default!;
        public DbSet<DMAPIGestorDeTareasMagic.Data.Models.Dmprioridad> Dmprioridad { get; set; } = default!;
        public DbSet<DMAPIGestorDeTareasMagic.Data.Models.Dmtarea> Dmtarea { get; set; } = default!;
    }
}
