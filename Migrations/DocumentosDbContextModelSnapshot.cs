﻿// <auto-generated />
using System;
using DocumentosOnlineAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentosOnlineAPI.Migrations
{
    [DbContext(typeof(DocumentosDbContext))]
    partial class DocumentosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Campo", b =>
                {
                    b.Property<int>("CampoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CampoId");

                    b.ToTable("Campos");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Documento", b =>
                {
                    b.Property<int>("DocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("SectorId")
                        .HasColumnType("int");

                    b.HasKey("DocumentoId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("SectorId");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.DocumentoCampo", b =>
                {
                    b.Property<int>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<int>("CampoId")
                        .HasColumnType("int");

                    b.HasKey("DocumentoId", "CampoId");

                    b.HasIndex("CampoId");

                    b.ToTable("DocumentosCampos");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Empresa", b =>
                {
                    b.Property<int>("EmpresaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnName("Descripcion")
                        .HasColumnType("nText");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("EmpresaId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Permiso", b =>
                {
                    b.Property<int>("PermisoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DocumentoId")
                        .HasColumnType("int");

                    b.Property<int?>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<int?>("SectorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("PermisoId");

                    b.HasIndex("DocumentoId");

                    b.HasIndex("IdEmpresa");

                    b.HasIndex("SectorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Sector", b =>
                {
                    b.Property<int>("SectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SectorId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Sectores");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("HashPwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Documento", b =>
                {
                    b.HasOne("DocumentosOnlineAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.HasOne("DocumentosOnlineAPI.Models.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorId");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.DocumentoCampo", b =>
                {
                    b.HasOne("DocumentosOnlineAPI.Models.Campo", "Campo")
                        .WithMany("DocumentoCampos")
                        .HasForeignKey("CampoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentosOnlineAPI.Models.Documento", "Documento")
                        .WithMany("DocumentoCampos")
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Permiso", b =>
                {
                    b.HasOne("DocumentosOnlineAPI.Models.Documento", "Documento")
                        .WithMany()
                        .HasForeignKey("DocumentoId");

                    b.HasOne("DocumentosOnlineAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("IdEmpresa");

                    b.HasOne("DocumentosOnlineAPI.Models.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorId");

                    b.HasOne("DocumentosOnlineAPI.Models.Usuario", "Usuario")
                        .WithMany("Permisos")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Sector", b =>
                {
                    b.HasOne("DocumentosOnlineAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");
                });

            modelBuilder.Entity("DocumentosOnlineAPI.Models.Usuario", b =>
                {
                    b.HasOne("DocumentosOnlineAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");
                });
#pragma warning restore 612, 618
        }
    }
}
