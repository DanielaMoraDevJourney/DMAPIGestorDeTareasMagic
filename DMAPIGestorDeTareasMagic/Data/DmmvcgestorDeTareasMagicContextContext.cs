using System;
using System.Collections.Generic;
using DMAPIGestorDeTareasMagic.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DMAPIGestorDeTareasMagic.Data
{
    public partial class DmmvcgestorDeTareasMagicContextContext : DbContext
    {
        public DmmvcgestorDeTareasMagicContextContext()
        {
        }

        public DmmvcgestorDeTareasMagicContextContext(DbContextOptions<DmmvcgestorDeTareasMagicContextContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dmcategorium> Dmcategoria { get; set; }
        public virtual DbSet<Dmprioridad> Dmprioridads { get; set; }
        public virtual DbSet<Dmtarea> Dmtareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DMMVCGestorDeTareasMagicContext;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dmcategorium>(entity =>
            {
                entity.HasKey(e => e.DmcategoriaId);

                entity.ToTable("DMCategoria");

                entity.HasIndex(e => e.DmtareaId, "IX_DMCategoria_DMTareaID");

                entity.Property(e => e.DmcategoriaId).HasColumnName("DMCategoriaID");
                entity.Property(e => e.Dmdescripcion).HasColumnName("DMDescripcion");
                entity.Property(e => e.Dmnombre).HasColumnName("DMNombre");
                entity.Property(e => e.DmtareaId).HasColumnName("DMTareaID");

                entity.HasOne(d => d.Dmtarea)
                      .WithMany(p => p.Dmcategoria)
                      .HasForeignKey(d => d.DmtareaId);
            });

            modelBuilder.Entity<Dmprioridad>(entity =>
            {
                entity.HasKey(e => e.DmprioridadId);

                entity.ToTable("DMPrioridad");

                entity.Property(e => e.DmprioridadId).HasColumnName("DMPrioridadID");
                entity.Property(e => e.Dmdescripcion).HasColumnName("DMDescripcion");
                entity.Property(e => e.Dmnombre).HasColumnName("DMNombre");
            });

            modelBuilder.Entity<Dmtarea>(entity =>
            {
                entity.HasKey(e => e.DmtareaId);

                entity.ToTable("DMTarea");

                entity.HasIndex(e => e.DmprioridadId, "IX_DMTarea_DMPrioridadID").IsUnique();

                entity.Property(e => e.DmtareaId).HasColumnName("DMTareaID");
                entity.Property(e => e.DmcategoriaId).HasColumnName("DMCategoriaID");
                entity.Property(e => e.Dmdescripcion).HasColumnName("DMDescripcion");
                entity.Property(e => e.DmfechaVencimiento).HasColumnName("DMFechaVencimiento");
                entity.Property(e => e.DmprioridadId).HasColumnName("DMPrioridadID");
                entity.Property(e => e.Dmtitulo).HasColumnName("DMTitulo");

                entity.HasOne(d => d.Dmprioridad)
                      .WithOne(p => p.Dmtarea)
                      .HasForeignKey<Dmtarea>(d => d.DmprioridadId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
