using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DocumentosOnlineAPI.Models;

namespace DocumentosOnlineAPI.Data {
    public partial class DocumentosDbContext : DbContext {

        public DbSet<Documento> Documentos { set; get; }
        public DbSet<Empresa> Empresas { set; get; }
        public DbSet<Sector> Sectores { set; get; }
        public DbSet<Usuario> Usuarios { set; get; }
        public DbSet<UsuarioSector> UsuarioSectores { set; get; }

        public DocumentosDbContext() {}

        public DocumentosDbContext(DbContextOptions<DocumentosDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost;database=DocumentosOnline;user=user;password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UsuarioSector>().HasKey(
                us => new { us.UsuarioId, us.SectorId }
            );
            modelBuilder.Entity<UsuarioSector>()
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(u => u.UsuarioSectores)
            .HasForeignKey(us => us.UsuarioId);
            modelBuilder.Entity<UsuarioSector>()
            .HasOne<Sector>(us => us.Sector)
            .WithMany(s => s.UsuarioSectores)
            .HasForeignKey(us => us.SectorId);
            /**********************************/
            modelBuilder.Entity<Sector>()
            .HasOne<Empresa>(s => s.Empresa)
            .WithMany(e => e.Sectores)
            .HasForeignKey(s => s.EmpresaId);
            /**********************************/
            modelBuilder.Entity<Documento>()
            .HasOne<Empresa>(d => d.Empresa)
            .WithMany(e => e.Documentos)
            .HasForeignKey(d => d.EmpresaId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            /**********************************/
            modelBuilder.Entity<Documento>()
            .HasOne<Sector>(d => d.Sector)
            .WithMany(s => s.Documentos)
            .HasForeignKey(d => d.SectorId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            /**********************************/
            modelBuilder.Entity<Usuario>()
            .HasOne<Empresa>(u => u.Empresa)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(u => u.EmpresaId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            /**********************************/
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
