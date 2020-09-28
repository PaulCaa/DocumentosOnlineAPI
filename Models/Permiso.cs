using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentosOnlineAPI.Models {
    public class Permiso {
        [Key]
        public int PermisoId { set; get; }
        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { set; get; }
        [ForeignKey("SectorId")]
        public Sector Sector { set; get; }
        [ForeignKey("DocumentoId")]
        public Documento Documento { set; get; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { set; get; }

        public override string ToString(){
            return "{\"PermisoId\": \"" + PermisoId + "\", "
            + "\"Empresa\": \"" + Empresa.ToString() + "\", "
            + "\"Sector\": \"" + Sector.ToString() + "\", "
            + "\"Documento\": \"" + Documento.ToString() + "\", "
            + "\"Usuario\": \"" + Usuario.ToString() + "\"}";
        }
    }
}