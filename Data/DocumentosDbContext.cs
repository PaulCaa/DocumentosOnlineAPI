using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DocumentosOnlineAPI.Models;

namespace DocumentosOnlineAPI.Data
{
    public partial class DocumentosDbContext : DbContext
    {
        public DbSet<Campo> Campos { set; get; }
        public DbSet<Documento> Documentos { set; get; }
        public DbSet<Empresa> Empresas { set; get; }
        public DbSet<Permiso> Permisos { set; get; }
        public DbSet<Sector> Sectores { set; get; }
        public DbSet<Usuario> Usuarios { set; get; }
        public DbSet<DocumentoCampo> DocumentosCampos { set; get; }

        public DocumentosDbContext()
        {}

        public DocumentosDbContext(DbContextOptions<DocumentosDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("server=localhost;database=DocumentosOnline;user=user;password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relacion N a N
            modelBuilder.Entity<DocumentoCampo>().HasKey(
                dc => new { dc.DocumentoId, dc.CampoId }
            );
            // FK
            modelBuilder.Entity<DocumentoCampo>()
            .HasOne<Documento>(dc => dc.Documento)
            .WithMany(d => d.DocumentoCampos)
            .HasForeignKey(dc => dc.DocumentoId);
            // FK
            modelBuilder.Entity<DocumentoCampo>()
            .HasOne<Campo>(dc => dc.Campo)
            .WithMany(c => c.DocumentoCampos)
            .HasForeignKey(dc => dc.CampoId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
